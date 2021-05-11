using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using Thundergen.Lightning;
using Json.Serialization;
using System.IO;

namespace Thundergen.UI
{
    public partial class BoltControl : UserControl
    {
        class CachedBolt
        {
            public object Bolt = null;
            public Vector3[] Path = null;
        }

        public event EventHandler<DBMBreakdown.GroundPropagationProgressEventArgs> BreakdownPropagationProgress;
        public event EventHandler<DBMBolt.PathGenerationProgressEventArgs> PathGenerationProgress;

        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;
        public event EventHandler<ValidityChangedEventArgs> CacheChanged;

        private CancellationTokenSource mCancelViaUI = null;

        private Dictionary<object, CachedBolt> mCache = new Dictionary<object, CachedBolt>();

        public bool Valid
        {
            get
            {
                if (tcConfig.SelectedTab == tpDBM)
                {
                    return dbmBoltControl1.Valid;
                }
                else
                {
                    throw new NotImplementedException("Selected bolt type is not yet supported");
                }
            }
        }

        public DBMBreakdown.Configuration BreakdownConfiguration
        {
            get
            {
                return dbmBoltControl1.BreakdownConfiguration;
            }
        }

        public DBMBolt.InterpolationConfiguration InterpolationConfiguration
        {
            get
            {
                return dbmBoltControl1.InterpolationConfiguration;
            }
        }

        public BoltControl()
        {
            InitializeComponent();

            ofdBolt.InitialDirectory = Directory.GetCurrentDirectory();
            sfdBolt.InitialDirectory = ofdBolt.InitialDirectory;
        }

        public async Task<Vector3[]> RequestPath(CancellationToken token)
        {
            if (mCache.ContainsKey(tcConfig.SelectedTab) && mCache[tcConfig.SelectedTab].Path != null) return mCache[tcConfig.SelectedTab].Path;

            if (tcConfig.SelectedTab == tpDBM)
            {
                mCancelViaUI = new CancellationTokenSource();
                this.Invoke(new Action(() => cmdGenerate.Text = "Pause"));
                token.Register(mCancelViaUI.Cancel);

                DBMBolt bolt = mCache.ContainsKey(tpDBM) && mCache[tpDBM].Bolt != null ? mCache[tpDBM].Bolt as DBMBolt : await dbmBoltControl1.RequestBolt(token);
                mCache[tpDBM] = new CachedBolt() { Bolt = bolt };
                Vector3[] path = await Asynchronizer.Wrap(() => bolt.GeneratePath(token, PathGenerationProgress));
                mCache[tpDBM].Path = path;

                this.Invoke(new Action(() =>
                {
                    cmdGenerate.Text = "Generate";
                    CacheChanged?.Invoke(this, new ValidityChangedEventArgs(true));
                    cmdExport.Enabled = true;
                    cmdGenerate.Enabled = false;
                }));
                return path;
            }
            else
            {
                throw new NotImplementedException("Selected bolt type is not yet supported");
            }
        }

        private async void cmdGenerate_Click(object sender, EventArgs e)
        {
            if (mCancelViaUI == null)
            {
                try
                {
                    await RequestPath(CancellationToken.None);
                }
                catch (TaskCanceledException)
                {
                    // Do nothing
                }
            }
            else
            {
                mCancelViaUI.Cancel();
            }
        }

        private void dbmBoltControl1_BreakdownPropagationProgress(object sender, DBMBreakdown.GroundPropagationProgressEventArgs e)
        {
            BreakdownPropagationProgress?.Invoke(this, e);
        }

        private void dbmBoltControl1_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            cmdGenerate.Enabled = e.Valid;
            if (tcConfig.SelectedTab == tpDBM)
            {
                ValidityChanged?.Invoke(this, e);
            }
        }

        private void dbmBoltControl1_ValueChanged(object sender, EventArgs e)
        {
            if (mCache.ContainsKey(tcConfig.SelectedTab))
            {
                mCache.Remove(tcConfig.SelectedTab);
                CacheChanged?.Invoke(this, new ValidityChangedEventArgs(false));
            }
        }

        public JsonObject JsonRepresentation
        {
            get
            {
                CachedBolt cache;
                if (!mCache.TryGetValue(tpDBM, out cache)) cache = new CachedBolt();
                return new JsonObject(new Dictionary<string, JsonObject>()
                {
                    {"BoltType", new JsonObject("DBMBolt") },
                    {"Bolt", Serialization.Translator.MakeJson(cache.Bolt as DBMBolt) },
                    {"Path", Serialization.Translator.MakeJson(cache.Path) }
                });
            }
            set
            {
                if (value != null && value.ObjectType == JsonObject.Type.Dictionary)
                {
                    if (value.Dictionary["BoltType"].String == "DBMBolt")
                    {
                        var bolt = Serialization.Translator.MakeObject<DBMBolt>(value.Dictionary["Bolt"]);
                        var path = Serialization.Translator.MakeObject<Vector3[]>(value.Dictionary["Path"]);
                        dbmBoltControl1.SetBolt(bolt);
                        mCache[tpDBM] = new CachedBolt() { Bolt = bolt, Path = path };
                        cmdExport.Enabled = true;
                        CacheChanged?.Invoke(this, new ValidityChangedEventArgs(true));
                        PathGenerationProgress?.Invoke(this, new DBMBolt.PathGenerationProgressEventArgs(path.Length - 1, path.Length - 1, true));
                        tcConfig.SelectedTab = tpDBM;
                    }
                    else
                    {
                        throw new ArgumentException("Bolt type is not yet supported");
                    }
                }
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            if (sfdBolt.ShowDialog(this) == DialogResult.OK)
            {
                if (tcConfig.SelectedTab == tpDBM)
                {
                    var fi = new FileInfo(sfdBolt.FileName);
                    using (var w = new StreamWriter(fi.FullName))
                    {
                        w.Write(JsonRepresentation.ToMultilineString());
                    }
                    sfdBolt.InitialDirectory = fi.DirectoryName;
                    ofdBolt.InitialDirectory = fi.DirectoryName;
                }
                else
                {
                    MessageBox.Show(this, "Selected bolt type is not yet supported");
                }
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            if (ofdBolt.ShowDialog(this) == DialogResult.OK)
            {
                var fi = new FileInfo(ofdBolt.FileName);
                try
                {
                    JsonRepresentation = JsonObject.Parse(File.ReadAllText(fi.FullName));
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(this, "Bolt type in file is not yet supported");
                    return;
                }
                sfdBolt.InitialDirectory = fi.DirectoryName;
                ofdBolt.InitialDirectory = fi.DirectoryName;
            }
        }

        private void tcConfig_TabIndexChanged(object sender, EventArgs e)
        {
            bool valid = mCache.ContainsKey(tcConfig.SelectedTab) && mCache[tcConfig.SelectedTab].Path != null;
            cmdExport.Enabled = valid;
            CacheChanged?.Invoke(this, new ValidityChangedEventArgs(valid));
        }
    }
}

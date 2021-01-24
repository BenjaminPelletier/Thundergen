using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thundergen.Thunder;
using System.IO;

namespace Thundergen.UI
{
    public partial class ThunderGeneratorConfigControl : UserControl
    {
        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;

        public bool Valid { get; private set; } = false;

        public ThunderGeneratorConfigControl()
        {
            InitializeComponent();

            txtObserver.SetValidator(Parsing.Vector3);
            txtFollowingStrokes.SetValidator(FollowingStroke.GroupFromString);
            txtInitialVolume.SetValidator(Parsing.Double(0, false, 200, false));
            txtInitialDistance.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));
            txtSamplesPerStep.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));

            Valid = true;
        }

        private void ComputeValidity()
        {
            bool old = Valid;
            Valid = true;
            foreach (Control c in this.Controls)
            {
                ValidatingTextBox txt = c as ValidatingTextBox;
                if (txt != null && !txt.Valid)
                {
                    Valid = false;
                    break;
                }
            }

            if (Valid != old)
            {
                ValidityChanged?.Invoke(this, new ValidityChangedEventArgs(Valid));
            }
        }

        public Generator.Config Config
        {
            get
            {
                if (Valid)
                {
                    var result = new Generator.Config();
                    result.Conditions = atmosphericConditions1.Conditions;
                    result.FollowingStrokes = FollowingStroke.GroupFromString(txtFollowingStrokes.Text).ToArray();
                    result.InitialVolume = double.Parse(txtInitialVolume.Text);
                    result.MaximumSteepeningIndexShift = chkSteepen.Checked ? double.Parse(txtSamplesPerStep.Text) : 0;
                    result.NoAtmosphericAttenuation = !chkAttenuation.Checked;
                    result.ObserverLocation = Parsing.Vector3(txtObserver.Text).Value;
                    result.R0 = double.Parse(txtInitialDistance.Text);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                atmosphericConditions1.Conditions = value.Conditions;
                txtFollowingStrokes.Text = value.FollowingStrokes.AsString();
                txtInitialVolume.Text = value.InitialVolume.ToString();
                txtSamplesPerStep.Text = value.MaximumSteepeningIndexShift.ToString();
                chkAttenuation.Checked = !value.NoAtmosphericAttenuation;
                txtObserver.Text = value.ObserverLocation.AsString();
                txtInitialDistance.Text = value.R0.ToString();
            }
        }

        private void cmdRandomFollowingStrokes_Click(object sender, EventArgs e)
        {
            FollowingStroke[] followingStrokes = FollowingStroke
                .GenerateSet(new Random())
                .Select(fs => new FollowingStroke(Math.Round(fs.Interval, 4), Math.Round(fs.RelativeAmplitude, 3)))
                .ToArray();
            txtFollowingStrokes.Text = followingStrokes.AsString();
        }

        private void cmdClearFollowingStrokes_Click(object sender, EventArgs e)
        {
            txtFollowingStrokes.Text = "";
        }

        private void Input_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            ComputeValidity();
        }
    }
}

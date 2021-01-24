using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thundergen.Lightning;

namespace Thundergen.UI
{
    public partial class DBMBoltInterpolationConfigControl : UserControl
    {
        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;

        public bool Valid { get; private set; } = false;

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

        public DBMBoltInterpolationConfigControl()
        {
            InitializeComponent();

            txtScale.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));
            txtSegmentLength.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));
            txtInitialSmoothing.SetValidator(Parsing.Int(1, false, int.MaxValue, false));
            txtEnvelopeExtent.SetValidator(Parsing.Int(0, true, int.MaxValue, false));

            Valid = true;
        }

        public DBMBolt.InterpolationConfiguration Config
        {
            get
            {
                if (Valid)
                {
                    return new DBMBolt.InterpolationConfiguration(
                        scale: double.Parse(txtScale.Text),
                        initialSmoothing: int.Parse(txtInitialSmoothing.Text),
                        envelopeExtent: int.Parse(txtEnvelopeExtent.Text),
                        interpolatedSegmentLength: float.Parse(txtSegmentLength.Text)
                    );
                }
                else
                {
                    return null;
                }
            }
        }

        private void txtInput_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            ComputeValidity();
        }
    }
}

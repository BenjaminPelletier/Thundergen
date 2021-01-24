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
using System.Numerics;

namespace Thundergen.UI
{
    public partial class BreakdownConfigControl : UserControl
    {
        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;
        public event EventHandler ValueChanged;

        public BreakdownConfigControl()
        {
            InitializeComponent();

            txtInitialBreakdown.SetValidator(Parsing.Vector3);
            txtBiasHeight.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));
            txtBiasStrength.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));
            txtRandomSeed.SetValidator(Parsing.Int);
            txtGrowthPerIteration.SetValidator(Parsing.Int(1, false, int.MaxValue, false));
            txtEta.SetValidator(Parsing.Double(0, true, double.PositiveInfinity, true));
            txtCullThreshold.SetValidator(Parsing.Int(1, false, int.MaxValue, false));
            txtCullLevel.SetValidator(Parsing.Int(1, false, int.MaxValue, false));
            txtFractionToCullByCharge.SetValidator(Parsing.Double(0, false, 100, false));

            Valid = true;
        }

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

            if (Valid)
            {
                Vector3 initialBreakdown = Parsing.Vector3(txtInitialBreakdown.Text).Value;
                double biasHeight = double.Parse(txtBiasHeight.Text);
                if (biasHeight <= initialBreakdown.Z) Valid = false;
                int cullThreshold = int.Parse(txtCullThreshold.Text);
                int cullLevel = int.Parse(txtCullLevel.Text);
                if (cullLevel > cullThreshold) Valid = false;
            }

            if (Valid != old)
            {
                ValidityChanged?.Invoke(this, new ValidityChangedEventArgs(Valid));
            }
        }

        public DBMBreakdown.Configuration Config
        {
            get
            {
                ComputeValidity();
                if (Valid)
                {
                    return new DBMBreakdown.Configuration(
                        new Vector3[] { Parsing.Vector3(txtInitialBreakdown.Text).Value },
                        new DBMBreakdown.Bias[] { new DBMBreakdown.Bias(double.Parse(txtBiasHeight.Text), double.Parse(txtBiasStrength.Text)) },
                        double.Parse(txtEta.Text),
                        int.Parse(txtRandomSeed.Text),
                        int.Parse(txtGrowthPerIteration.Text),
                        int.Parse(txtCullThreshold.Text),
                        int.Parse(txtCullLevel.Text),
                        double.Parse(txtFractionToCullByCharge.Text)
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
            
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            ComputeValidity();
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

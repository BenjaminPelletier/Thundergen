using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thundergen.UI
{
    public partial class AtmosphericConditionsControl : UserControl
    {
        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;
        public event EventHandler ValueChanged;

        public bool Valid { get; private set; } = false;

        public Thunder.AtmosphericConditions Conditions
        {
            get
            {
                return Valid ? new Thunder.AtmosphericConditions(double.Parse(txtTemperature.Text) + 273.15, double.Parse(txtRelativeHumidity.Text), double.Parse(txtPressure.Text) * 100) : null;
            }
            set
            {
                if (value != null)
                {
                    txtTemperature.Text = (value.Temperature - 273.15).ToString();
                    txtRelativeHumidity.Text = value.RelativeHumidity.ToString();
                    txtPressure.Text = (value.Pressure / 100).ToString();
                }
            }
        }

        public AtmosphericConditionsControl()
        {
            InitializeComponent();

            txtTemperature.SetValidator(Parsing.Double(-273, false, 1000, false));
            txtPressure.SetValidator(Parsing.Double(0, true, 2000, false));
            txtRelativeHumidity.SetValidator(Parsing.Double(0, false, 100, false));

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

        private void txtInput_ValidityChanged(object sender, ValidityChangedEventArgs e)
        {
            ComputeValidity();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

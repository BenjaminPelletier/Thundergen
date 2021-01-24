using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thundergen.UI
{
    public class ValidatingTextBox : TextBox
    {
        public event EventHandler<ValidityChangedEventArgs> ValidityChanged;

        static readonly Color RED = Color.FromArgb(0xff, 0xbb, 0xbb);

        public delegate bool Validate(string s);

        private Validate mValidator;

        public bool Valid { get; private set; } = true;

        public ValidatingTextBox()
        {
            base.TextChanged += Base_TextChanged;
        }

        public void SetValidator<T>(Func<string, T> validator)
        {
            if (validator != null)
            {
                mValidator = s => validator(s) != null;
            }
            else
            {
                mValidator = null;
            }
        }

        private void Base_TextChanged(object sender, EventArgs e)
        {
            bool old = Valid;
            var d = mValidator;
            Valid = d != null ? d(this.Text) : true;
            this.BackColor = Valid ? Color.White : RED;
            if (old != Valid) ValidityChanged?.Invoke(this, new ValidityChangedEventArgs(Valid));
        }
    }
}

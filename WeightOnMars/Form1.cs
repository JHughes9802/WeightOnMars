using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeightOnMars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateName(txtObject.Text, out string objectName, out string nameErrorMessage))
            {
                MessageBox.Show(nameErrorMessage, "Object Name Error");
                txtObject.Focus();
                return;
            }

            if (!ValidatePositiveDouble(txtWeightEarth.Text, out double earthWeight, out string weightErrorMessage))
            {
                MessageBox.Show(weightErrorMessage, "Earth Weight Error");
                txtWeightEarth.Focus();
                return;
            }

            double conversionFactor = 0.377;
            double marsWeight = earthWeight * conversionFactor;
            txtWeightMars.Text = String.Format("{0} weighs {1} on Mars", objectName, marsWeight);
        }
        private bool ValidatePositiveDouble(string text, out double number, out string errorMessage)
        {
            errorMessage = null;
            number = 0;

            /* I added this because it's improper to tell the user to enter *only* numbers
             * when they haven't entered anything yet */
            if (String.IsNullOrWhiteSpace(text))
            {
                errorMessage = "Please enter a number";
                return false;
            }
            try
            {
                number = double.Parse(text);

                if (number >= 0)
                {
                    return true;
                }
                else
                {
                    errorMessage = "Please enter a positive number";
                    return false;
                }
            }
            catch (FormatException)
            {
                errorMessage = "Please enter only numbers";
                return false;
            }
            catch (OverflowException)
            {
                errorMessage = "Please enter a smaller number";
                return false;
            }
        }

        private bool ValidateName(string text, out string name, out string errorMessage)
        {
            /* I added the Trim method because the user could add
             * white space, which would make the output look bad */
            errorMessage = null;
            name = text.Trim();

            /* I substituted name for text in both if statements because the user could
             * add white space to work around the length requirement. But if there were
             * no length requirement, I could've used IsNullOrWhiteSpace instead */
            if (String.IsNullOrEmpty(name))
            {
                errorMessage = "Please enter a name";
                return false;
            }

            if (name.Length < 2)
            {
                errorMessage = "Please enter at least two characters";
                return false;
            }

            return true;
        }
    }
}

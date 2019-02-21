using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleCalc
{
    public partial class MainPage : ContentPage
    {
        Calc calc;

        bool resultFont = false;

        public MainPage()
        {
            InitializeComponent();
            calc = new Calc();
        }

        /// <summary>
        /// Handler for clicking on the numbers.
        /// </summary>
        private void NumbersButtonClick(object sender, System.EventArgs e)
        {
            if (resultFont)
                ReturnOriginalFonts();

            Button button = (Button)sender;

            if (firstOperandLabel.Text != "0")
            {
                if (firstOperandLabel.Text.Length != 12)
                {
                    firstOperandLabel.Text += button.Text.ToString();                  
                }
            }
            else
                firstOperandLabel.Text = button.Text.ToString();

            resultLabel.Text = calc.CalcString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
        }

        /// <summary>
        /// Handler for clicking on the clear button.
        /// </summary>
        private void C_ButtonClicked(object sender, System.EventArgs e)
        {
            if (resultFont)
                ReturnOriginalFonts();

            if (firstOperandLabel.Text != "0")
            {
                firstOperandLabel.Text = "0";
            }
            else
            {
                secondOperandLabel.Text = "";
                operatorLabel.Text = "";
                calc.ResetResult();
            }

            resultLabel.Text = calc.CalcString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
        }

        /// <summary>
        /// Handler for clicking on operators.
        /// </summary>
        private void OperatorButtonClicked(object sender, System.EventArgs e)
        {
            if (resultFont)
                return;
               
            Button button = (Button)sender;
            operatorLabel.Text = button.Text;

            if(firstOperandLabel.Text.Length > 0)
            secondOperandLabel.Text = calc.Result.ToString();

            firstOperandLabel.Text = "";

            resultLabel.Text = calc.CalcString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
        }

        /// <summary>
        /// Handler for clicking on the Backspace (button D).
        /// </summary>
        private void DelButtonClicked(object sender, System.EventArgs e)
        {
            if (resultFont)
                ReturnOriginalFonts();

            if (firstOperandLabel.Text.Length > 0)
            {
               firstOperandLabel.Text = firstOperandLabel.Text.Remove(firstOperandLabel.Text.Length - 1);
            }
            resultLabel.Text = calc.CalcString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
        }

        /// <summary>
        /// Handler for clicking on a comma.
        /// </summary>
        private void CommaClicked(object sender, System.EventArgs e)
        {
            if (resultFont)
                ReturnOriginalFonts();

            if (firstOperandLabel.Text.Contains(','))
                return;

            firstOperandLabel.Text += ',';
            resultLabel.Text = calc.CalcString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
        }

        /// <summary>
        /// Handler for clicking on interest.
        /// </summary>
        private void PercentClicked(object sender, System.EventArgs e)
        {
            if (resultFont)
                ReturnOriginalFonts();

            if (secondOperandLabel.Text.Length < 1)
            {
                firstOperandLabel.Text = calc.CalcPercentString(firstOperandLabel.Text);
            }
            else
            {
                firstOperandLabel.Text = calc.CalcPercentString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
            }

            resultLabel.Text = calc.CalcString(firstOperandLabel.Text, secondOperandLabel.Text, operatorLabel.Text);
        }

        /// <summary>
        /// Handler for clicking on equal.
        /// </summary>
        private void EqualClicked(object sender, System.EventArgs e)
        {
            if (firstOperandLabel.Text.Length > 0)
                secondOperandLabel.Text = calc.Result.ToString();

            firstOperandLabel.Text = "";
            secondOperandLabel.Text = "";
            operatorLabel.Text = "";

            if (!resultFont)
                ChangeFonts();
        }

        /// <summary>
        /// Increases the font size for resultLabel.
        /// </summary>
        private void ChangeFonts()
        {
            row1.Height = new GridLength(1, GridUnitType.Star);
            row2.Height = new GridLength(2, GridUnitType.Star);
            row3.Height = new GridLength(1, GridUnitType.Star);
            row4.Height = new GridLength(2, GridUnitType.Star);
            resultLabel.FontSize = 50;
            firstOperandLabel.FontSize = 25;
            secondOperandLabel.FontSize = 25;

            resultFont = true;
        }

        /// <summary>
        /// Returns the original font sizes.
        /// </summary>
        private void ReturnOriginalFonts()
        {
            row1.Height = new GridLength(2, GridUnitType.Star);
            row2.Height = new GridLength(1, GridUnitType.Star);
            row3.Height = new GridLength(2, GridUnitType.Star);
            row4.Height = new GridLength(1, GridUnitType.Star);
            resultLabel.FontSize = 25;
            firstOperandLabel.FontSize = 50;
            secondOperandLabel.FontSize = 50;

            resultFont = false;
        }
    }
}

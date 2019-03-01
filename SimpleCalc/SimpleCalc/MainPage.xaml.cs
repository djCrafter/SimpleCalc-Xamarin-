using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace SimpleCalc
{
    public partial class MainPage : ContentPage
    {
        int currentState;
        double firstNumber, secondNumber, resultNumber;
        bool dotInLine = false;
        bool pressedDot = false;
        bool resultFont = false;

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handler for clicking on the numbers.
        /// </summary>
        private void NumbersButtonClick(object sender, System.EventArgs e)
        {
            if (currentState == -1)
                C_ButtonClicked(this, null);

            Button button = (Button)sender;

            string str = firstNumber.ToString();

            if (pressedDot)
            {
                str += ',' + button.Text;
                pressedDot = false;
            }
            else
            {
                str += button.Text;
            }

            double temp;
            if (double.TryParse(str, out temp))
            {
                if(!IsFifteenDigits(temp))
                firstNumber = temp;
            }

            firstOperandLabel.Text = firstNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US"));

            UpdateResult();
        }

        /// <summary>
        /// Handler for clicking on the clear button.
        /// </summary>
        private void C_ButtonClicked(object sender, System.EventArgs e)
        {
            currentState = 0;
            firstNumber = 0;
            secondNumber = 0;
            resultNumber = 0;

            firstOperandLabel.Text = "0";
            secondOperandLabel.Text = string.Empty;
            operatorLabel.Text = string.Empty;
            resultLabel.Text = string.Empty;
            dotInLine = false;
            pressedDot = false;

            if (resultFont)
                ReturnOriginalFonts();
        }

        /// <summary>
        /// Handler for clicking on operators.
        /// </summary>
        private void OperatorButtonClicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            operatorLabel.Text = button.Text;

            if (currentState == -1)
            {
                ReturnOriginalFonts();
                currentState = 1;
            }
            if (currentState == 0)
            {
                secondNumber = firstNumber;
                secondOperandLabel.Text = secondNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US"));
                currentState = 1;
            }
            else if (currentState == 1)
            {
                secondNumber = resultNumber;
                secondOperandLabel.Text = resultNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US"));
            }

            firstOperandLabel.Text = string.Empty;
            firstNumber = 0;
            dotInLine = false;

            UpdateResult();
        }

        /// <summary>
        /// Handler for clicking on the Backspace (button D).
        /// </summary>
        private void DelButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                if (firstOperandLabel.Text == "0" || firstOperandLabel.Text == string.Empty)
                    return;

                if (firstOperandLabel.Text.Length == 1)
                {
                    firstNumber = 0;
                    firstOperandLabel.Text = "0";
                    return;
                }

                if (firstOperandLabel.Text[firstOperandLabel.Text.Length - 1] == '.')
                {
                    firstOperandLabel.Text = firstOperandLabel.Text.Remove(firstOperandLabel.Text.Length - 1);
                    dotInLine = false;
                    return;
                }

                string str = firstNumber.ToString();
                str = str.Remove(str.Length - 1);
                firstNumber = double.Parse(str);

                if (str[str.Length - 1] == ',')
                    firstOperandLabel.Text = firstNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US")) + '.';
                else
                    firstOperandLabel.Text = firstNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US"));
            }
            finally
            {
                UpdateResult();
            }
        }

        /// <summary>
        /// Handler for clicking on a dot.
        /// </summary>
        private void Dot_Clicked(object sender, System.EventArgs e)
        {
            if (!dotInLine)
            {
                string str = firstNumber.ToString();
                firstOperandLabel.Text += '.';

                pressedDot = true;
                dotInLine = true;
            }
        }

        /// <summary>
        /// Handler for clicking on interest.
        /// </summary>
        private void PercentClicked(object sender, System.EventArgs e)
        {
            if (resultFont)
                ReturnOriginalFonts();

            if(currentState == 0)
            {
                firstNumber = Calc.Percent(firstNumber);
                
            }
            else if(currentState == 1)
            {
                firstNumber = Calc.Percent(firstNumber, secondNumber);
            }

            firstOperandLabel.Text = firstNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US"));

            UpdateResult();
        }

        private void PrintResult(string result)
        {
            resultLabel.Text = "= " + result;
        }

        /// <summary>
        /// Handler for clicking on equal.
        /// </summary>
        private void EqualClicked(object sender, System.EventArgs e)
        {
            if (!resultFont)
            {
                currentState = -1;
                ChangeFonts();
            }
        }

        /// <summary>
        /// Increases the font size for resultLabel.
        /// </summary>
        private void ChangeFonts()
        {     
            row4.Height = new GridLength(2, GridUnitType.Star);
            resultLabel.FontSize = 50;
            firstOperandLabel.FontSize = 25;   
            resultFont = true;
        }

        /// <summary>
        /// Returns the original font sizes.
        /// </summary>
        private void ReturnOriginalFonts()
        {         
            row4.Height = new GridLength(1, GridUnitType.Star);
            resultLabel.FontSize = 25;
            firstOperandLabel.FontSize = 50;         
            resultFont = false;
        }

       
        /// <summary>
        /// Updates the result on the calculator screen.
        /// </summary>
        private void UpdateResult()
        {
            try
            {
                if (currentState == 0)
                    resultNumber = firstNumber;

                else if (currentState == 1 && firstOperandLabel.Text != string.Empty)
                    resultNumber = Calc.Calculate(secondNumber, firstNumber, operatorLabel.Text);

                if (IsFifteenDigits(resultNumber))
                    resultLabel.Text = "= " + resultNumber.ToString();
                else
                    resultLabel.Text = "= " + resultNumber.ToString("#,0.##########", CultureInfo.GetCultureInfo("en-US"));
            }
            catch (DivideByZeroException ex)
            {
                resultLabel.Text = ex.Message;
            }
        }
    
        /// <summary>
        /// Check for exceeding fifteen digits of the number.
        /// </summary>
        private bool IsFifteenDigits(double number)
        {
            return ((int)Math.Log10(number)) + 1 > 15 ? true : false;
        }

        /// <summary>
        /// Changes font size for correct number display in label.
        /// </summary>
        private void FirstOperandLabel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (firstOperandLabel.Text.Length > 10)
                firstOperandLabel.FontSize = 25;
            else
            {
                if(currentState != -1)
                firstOperandLabel.FontSize = 50;
            }
        }

        /// <summary>
        /// Changes font size for correct number display in label.
        /// </summary>  
        private void ResultLabel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (currentState == -1)
            {
                if (resultLabel.Text.Length > 10)
                    resultLabel.FontSize = 35;
                else
                    resultLabel.FontSize = 50;
            }
        }
    }
}

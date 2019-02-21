using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace SimpleCalc
{
    /// <summary>
    /// The class performs calculations and returns string values.
    /// </summary>
    public class Calc
    {
        /// <summary>
        /// This property stores the result of calculations.
        /// </summary>
        public double Result { get; private set; }

        /// <summary>
        /// The method returns a string representation of the calculations.
        /// </summary>     
        public string CalcString(string firstOperand, string secondOperand, string _operator)
        {
            double a = 0, b = 0;

            if (double.TryParse(firstOperand, out a) && double.TryParse(secondOperand, out b))
            {
                if (_operator != "")
                    return "= " + Calculate(b, a, _operator).ToString();
                else
                    return string.Empty;
            }
            else if (secondOperand.Length < 1)
            {
                double.TryParse(firstOperand, out a);
                Result = a;
                return "= " + firstOperand;
            }
            else
            {
                double.TryParse(firstOperand, out b);
                Result = b;
                return "= " + secondOperand;
            }
        }

        public void ResetResult()
        {
            Result = 0;
        }
        
        private double Calculate(double a, double b, string op)
        {          
            switch (op)
            {
                case "+": Result = a + b; break;
                case "-": Result = a - b; break;
                case "X": Result = a * b; break;
                case "/": Result = a / b; break;              
            }
            return Result;
        }

        /// <summary>
        /// Returns a string representation of the percent of the number.
        /// </summary>
        public string CalcPercentString(string operand)
        {
            double a;

            if (double.TryParse(operand, out a))
                return Calculate(a, 100, "/").ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns a string representation of the percent of a number for the second operand.
        /// </summary>
        public string CalcPercentString(string firstOperand, string secondOperand, string _operator)
        {
            double a;
            double b;

            if (double.TryParse(firstOperand, out a) && double.TryParse(secondOperand, out b))
            {
                b /= 100;
                return Calculate(b, a, "X").ToString();
            }
            return string.Empty;
        }
    }
}


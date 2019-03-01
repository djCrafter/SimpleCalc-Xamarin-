using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCalc
{ 
    public static class Calc
    {
        public static double Calculate(double firstOperand, double secondOperand, string _operator)
        {
            switch (_operator)
            {
                case "+": return firstOperand + secondOperand;
                case "-": return firstOperand - secondOperand;
                case "X": return firstOperand * secondOperand;
                case "/":
                    if (secondOperand == 0)
                        throw new DivideByZeroException("Divide by Zero!!!");
                    return firstOperand / secondOperand;
                default:
                    throw new ArgumentException("Wrong operator!");
            }
        }

        public static double Percent(double a)
        {
            return a / 100;
        }

        public static double Percent(double a, double b)
        {
            b /= 100;
            return b * a;
        }
    }
}

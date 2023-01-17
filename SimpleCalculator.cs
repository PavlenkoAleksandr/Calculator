using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SimpleCalculator : BaseCalculator
    {
        private string firstNumber;
        private string secondNumber;
        private string operation;
        private double numberOne;
        private double numberTwo;
        private double result;

        UserInput userInput = new UserInput();

        public SimpleCalculator(string name) : base(name)
        {
        }

        public override void GettingInput()
        {
            Console.WriteLine("Введите первое число");
            firstNumber = userInput.GetUserInput(TypeOfUserInput.number);

            Console.Clear();
            Console.WriteLine("Введите требуемую операцию:");
            Console.WriteLine(@"""+"" - сложение");
            Console.WriteLine(@"""-"" - вычитание");
            Console.WriteLine(@"""/"" - деление");
            Console.WriteLine(@"""*"" - умножение");
            Console.WriteLine(@"""%"" - сколько процентов составляет первое число от второго)");

            operation = userInput.GetUserInput(TypeOfUserInput.operation);

            Console.Clear();
            Console.WriteLine("Введите второе число");
            secondNumber = userInput.GetUserInput(TypeOfUserInput.number);
            Console.Clear();
        }

        public override void Calculation()
        {
            int hundredPercent = 100;

            UniversalDecimalSeparator();

            if (operation == "+")
            {
                result = numberOne + numberTwo;
                Console.WriteLine($"{numberOne} + {numberTwo} = {FormattoString(result)}");
            }
            else if (operation == "-")
            {
                result = numberOne - numberTwo;
                Console.WriteLine($"{numberOne} - {numberTwo} = {FormattoString(result)}");
            }
            else if (operation == "/")
            {
                result = numberOne / numberTwo;
                Console.WriteLine($"{numberOne} / {numberTwo} = {FormattoString(result)}");
            }
            else if (operation == "*")
            {
                result = numberOne * numberTwo;
                Console.WriteLine($"{numberOne} * {numberTwo} = {FormattoString(result)}");
            }
            else if (operation == "%")
            {
                result = numberOne / numberTwo * hundredPercent;
                Console.WriteLine($"{numberOne} составляет {FormattoString(result)}% от числа {numberTwo}");
            }

            Console.WriteLine("Нажмите любую кнопку, чтобы продолжить");
            Console.ReadKey();
        }

        NumberFormatInfo DotDecimalSeparator = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        NumberFormatInfo CommaDecimalSeparator = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ","
        };

        private void UniversalDecimalSeparator()
        {
            if (firstNumber.Contains("."))
            {
                numberOne = Convert.ToDouble(firstNumber, DotDecimalSeparator);
            }
            else if (firstNumber.Contains(","))
            {
                numberOne = Convert.ToDouble(firstNumber, CommaDecimalSeparator);
            }
            else
            {
                numberOne = Convert.ToDouble(firstNumber, CommaDecimalSeparator);
            }

            if (secondNumber.Contains("."))
            {
                numberTwo = Convert.ToDouble(secondNumber, DotDecimalSeparator);
            }
            else if (firstNumber.Contains(","))
            {
                numberTwo = Convert.ToDouble(secondNumber, CommaDecimalSeparator);
            }
            else
            {
                numberTwo = Convert.ToDouble(secondNumber, CommaDecimalSeparator);
            }
        }

        private string FormattoString(double value)
        {
            return String.Format("{0:f2}", value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SimpleCalculator
    {
        private string firstNumber;
        private string secondNumber;
        private string operation;
        private double numberOne;
        private double numberTwo;
        private double result;

        NumberFormatInfo DotDecimalSeparator = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        NumberFormatInfo CommaDecimalSeparator = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ","
        };

        UserInput userInput = new UserInput();

        public void Show()
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

            Calculation();
        }

        private void Calculation()
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

            Decision();
        }

        private void Decision()
        {
            string decision;

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Для повторного подсчёта введите \"calculate again\"\nДля закрытия калькулятора введите \"exit\"\nДля выхода в главное меню введите \"return\"");
            decision = userInput.GetUserInput(TypeOfUserInput.command);

            if (decision == "calculate again")
            {
                Console.Clear();
                Show();
            }
            else if (decision == "exit")
            {
                Environment.Exit(0);
            }
            else if (decision == "return")
            {
                Console.Clear();
                MainMenu mainMenu = new MainMenu();
                mainMenu.MenuSelection();
            }
        }

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

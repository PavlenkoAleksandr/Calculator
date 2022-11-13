using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class UserInput
    {
        //разобраться с геттерами и сеттерами
        private string checkedInput;
        private string currentInput;

        public string GetUserInput(bool showWarning = true)
        {
            currentInput = Console.ReadLine();

            if (currentInput == null && showWarning)
            {
                ShowWarning();
                GetUserInput(showWarning);
            }
            else if (currentInput == null)
            {
                Console.WriteLine("Пустой ввод!");
                GetUserInput(showWarning);
            }
            else if (currentInput != null)
            {
                checkedInput = currentInput;
            }

            return checkedInput;
        }

        public string GetUserInput(Enum.TypeOfUserInput type, bool showWarning = true)
        {
            currentInput = Console.ReadLine();

            if (type == Enum.TypeOfUserInput.command)
            {
                if ((currentInput == "exit") || (currentInput == "calculate again"))
                {
                    checkedInput = currentInput;
                }
                else if (showWarning)
                {
                    ShowWarning();
                    Console.WriteLine("Для повторного подсчёта налогов введите \"calculate again\"\nДля выхода из калькулятора введите \"exit\"");
                    GetUserInput(Enum.TypeOfUserInput.command, showWarning);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Неизвестная команда, проверьте ввод и попробуйте снова.");
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Для повторного подсчёта налогов введите \"calculate again\"\nДля выхода из калькулятора введите \"exit\"");
                    checkedInput = GetUserInput(Enum.TypeOfUserInput.command);
                }
            }

            if (type == Enum.TypeOfUserInput.currency)
            {
                if ((currentInput == "USD") || (currentInput == "EUR") || (currentInput == "UAH"))
                {
                    checkedInput = currentInput;
                }
                else if (showWarning)
                {
                    ShowWarning();
                    Console.WriteLine("USD - в долларах, EUR - в евро, UAH - в гривне");
                    GetUserInput(Enum.TypeOfUserInput.currency, showWarning);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод валюты, повторите ещё раз. Обратите внимание на регистр и язык ввода");
                    Console.WriteLine("USD - в долларах, EUR - в евро, UAH - в гривне");
                    checkedInput = GetUserInput(Enum.TypeOfUserInput.currency);
                }
            }

            if (type == Enum.TypeOfUserInput.money)
            {
                decimal income;
                bool isNumber = decimal.TryParse(currentInput, out income);
                bool isDot = currentInput.Contains(".");
                bool isComma = currentInput.Contains(",");
                bool isLetter = currentInput.Any(Char.IsLetter);

                if(showWarning == true)
                {
                    if (isComma && isDot)
                    {
                        ShowWarning();
                        checkedInput = GetUserInput(Enum.TypeOfUserInput.money, showWarning);
                    }
                    else if (isNumber)
                    {
                        checkedInput = currentInput;
                    }
                    else if (!isLetter && isDot)
                    {
                        int numberOfDots = currentInput.Count(x => x == '.');

                        if (numberOfDots > 1)
                        {
                            ShowWarning();
                            checkedInput = GetUserInput(Enum.TypeOfUserInput.money, showWarning);
                        }
                        else
                        {
                            checkedInput = currentInput;
                        }
                    }
                    else if (!isLetter && isComma)
                    {
                        int numberOfCommas = currentInput.Count(x => x == ',');

                        if (numberOfCommas > 1)
                        {
                            ShowWarning();
                            checkedInput = GetUserInput(Enum.TypeOfUserInput.money, showWarning);
                        }
                        else
                        {
                            checkedInput = currentInput;
                        }
                    }
                    else
                    {
                        ShowWarning();
                        checkedInput = GetUserInput(Enum.TypeOfUserInput.money, showWarning);
                    }
                }

                if(showWarning == false)
                {
                    if (isComma && isDot)
                    {
                        Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе суммы дохода. Повторите попытку");
                        checkedInput = GetUserInput(Enum.TypeOfUserInput.money);
                    }
                    else if (isNumber)
                    {
                        checkedInput = currentInput;
                    }
                    else if (!isLetter && isDot)
                    {
                        int numberOfDots = currentInput.Count(x => x == '.');

                        if (numberOfDots > 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Допущена ошибка при вводе суммы дохода. Повторите попытку");
                            checkedInput = GetUserInput(Enum.TypeOfUserInput.money);
                        }
                        else
                        {
                            checkedInput = currentInput;
                        }
                    }
                    else if (!isLetter && isComma)
                    {
                        int numberOfCommas = currentInput.Count(x => x == ',');

                        if (numberOfCommas > 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Допущена ошибка при вводе суммы дохода. Повторите попытку");
                            checkedInput = GetUserInput(Enum.TypeOfUserInput.money);
                        }
                        else
                        {
                            checkedInput = currentInput;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе суммы дохода. Повторите попытку");
                        checkedInput = GetUserInput(Enum.TypeOfUserInput.money);
                    }
                }
                
            }

            if (type == Enum.TypeOfUserInput.year)
            {
                int date;
                bool isNumber = int.TryParse(currentInput, out date);

                if (isNumber)
                {
                    checkedInput = currentInput;
                }
                else if (showWarning)
                {
                    ShowWarning();
                    checkedInput = GetUserInput(Enum.TypeOfUserInput.year, showWarning);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Допущена ошибка при вводе года рождения. Повторите попытку");
                    checkedInput = GetUserInput(Enum.TypeOfUserInput.year);
                }
            }
            return checkedInput;
        }

        private void ShowWarning()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Получен неверный ввод! Повторите попытку");
            Console.WriteLine("----------------------------------------");
        }
    }
}

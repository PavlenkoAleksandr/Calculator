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
        private string checkedInput;
        private string currentInput;

        public string GetUserInput(bool showWarning = true)
        {
            currentInput = Console.ReadLine();
            if (showWarning)
            {
                if (String.IsNullOrWhiteSpace(currentInput))
                {
                    ShowWarning();
                    checkedInput = GetUserInput(showWarning);
                }
                else
                {
                    checkedInput = currentInput;
                }  
            }
            else if (!showWarning)
            {
                if (String.IsNullOrWhiteSpace(currentInput))
                {
                    Console.Clear();
                    Console.WriteLine("Получен пустой ввод, повторите попытку.");
                    checkedInput = GetUserInput(showWarning);
                }
                else
                {
                    checkedInput = currentInput;
                }
            }
            

            return checkedInput;
        }
        
        public string GetUserInput(TypeOfUserInput type, bool showWarning = true)
        {
            currentInput = GetUserInput(showWarning);

            if (type == TypeOfUserInput.command)
            {
                if ((currentInput == "exit") || (currentInput == "calculate again") || (currentInput == "return"))
                {
                    checkedInput = currentInput;
                }
                else if (showWarning)
                {
                    ShowWarning();
                    checkedInput = GetUserInput(TypeOfUserInput.command, showWarning);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Неизвестная команда, проверьте ввод и попробуйте снова.");
                    Console.WriteLine("-------------------------------------------------------");
                    checkedInput = GetUserInput(TypeOfUserInput.command);
                }
            }

            if (type == TypeOfUserInput.currency)
            {
                if ((currentInput == "USD") || (currentInput == "EUR") || (currentInput == "UAH"))
                {
                    checkedInput = currentInput;
                }
                else if (showWarning)
                {
                    ShowWarning();
                    Console.WriteLine("USD - в долларах, EUR - в евро, UAH - в гривне");
                    GetUserInput(TypeOfUserInput.currency, showWarning);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод валюты, повторите ещё раз. Обратите внимание на регистр и язык ввода");
                    Console.WriteLine("USD - в долларах, EUR - в евро, UAH - в гривне");
                    checkedInput = GetUserInput(TypeOfUserInput.currency);
                }
            }

            if (type == TypeOfUserInput.money || type == TypeOfUserInput.number)
            {
                decimal income;
                bool isNumber = decimal.TryParse(currentInput, out income);
                bool isDot = currentInput.Contains(".");
                bool isComma = currentInput.Contains(",");
                bool isLetter = currentInput.Any(Char.IsLetter);

                if(showWarning)
                {
                    if (isComma && isDot)
                    {
                        ShowWarning();
                        checkedInput = GetUserInput(TypeOfUserInput.money, showWarning);
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
                            checkedInput = GetUserInput(TypeOfUserInput.money, showWarning);
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
                            checkedInput = GetUserInput(TypeOfUserInput.money, showWarning);
                        }
                        else
                        {
                            checkedInput = currentInput;
                        }
                    }
                    else
                    {
                        ShowWarning();
                        checkedInput = GetUserInput(TypeOfUserInput.money, showWarning);
                    }
                }
                else if (!showWarning)
                {
                    if (isComma && isDot)
                    {
                        Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе. Повторите попытку");
                        checkedInput = GetUserInput(TypeOfUserInput.money);
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
                            Console.WriteLine("Допущена ошибка при вводе. Повторите попытку");
                            checkedInput = GetUserInput(TypeOfUserInput.money);
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
                            Console.WriteLine("Допущена ошибка при вводе. Повторите попытку");
                            checkedInput = GetUserInput(TypeOfUserInput.money);
                        }
                        else
                        {
                            checkedInput = currentInput;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе. Повторите попытку");
                        checkedInput = GetUserInput(TypeOfUserInput.money);
                    }
                }

            }

            if (type == TypeOfUserInput.year)
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
                    checkedInput = GetUserInput(TypeOfUserInput.year, showWarning);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Допущена ошибка при вводе. Повторите попытку");
                    checkedInput = GetUserInput(TypeOfUserInput.year);
                }
            }

            if (type == TypeOfUserInput.operation)
            {
                string[] operations = new string[] {"+", "-", "/", "*", "%"};

                for (int i= 0; i < operations.Length; i++)
                {
                    if(operations[i].Contains(currentInput))
                    {
                        checkedInput = currentInput;
                        break;
                    }
                    else if (!operations[i].Contains(currentInput) && i == operations.Length - 1 && showWarning)
                    {
                        ShowWarning();
                        checkedInput = GetUserInput(TypeOfUserInput.operation);
                    }
                    else if(!operations[i].Contains(currentInput) && i == operations.Length - 1 && !showWarning)
                    {
                        checkedInput = GetUserInput(TypeOfUserInput.operation);
                    }
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

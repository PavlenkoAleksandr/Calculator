using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp1
{
    internal class Program
    {
        enum TypeOfUserInput
        {
            command,
            currency,
            money,
            year
        }

        static void Main(string[] args)
        {
            string okInput = "";
            string userInput;
            int incomeInt = 0;
            decimal singleTax = 0;
            decimal singleDeposit = 0;
            decimal profit = 0;
            decimal incomeAfterExchange = 0;
            int dateOfBirth = 0;
            string currencies;

            ageControl();

            void ageControl()
            {
                string date;
                bool isEighteen;
                int ageOfUser;
                int adultAge = 18;
                int oldestManAlive = 1904;

                Console.WriteLine("Добро пожаловать в калькулятор доходов!\nВведите свой год рождения.");
                date = GetUserInput(TypeOfUserInput.year);
                dateOfBirth = Convert.ToInt32(date);
                ageOfUser = Convert.ToInt32(DateTime.Today.Year) - dateOfBirth;

                if (ageOfUser >= adultAge && dateOfBirth >= oldestManAlive)
                {
                    isEighteen = true;
                }
                else
                {
                    isEighteen = false;
                }

                if (isEighteen)
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Совершеннолетие подтверждено. Нажмите ввод (Enter) для продолжения.");
                    Console.WriteLine("-------------------------------------------------------");
                    Console.ReadKey();
                    SelectYearOrMonth();
                }
                else
                {
                    Console.WriteLine("Вы несовершеннолетний, дальнейшая работа калькулятора доходов ограничена. Нажмите ввод (Enter) для выхода.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            void SelectYearOrMonth()
            {
                string choice;
                Console.WriteLine("Выберите интересующий вас калькулятор\n1 - для подсчёта месячного дохода\n2 - для посчёта годового дохода");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    SingleMonthProfit();
                }
                else if (choice == "2")
                {
                    FullYearProfit();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Калькулятор не выбран, попробуйте снова");
                    SelectYearOrMonth();
                }
            }

            void FullYearProfit()
            {
                int fullYearProfit = 0;
                SelectCurrency();

                string[] month = new string[12] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
                int[] partOfYearProfit = new int[12];

                for (int n = 0; n < partOfYearProfit.Length; n++)
                {
                    string oneMonthIncome;
                    Console.Clear();
                    Console.WriteLine($"Введите ваш доход за {month[n]}");
                    oneMonthIncome = GetUserInput(TypeOfUserInput.money);
                    partOfYearProfit[n] = Convert.ToInt32(oneMonthIncome);
                    fullYearProfit += partOfYearProfit[n];
                }
                incomeInt = fullYearProfit;
                Calculation();
            }

            void SingleMonthProfit()
            {
                string fullMonthIncome;
                SelectCurrency();
                Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше (используя числовой формат записи)");
                fullMonthIncome = GetUserInput(TypeOfUserInput.money);
                incomeInt = Convert.ToInt32(fullMonthIncome);
                Calculation();
            }

            void SelectCurrency()
            {
                Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
                currencies = GetUserInput(TypeOfUserInput.currency);
            }

            void Calculation()
            {
                decimal exchangeUSD = 37.17m;
                decimal exchangeEUR = 36.01m;
                decimal singleTaxRate = 0.05m;
                decimal singleDepositRate = 0.22m;
                int minProfit = 6500;

                switch (currencies)
                {
                    case "EUR":
                        incomeAfterExchange = incomeInt * exchangeEUR;
                        break;
                    case "USD":
                        incomeAfterExchange = incomeInt * exchangeUSD;
                        break;
                    case "UAH":
                        incomeAfterExchange = incomeInt;
                        break;
                }

                singleTax = incomeAfterExchange * singleTaxRate;
                singleDeposit = minProfit * singleDepositRate;
                profit = incomeAfterExchange - singleTax - singleDeposit;
                ShowResult();
            }

            void ShowResult()
            {
                string decision;

                Console.WriteLine($"Сумма вашего дохода составляет {FormattoString(incomeAfterExchange)} грн");
                Console.WriteLine($"Единый налог составит {FormattoString(singleTax)} грн");
                Console.WriteLine($"Единый социальный вклад составит {FormattoString(singleDeposit)} грн");
                Console.WriteLine($"Ваш доход за вычетом налогов составит {FormattoString(profit)} грн");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Для повторного подсчёта налогов введите \"calculate again\"\nДля выхода из калькулятора введите \"exit\"");
                decision = GetUserInput(TypeOfUserInput.command);

                if (decision == "calculate again")
                {
                    Console.Clear();
                    ageControl();
                }
                else if (decision == "exit")
                {
                    Environment.Exit(0);
                }
            }

            string GetUserInput(TypeOfUserInput type)
            {
                if (type == TypeOfUserInput.command)
                {
                    userInput = Console.ReadLine();

                    if ((userInput == "exit") || (userInput == "calculate again"))
                    {
                        okInput = userInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("Неизвестная команда, проверьте ввод и попробуйте снова.");
                        Console.WriteLine("-------------------------------------------------------");
                        GetUserInput(TypeOfUserInput.command);
                    }
                }
                else if (type == TypeOfUserInput.currency)
                {
                    userInput = Console.ReadLine();

                    if ((userInput == "USD") || (userInput == "EUR") || (userInput == "UAH"))
                    {
                        okInput = userInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Некорректный ввод валюты, повторите ещё раз. Обратите внимание на регистр и язык ввода");
                        Console.WriteLine("USD - в долларах, EUR - в евро, UAH - в гривне");
                        GetUserInput(TypeOfUserInput.currency);
                    }
                }
                else if (type == TypeOfUserInput.money)
                {
                    userInput = Console.ReadLine();
                    bool isNumber = int.TryParse(userInput, out incomeInt);

                    if (isNumber == true)
                    {
                        okInput = userInput;
                    }
                    else
                    {
                        //Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе суммы дохода. Повторите попытку");
                        GetUserInput(TypeOfUserInput.money);
                    }
                }
                else if (type == TypeOfUserInput.year)
                {
                    userInput = Console.ReadLine();
                    bool isNumber = int.TryParse(userInput, out dateOfBirth);

                    if (isNumber == true)
                    {
                        okInput = userInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе года рождения. Повторите попытку");
                        GetUserInput(TypeOfUserInput.year);
                    }
                }
                return okInput;
            }

            string FormattoString(decimal value)
            {
                return String.Format("{0:f2}", value);
            }
        }
    }
}
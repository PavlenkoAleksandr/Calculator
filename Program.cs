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

            int incomeInt = 0;
            decimal singleTax = 0;
            decimal singleDeposit = 0;
            decimal profit = 0;
            decimal incomeAfterExchange = 0;
            int dateOfBirth;
            string currencies;

            ageControl();

            void ageControl()
            {
                string date;
                bool isEighteen;
                int ageOfUser;
                const int adultAge = 18;
                const int oldestManAlive = 1904;

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
                const decimal exchangeUSD = 37.17m;
                const decimal exchangeEUR = 36.01m;
                const decimal singleTaxRate = 0.05m;
                const decimal singleDepositRate = 0.22m;
                const int minProfit = 6500;

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

                Console.WriteLine("-------------------------------------------------------");
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
                string checkedInput = "";
                string currentInput;

                currentInput = Console.ReadLine();

                if (type == TypeOfUserInput.command)
                {
                    if ((currentInput == "exit") || (currentInput == "calculate again"))
                    {
                        checkedInput = currentInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("Неизвестная команда, проверьте ввод и попробуйте снова.");
                        Console.WriteLine("-------------------------------------------------------");
                        Console.WriteLine("Для повторного подсчёта налогов введите \"calculate again\"\nДля выхода из калькулятора введите \"exit\"");
                        checkedInput = GetUserInput(TypeOfUserInput.command);
                    }
                }
                else if (type == TypeOfUserInput.currency)
                {
                    if ((currentInput == "USD") || (currentInput == "EUR") || (currentInput == "UAH"))
                    {
                        checkedInput = currentInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Некорректный ввод валюты, повторите ещё раз. Обратите внимание на регистр и язык ввода");
                        Console.WriteLine("USD - в долларах, EUR - в евро, UAH - в гривне");
                        checkedInput = GetUserInput(TypeOfUserInput.currency);
                    }
                }
                else if (type == TypeOfUserInput.money)
                {
                    bool isNumber = int.TryParse(currentInput, out incomeInt);

                    if (isNumber)
                    {
                        checkedInput = currentInput;
                    }
                    else
                    {
                        //Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе суммы дохода. Повторите попытку");
                        checkedInput = GetUserInput(TypeOfUserInput.money);
                    }
                }
                else if (type == TypeOfUserInput.year)
                {
                    bool isNumber = int.TryParse(currentInput, out dateOfBirth);

                    if (isNumber)
                    {
                        checkedInput = currentInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Допущена ошибка при вводе года рождения. Повторите попытку");
                        checkedInput = GetUserInput(TypeOfUserInput.year);
                    }
                }
                return checkedInput;
            }

            string FormattoString(decimal value)
            {
                return String.Format("{0:f2}", value);
            }
        }
    }
}
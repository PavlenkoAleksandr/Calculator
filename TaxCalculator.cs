using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class TaxCalculator
    {
        private decimal incomeDecimal = 0;
        private decimal singleTax = 0;
        private decimal singleDeposit = 0;
        private decimal profit = 0;
        private decimal incomeAfterExchange = 0;
        private int dateOfBirth;
        private string currencies;

        UserInput userInput = new UserInput();

        public void Show()
        {

            ageControl();
        }

        NumberFormatInfo DotDecimalSeparator = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        NumberFormatInfo CommaDecimalSeparator = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ","
        };

        private void ageControl()
        {
            string date;
            bool isEighteen;
            int ageOfUser;
            const int adultAge = 18;
            const int oldestManAlive = 1904;

            Console.WriteLine("Добро пожаловать в калькулятор доходов!\nВведите свой год рождения.");
            date = userInput.GetUserInput(Enum.TypeOfUserInput.year);
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
                Console.Clear();
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

        private void SelectYearOrMonth()
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

        private void FullYearProfit()
        {
            decimal fullYearProfit = 0;
            SelectCurrency();

            string[] month = new string[12] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
            decimal[] partOfYearProfit = new decimal[12];

            for (int n = 0; n < partOfYearProfit.Length; n++)
            {
                string oneMonthIncome;
                Console.Clear();
                Console.WriteLine($"Введите ваш доход за {month[n]}");
                oneMonthIncome = userInput.GetUserInput(Enum.TypeOfUserInput.money);

                if (oneMonthIncome.Contains("."))
                {
                    partOfYearProfit[n] = Convert.ToDecimal(oneMonthIncome, DotDecimalSeparator);
                }
                else if (oneMonthIncome.Contains(","))
                {
                    partOfYearProfit[n] = Convert.ToDecimal(oneMonthIncome, CommaDecimalSeparator);
                }
                else
                {
                    partOfYearProfit[n] = Convert.ToDecimal(oneMonthIncome);
                }

                fullYearProfit += partOfYearProfit[n];
            }
            incomeDecimal = fullYearProfit;
            Calculation();
        }

        private void SingleMonthProfit()
        {
            string fullMonthIncome;
            SelectCurrency();
            Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше (используя числовой формат записи)");
            fullMonthIncome = userInput.GetUserInput(Enum.TypeOfUserInput.money);
            if (fullMonthIncome.Contains("."))
            {
                incomeDecimal = Convert.ToDecimal(fullMonthIncome, DotDecimalSeparator);
            }
            else if (fullMonthIncome.Contains(","))
            {
                incomeDecimal = Convert.ToDecimal(fullMonthIncome, CommaDecimalSeparator);
            }
            else
            {
                incomeDecimal = Convert.ToDecimal(fullMonthIncome);
            }

            Calculation();
        }

        private void SelectCurrency()
        {
            Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
            currencies = userInput.GetUserInput(Enum.TypeOfUserInput.currency);
        }

        private void Calculation()
        {
            const decimal exchangeUSD = 37.17m;
            const decimal exchangeEUR = 36.01m;
            const decimal singleTaxRate = 0.05m;
            const decimal singleDepositRate = 0.22m;
            const int minProfit = 6500;

            switch (currencies)
            {
                case "EUR":
                    incomeAfterExchange = incomeDecimal * exchangeEUR;
                    break;
                case "USD":
                    incomeAfterExchange = incomeDecimal * exchangeUSD;
                    break;
                case "UAH":
                    incomeAfterExchange = incomeDecimal;
                    break;
            }

            singleTax = incomeAfterExchange * singleTaxRate;
            singleDeposit = minProfit * singleDepositRate;
            profit = incomeAfterExchange - singleTax - singleDeposit;
            ShowResult();
        }

        private void ShowResult()
        {
            string decision;

            Console.Clear();
            Console.WriteLine($"Вы ввели общую сумму {incomeDecimal} {currencies}");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"Сумма вашего дохода составляет {FormattoString(incomeAfterExchange)} грн");
            Console.WriteLine($"Единый налог:                  {FormattoString(singleTax)} грн");
            Console.WriteLine($"Единый социальный вклад:       {FormattoString(singleDeposit)} грн");
            Console.WriteLine($"Ваш доход за вычетом налогов:  {FormattoString(profit)} грн");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Для повторного подсчёта налогов введите \"calculate again\"\nДля выхода из калькулятора введите \"exit\"");
            decision = userInput.GetUserInput(Enum.TypeOfUserInput.command);

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

        string FormattoString(decimal value)
        {
            return String.Format("{0:f2}", value);
        }
    }
}

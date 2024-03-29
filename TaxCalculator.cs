﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TaxCalculator : BaseCalculator
    {
        private decimal incomeDecimal;
        private decimal singleTax;
        private decimal singleDeposit;
        private decimal profit;
        private decimal incomeAfterExchange;
        private string currencies;

        UserInput userInput = new UserInput();

        public TaxCalculator(string name) : base(name)
        {
        }

        public override void GettingInput()
        {
            string choice;

            Console.WriteLine("Введите:\n1 - для подсчёта месячного дохода\n2 - для посчёта годового дохода");
            choice = userInput.GetUserInput(TypeOfUserInput.number);

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
                Console.ReadLine();
                GettingInput();
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
                oneMonthIncome = userInput.GetUserInput(TypeOfUserInput.money);

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
        }

        private void SingleMonthProfit()
        {
            string fullMonthIncome;
            SelectCurrency();
            Console.Clear();
            Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше (используя числовой формат записи)");
            fullMonthIncome = userInput.GetUserInput(TypeOfUserInput.money);
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
        }

        private void SelectCurrency()
        {
            Console.Clear();
            Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
            currencies = userInput.GetUserInput(TypeOfUserInput.currency);
        }

        public override void Calculation()
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
            ShowResults();
        }

        private void ShowResults()
        {
            Console.Clear();
            Console.WriteLine($"Вы ввели общую сумму {incomeDecimal} {currencies}");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"Сумма вашего дохода составляет {FormattoString(incomeAfterExchange)} грн");
            Console.WriteLine($"Единый налог:                  {FormattoString(singleTax)} грн");
            Console.WriteLine($"Единый социальный вклад:       {FormattoString(singleDeposit)} грн");
            Console.WriteLine($"Ваш доход за вычетом налогов:  {FormattoString(profit)} грн");
            Console.WriteLine("-------------------------------------------------------");
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

        string FormattoString(decimal value)
        {
            return String.Format("{0:f2}", value);
        }
    }
}

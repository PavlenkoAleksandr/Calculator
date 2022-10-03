﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp1
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            //здесь почему-то ругается, если делаю его private
            private string FormattoString(decimal value)
            {
                return String.Format("{0:f2}", value);
            }

            decimal singleTaxRate = 0.05m;
            decimal singleDepositRate = 0.22m;
            int minProfit = 6500;
            string income; 
            decimal incomeInDecimal;
            decimal singleTax;
            decimal singleDeposit;
            decimal profit;
            decimal exchangeUSD = 37.17m;
            decimal exchangeEUR = 36.01m;
            string currencies;
            decimal incomeAfterExchange = 0;

            Console.WriteLine("Приветствую Вас в калькуляторе доходов!");
            Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
            currencies = Console.ReadLine();

            Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше(используя числовой формат записи)");
            income = Console.ReadLine();
            incomeInDecimal = Convert.ToDecimal(income);

            //сравниваю введённую пользователем валюту c тремя возможными вариантами

            switch (currencies)
            {
                case "EUR":
                    incomeAfterExchange = incomeInDecimal * exchangeEUR;
                    break;
                case "USD":
                    incomeAfterExchange = incomeInDecimal * exchangeUSD;
                    break;
                case "UAH":
                    incomeAfterExchange = incomeInDecimal;
                    break;
            }

            singleTax = incomeAfterExchange * singleTaxRate;
            singleDeposit = minProfit * singleDepositRate;
            profit = incomeAfterExchange - singleTax - singleDeposit;

            if (incomeAfterExchange == 0)
            {
                Console.WriteLine("Некорректный ввод данных!");
            }
            else
            {
            Console.WriteLine("Сумма вашего дохода составляет " + FormattoString(incomeAfterExchange) + "грн");
            Console.WriteLine("Единый налог составит " + FormattoString(singleTax) + "грн");
            Console.WriteLine("Единый социальный вклад составит " + FormattoString(singleDeposit) + "грн");
            Console.WriteLine("Ваш доход за вычетом налогов составит " + FormattoString(profit) + "грн");
            }
            Console.ReadKey();

        }
    }
}

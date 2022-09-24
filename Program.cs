using System;
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
            decimal singleTaxPercentage = 0.05m;
            decimal singleDepositPercentage = 0.22m;
            int minProfit = 6500;
            string profit; 
            decimal profitInDecimal;
            decimal singleTax;
            decimal singleDeposit;
            decimal profitAfterTaxes;
            decimal exchangeUSD = 37.17m;
            decimal exchangeEUR = 36.01m;
            string currencies;
            decimal profitInDecAfterExchange;

            Console.WriteLine("Приветствую Вас в калькуляторе доходов!");


            Beggining:
            Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
            currencies = Console.ReadLine();


            Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше(используя числовой формат записи)");
            profit = Console.ReadLine();
            profitInDecimal = Convert.ToDecimal(profit);


            //сравниваю введённую пользователем валюту, если допустил ошибку - возвращаю на этап ввода валюты


            if (currencies.Equals("EUR"))
            {
                profitInDecAfterExchange = profitInDecimal * exchangeEUR;
            }
            else if (currencies.Equals("USD"))
            {
                profitInDecAfterExchange = profitInDecimal * exchangeUSD;
            }
            else if (currencies.Equals("UAH"))
            {
                profitInDecAfterExchange = profitInDecimal;
            }
            else
            {
                Console.WriteLine("Некорректный ввод валюты. Обратите внимание на раскладку и заглавные буквы");
                goto Beggining;
            }
           

            singleTax = profitInDecAfterExchange * singleTaxPercentage;
            singleDeposit = minProfit * singleDepositPercentage;
            profitAfterTaxes = profitInDecAfterExchange - singleTax - singleDeposit;


            //у меня начали появляться лишние цифры после запятой, нагуглил спецификатор вывода и использовал его


            Console.WriteLine("Сумма вашего дохода составляет " + String.Format("{0:f2}",profitInDecAfterExchange) + "грн");
            Console.WriteLine("Единый налог составит " + String.Format("{0:f2}", singleTax) + "грн");
            Console.WriteLine("Единый социальный вклад составит " + String.Format("{0:f2}",singleDeposit) + "грн");
            Console.WriteLine("Ваш доход за вычетом налогов составит " + String.Format("{0:f2}",profitAfterTaxes) + "грн");


            Console.ReadKey();

        }
    }
}

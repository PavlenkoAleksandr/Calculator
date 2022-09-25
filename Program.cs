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
            decimal incomeAfterExchange;
            string incomeInUahForUser;
            string singleTaxForUser;
            string singleDepositForUser;
            string profitForUser;

            Console.WriteLine("Приветствую Вас в калькуляторе доходов!");

            Beggining:
            Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
            currencies = Console.ReadLine();

            Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше(используя числовой формат записи)");
            income = Console.ReadLine();
            incomeInDecimal = Convert.ToDecimal(income);

            //сравниваю введённую пользователем валюту, если допустил ошибку - возвращаю на этап ввода валюты

            if (currencies.Equals("EUR"))
            {
                incomeAfterExchange = incomeInDecimal * exchangeEUR;
            }
            else if (currencies.Equals("USD"))
            {
                incomeAfterExchange = incomeInDecimal * exchangeUSD;
            }
            else if (currencies.Equals("UAH"))
            {
                incomeAfterExchange = incomeInDecimal;
            }
            else
            {
                Console.WriteLine("Некорректный ввод валюты. Обратите внимание на раскладку и заглавные буквы");
                goto Beggining;
            }
           
            singleTax = incomeAfterExchange * singleTaxRate;
            singleDeposit = minProfit * singleDepositRate;
            profit = incomeAfterExchange - singleTax - singleDeposit;

            //у меня начали появляться лишние цифры после запятой, нагуглил спецификатор вывода и использовал его
            incomeInUahForUser = String.Format("{0:f2}",incomeAfterExchange);
            singleTaxForUser = String.Format("{0:f2}", singleTax);
            singleDepositForUser = String.Format("{0:f2}",singleDeposit);
            profitForUser = String.Format("{0:f2}",profit);

            Console.WriteLine("Сумма вашего дохода составляет " + incomeInUahForUser + "грн");
            Console.WriteLine("Единый налог составит " + singleTaxForUser + "грн");
            Console.WriteLine("Единый социальный вклад составит " + singleDepositForUser + "грн");
            Console.WriteLine("Ваш доход за вычетом налогов составит " + profitForUser + "грн");

            Console.ReadKey();

        }
    }
}

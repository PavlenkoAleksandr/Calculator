using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Principal;
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
            decimal incomeInDecimal;
            decimal singleTax;
            decimal singleDeposit;
            decimal profit;
            string currencies;
            decimal incomeAfterExchange = 0;

            
            Start();

            /*void ageControl()
            {
                int dateOfBirth;
                bool isEighteen;

                Console.WriteLine("Добро пожаловать в калькулятор доходов!\nВведите свой год рождения.");
                dateOfBirth = Convert.ToInt32(Console.ReadLine());
                int a = DateTime.Today - dateOfBirth;
                Console.WriteLine($"{a}");
            }
            */

            void Start()
            {
                //ageControl();
                ShowBeginning();
                Calculation();
                ShowResult();
            }
            
            void ShowBeginning()
            {
                string income; 

                Console.WriteLine("Приветствую Вас в калькуляторе доходов!");
                Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
                currencies = Console.ReadLine();

                Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше(используя числовой формат записи)");
                income = Console.ReadLine();
                incomeInDecimal = Convert.ToDecimal(income);
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
                    incomeAfterExchange = incomeInDecimal * exchangeEUR;
                    break;
                case "USD":
                    incomeAfterExchange = incomeInDecimal * exchangeUSD;
                    break;
                case "UAH":
                    incomeAfterExchange = incomeInDecimal;
                    break;
                }
                
                if ((currencies == "EUR") || (currencies == "USD") || (currencies == "UAH"))
                {
                    singleTax = incomeAfterExchange * singleTaxRate;
                    singleDeposit = minProfit * singleDepositRate;
                    profit = incomeAfterExchange - singleTax - singleDeposit;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод валюты, повторите ещё раз. Обратите внимание на регистр и язык ввода");
                    Start();
                }
            }

            void ShowResult()
            {
                string exitOrCalculateAgain;
                Console.WriteLine($"Сумма вашего дохода составляет {FormattoString(incomeAfterExchange)} грн");
                Console.WriteLine($"Единый налог составит {FormattoString(singleTax)} грн");
                Console.WriteLine($"Единый социальный вклад составит {FormattoString(singleDeposit)} грн");
                Console.WriteLine($"Ваш доход за вычетом налогов составит {FormattoString(profit)} грн");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Для повторного подсчёта налогов введите \"calculate again\"\nДля выхода из калькулятора введите \"exit\"");
                exitOrCalculateAgain = Console.ReadLine();

                if(exitOrCalculateAgain == "calculate again")
                {
                    Console.Clear();
                    Start();
                }
                else if(exitOrCalculateAgain == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неизвестная команда, проверьте ввод и попробуйте снова.");
                    Console.WriteLine("----------------------------------------------------------");
                    ShowResult();
                }
                
            }

            string FormattoString(decimal value)
            {
                return String.Format("{0:f2}", value);
            }
        }
    }
}

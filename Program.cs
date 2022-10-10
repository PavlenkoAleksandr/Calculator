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
            decimal singleTax = 0;
            decimal singleDeposit = 0;
            decimal profit = 0;
            string currencies;
            decimal incomeAfterExchange = 0;

            ageControl();

            void ageControl()
            {
                int dateOfBirth;
                bool isEighteen;
                int ageOfUser;
                int adultAge = 18;

                Console.WriteLine("Добро пожаловать в калькулятор доходов!\nВведите свой год рождения.");
                dateOfBirth = Convert.ToInt32(Console.ReadLine());

                ageOfUser = Convert.ToInt32(DateTime.Today.Year) - dateOfBirth;

                if (ageOfUser >= adultAge)
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
                    ShowBeginning();
                }
                else
                {
                    Console.WriteLine("Вы несовершеннолетний, дальнейшая работа калькулятора доходов ограничена. Нажмите ввод (Enter) для выхода.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            
            void ShowBeginning()
            {
                string income; 

                Console.WriteLine("Введите, пожалуйста, валюту в которой получаете доход\nUSD - в долларах, EUR - в евро, UAH - в гривне");
                currencies = Console.ReadLine();

                Console.WriteLine("Введите сумму Вашего месячного дохода в валюте, которую указали выше (используя числовой формат записи)");
                income = Console.ReadLine();
                incomeInDecimal = Convert.ToDecimal(income);
                Calculation();
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
                    ShowResult();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод валюты, повторите ещё раз. Обратите внимание на регистр и язык ввода");
                    ageControl();
                }
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
                decision = Console.ReadLine();

                if(decision == "calculate again")
                {
                    Console.Clear();
                    ageControl();
                }
                else if(decision == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Неизвестная команда, проверьте ввод и попробуйте снова.");
                    Console.WriteLine("-------------------------------------------------------");
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

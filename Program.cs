using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите сумму Вашего месячного дохода в гривнах (используя числовой формат записи)");
            string profit = Console.ReadLine();
            decimal profitInDecimal = Convert.ToDecimal(profit);
            decimal singleTaxPercentage = 0.05m;
            decimal singleTax = profitInDecimal * singleTaxPercentage;
            decimal singleDeposit;
            decimal singleDepositPercentage = 0.22m;
            // с помощью оператора if  сравниваю доход пользователя с 6500, для корректного вычисления singleDeposit.
            if (profitInDecimal <= 6500)
            {
                singleDeposit = profitInDecimal * singleDepositPercentage;
            }
            else
            {
                singleDeposit = 6500 * singleDepositPercentage;
            }
            decimal profitAfterTaxes = profitInDecimal - singleTax - singleDeposit;

            Console.WriteLine("Сумма вашего дохода составляет " + profitInDecimal + "грн");
            Console.WriteLine("Единый налог составит " + singleTax + "грн");
            Console.WriteLine("Единый социальный вклад составит " + singleDeposit + "грн");
            Console.WriteLine("Ваш доход за вычетом налогов составит " + profitAfterTaxes + "грн");

            Console.ReadKey();

        }
    }
}

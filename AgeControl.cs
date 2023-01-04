using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class AgeControl
    {
        UserInput userInput = new UserInput();

        public void ShowAgeControl()
        {
            string date;
            bool isEighteen;
            int ageOfUser;
            int dateOfBirth;
            const int adultAge = 18;
            const int oldestManAlive = 1904;

            Console.WriteLine("--------------------------");
            Console.WriteLine("Введите свой год рождения.");
            Console.WriteLine("--------------------------");
            date = userInput.GetUserInput(TypeOfUserInput.year);
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

            if (!isEighteen)
            {
                Console.Clear();
                Console.WriteLine("Дальнейшая работа программы ограничена. Нажмите ввод (Enter) для выхода.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}

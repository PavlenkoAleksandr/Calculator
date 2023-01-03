using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class AgeCalculator
    {
        public void Show()
        {
            AgeFromDate();
        }

        private void AgeFromDate()
        {
            int daysInYear = 365;

            UserInput userInput = new UserInput();

            Console.WriteLine("Укажите дату рождения в формате дд.мм.гггг");

            string dateString = Console.ReadLine();
            DateTime dateOfUser = DateTime.Parse(dateString);
            DateTime dateTimeNow = DateTime.Today;
            TimeSpan difference = dateTimeNow.Subtract(dateOfUser);
            
            int numberOfyears = difference.Days / daysInYear;

            int[] type1 = { 1, 21, 31, 41, 51, 61, 71, 81, 91, 101 };
            int[] type2 = { 2, 3, 4, 22, 23, 24, 32, 33, 34, 42, 43, 44, 52, 53, 54, 52, 53, 54, 62, 63, 64, 72, 73, 74, 82, 83, 84, 92, 93, 94, 102, 103, 104 };

            if (type1.Contains(numberOfyears))
            {
                Console.WriteLine($"Возраст человека, который родился {dateOfUser.ToShortDateString()} составляет {numberOfyears} год");
            }
            else if(type2.Contains(numberOfyears))
            {
                Console.WriteLine($"Возраст человека, который родился {dateOfUser.ToShortDateString()} составляет {numberOfyears} года");
            }
            else
            {
                Console.WriteLine($"Возраст человека, который родился {dateOfUser.ToShortDateString()} составляет {numberOfyears} лет");
            }
            Console.ReadKey();

            string decision;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Для повторного подсчёта введите \"calculate again\"\nДля закрытия калькулятора введите \"exit\"\nДля выхода в главное меню введите \"return\"");
            decision = userInput.GetUserInput(TypeOfUserInput.command);

            if (decision == "calculate again")
            {
                Console.Clear();
                Show();
            }
            else if (decision == "exit")
            {
                Environment.Exit(0);
            }
            else if (decision == "return")
            {
                Console.Clear();
                MainMenu mainMenu = new MainMenu();
                mainMenu.MenuSelection();
            }
        }
    }
}

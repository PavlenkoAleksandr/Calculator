using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class MainMenu
    {
        UserInput userInput = new UserInput();

        private void AgeControl()
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

        public void ShowMenu()
        {
            Console.WriteLine("Добро пожаловать в универсальный калькулятор!");

            AgeControl();
            MenuSelection();
        }

        private void MenuSelection()
        {
            string decision;

            //Console.Clear();
            Console.WriteLine("Для выбора приложения введите цифру, которая соответствует нужному пункту в меню.\n1.Простой калькулятор\n2.Калькулятор возраста\n3.Калькулятор налогов");

            string mainchoice = userInput.GetUserInput(TypeOfUserInput.number);

            if (mainchoice == "1")
            {
                Console.Clear();
                SimpleCalculator simpleCalculator = new SimpleCalculator();
                simpleCalculator.Show();
            }
            else if (mainchoice == "2")
            {
                Console.Clear();
                Console.WriteLine("На данный момент функционал ограничен.\nДля выхода в главное меню введите \"return\"\nДля выхода из программы введите \"exit\"");
                decision = userInput.GetUserInput(TypeOfUserInput.command);

                if (decision == "exit")
                {
                    Environment.Exit(0);
                }
                else if (decision == "return")
                {
                    Console.Clear();
                    MenuSelection();
                }
                Console.ReadKey();
            }
            else if (mainchoice == "3")
            {
                Console.Clear();
                TaxCalculator taxCalculator = new TaxCalculator();
                taxCalculator.Show();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("------------------------------\nНе удалось выбрать калькулятор\n------------------------------");
                MenuSelection();
            }
        }
    }
}

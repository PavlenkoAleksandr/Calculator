using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class MainMenu
    {
        UserInput userInput = new UserInput();
        AgeControl ageControl = new AgeControl();

        public void ShowMenu()
        {
            Console.WriteLine("Добро пожаловать в универсальный калькулятор!");

            ageControl.ShowAgeControl();
            Console.Clear();
            MenuSelection();
        }

        public void MenuSelection()
        {
            string decision;

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

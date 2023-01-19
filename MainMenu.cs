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
            Console.WriteLine("Нажмите любую кнопку, чтобы продолжить");
            Console.ReadKey();
            Console.Clear();
            ageControl.ShowAgeControl();
            Console.Clear();
            MenuSelection();
        }

        public void MenuSelection()
        {
            Console.WriteLine("Для выбора приложения введите цифру, которая соответствует нужному пункту в меню.\n1.Простой калькулятор\n2.Калькулятор возраста\n3.Калькулятор доходов");

            string mainchoice = userInput.GetUserInput(TypeOfUserInput.number);

            if (mainchoice == "1")
            {
                Console.Clear();
                ICalculator iCalculator = new SimpleCalculator("простой калькулятор");
                iCalculator.Start();
            }
            else if (mainchoice == "2")
            {
                Console.Clear();
                ICalculator iCalculator = new AgeCalculator("калькулятор возраста");
                iCalculator.Start();
            }
            else if (mainchoice == "3")
            {
                Console.Clear();
                ICalculator iCalculator = new TaxCalculator("калькулятор доходов");
                iCalculator.Start();
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

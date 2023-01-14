using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class BaseCalculator
    {
        UserInput userInput = new UserInput();
        private string name;

        public BaseCalculator(string name)
        {
            this.name = name;
        }

        public void Start()
        {
            ShowGreeting();
            GettingInput();
            Calculation();
            Decision();
        }

        public virtual void ShowGreeting()
        {
            Console.WriteLine($"Вы выбрали {name}.");
        }
        
        public abstract void GettingInput();

        public abstract void Calculation();

        private void Decision()
        {
            string decision;

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Для повторного подсчёта введите \"calculate again\"\nДля закрытия калькулятора введите \"exit\"\nДля выхода в главное меню введите \"return\"");
            Console.WriteLine("-------------------------------------------------");
            decision = userInput.GetUserInput(TypeOfUserInput.command);

            if (decision == "calculate again")
            {
                Console.Clear();
                Start();
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

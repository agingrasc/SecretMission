using System;

namespace SecretMission
{
    public class Program
    {
        private static void DisplayMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1.Create Account");
            Console.WriteLine("2.Deposit");
            Console.WriteLine("3.Withdraw");
            Console.WriteLine("4.Account info");
            Console.WriteLine("5.Exit");
            Console.Write("Please enter your selection: ");
        }

        private static int GetMenuChoice()
        {
            var menuChoice = -1;
            try
            {
                menuChoice = int.Parse(Console.ReadLine());
            }
            catch (ArgumentNullException)
            {
            }

            return menuChoice;
        }

        private static bool ExecuteMenuSelection(int menuChoice, Atm atm)
        {
            switch (menuChoice)
            {
                case 1:
                    atm.CreateAccount();
                    break;
                case 2:
                    atm.Deposit();
                    break;
                case 3:
                    atm.Withdraw();
                    break;
                case 4:
                    atm.AccountInfo();
                    break;
                case 5:
                    return true;
                default:
                    Console.WriteLine(@"Invalid selection!");
                    break;
            }

            return false;
        }

        static void Main(string[] args)
        {
            var accountFactory = new AccountFactory(new RandomAccountNumberGenerator());
            var atm = new Atm(new ConsoleWrapper(), accountFactory);
            var exit = false;
            while (!exit)
            {
                DisplayMenu();
                var menuChoice = GetMenuChoice();
                exit = ExecuteMenuSelection(menuChoice, atm);
            }
        }
    }
}
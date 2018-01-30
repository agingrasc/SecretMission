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
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                return -1;
            }
        }

        private static void ExecuteMenuSelection(int menuChoice, Atm atm)
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
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(@"Invalid selection!");
                    break;
            }
        }

        static void Main(string[] args)
        {
            var accountFactory = new AccountFactory(new RandomAccountNumberGenerator());
            var atm = new Atm(new ConsoleWrapper(), accountFactory);
            while (true)
            {
                DisplayMenu();
                var menuChoice = GetMenuChoice();
                ExecuteMenuSelection(menuChoice, atm);
            }
        }
    }
}
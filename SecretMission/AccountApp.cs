using System;

namespace SecretMission
{
    public class Program
    {
        static void Main(string[] args)
        {
            var atm = new Atm();
            while (true)
            {
                Console.WriteLine(@"Menu");
                Console.WriteLine(@"1.Create Account");
                Console.WriteLine(@"2.ATM");
                Console.WriteLine(@"3.Account info");
                Console.Write(@"Please enter your selection: ");
                var menuChoice = int.Parse(Console.ReadLine());

                //Wow, look at that switch case. My eyes are burning. Switchception.
                switch (menuChoice)
                {
                    case 1:                       
                        atm.CreateAccount();
                        break;
                    case 2:
                        //Console.WriteLine(@"1.Deposit Or Withdraw");
                        Console.WriteLine(@"1.Deposit");
                        Console.WriteLine(@"2.Withdraw");
                        Console.Write(@"Please enter your selection: ");
                        var atmMenuChoice = int.Parse(Console.ReadLine());
                        switch (atmMenuChoice)
                        {
                            case 1:
                                atm.Deposit();
                                break;
                            case 2:
                                atm.Withdraw();
                                break;
                            default:
                                Console.WriteLine(@"Invalid selection!");
                                break;
                        }
                        break;
                    case 3:
                        atm.AccountInfo();
                        break;
                    default:
                        Console.WriteLine(@"Invalid selection!");
                        break;
                }
            }
        }
    }
}

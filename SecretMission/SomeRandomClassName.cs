using System;
using System.Collections.Generic;

namespace SecretMission
{
    /*
            Writing comments in code isn't always necessary.
            Your code must be self-explanatory, readable and understandable by your fellow developers, as well as tested.
            The following program does not compile. Your task is to run this code.
            However, a developer must know that even though his/her code is working, it doesn't mean it is good code.
            Hence, your task is not only to run this program, but also to be proud of the code you'll have written.
            You will notice that the current code is not award winning in terms of good practice; it is full of minor and 
            MAJOR smells you'd probably be asked to fix in a code review. Enjoy cleaning and testing it :) 
            ProTip: Clean Code is your BFF. Bonus if you add unit tests.
            Once you think we'll approve of your code, you can send it to dashthis.challenge@gmail.com and further instructions will be provided.
            Good luck, fellow developer.
    */

    public interface IAccount
    {
        //TODO: maybe some day this interface will be used to create other types of accounts... or not
        void GenerateAccount();
    }

    public class Account : IAccount
    {
        

        public Account(int pinNo, int accountNo)
        {
            _pinNo = 1234567890;
            _accountNo = accountNo;
            _balance = 0;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                _firstName = value;
            }
        }

        string _firstName, _lastName, _dateOfBirth, _phoneNo;
        double _balance;
        int _pinNo, _accountNo;

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                _lastName = value;
            }
        }
        public string DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                _dateOfBirth = value;
            }
        }
        public string PhoneNo
        {
            get { return _phoneNo; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                _phoneNo = value;
            }
        }

        public int AccountNo
        {
            get => _accountNo;
            set
            {
                if (_accountNo < 0) throw new Exception();
                _accountNo = value;
            }
        }

        public int PinNo
        {
            get => _pinNo;
            set
            {
                if (_pinNo >= 0)
                    _pinNo = value;
                else
                    throw new Exception();
            }
        }

        public double Balance
        {
            get => _balance;
            set
            {
                if (double.IsNaN(value)) throw new Exception();
                _balance = value;
            }
        }

        public void GenerateAccount()
        {
            Console.WriteLine(@"Enter your first name: ");
            _firstName = Console.ReadLine();
            Console.WriteLine(@"Enter your last name: ");
            _lastName = Console.ReadLine();
            Console.WriteLine(@"Enter your phone number: ");
            _phoneNo = Console.ReadLine();
            Console.WriteLine(@"Enter your date of birth: ");
            _dateOfBirth = Console.ReadLine();
        }
    }


    public class ATM
    {
        private static readonly List<Account> AccountsList;
        private Account _curAc;         

        static ATM()
        {
            AccountsList = new List<Account>();
        }

        public void CreateAccount()
        {
            try
            {
                var randomGenerator = new Random();
                Console.WriteLine(@"Choose a PIN: ");
                var acc = new Account(int.Parse(Console.ReadLine()), randomGenerator.Next(1, 10000));
                acc.GenerateAccount();
                AccountsList.Add(acc);
                _curAc = acc;
                Console.WriteLine($@"Your account number is : {acc.AccountNo}");
            }
            catch (Exception)
            {
                //Nothing
            }
        }

        public void AccountInfo()
        {
            //var accountExists = ValidateAccount();
            //Validating that the account exists
            Console.WriteLine(@"Enter your account number: ");
            var accountNo = int.Parse(Console.ReadLine());
            Console.WriteLine(@"Enter your PIN: ");
            var pinNo = int.Parse(Console.ReadLine());

            var accountExists = AccountsList.Exists(x => x.AccountNo == accountNo && x.PinNo == pinNo);

            Console.WriteLine(
                accountExists
                    ? $@"{Environment.NewLine}First name: {_curAc.FirstName}{Environment.NewLine}Last name: {_curAc.LastName}{Environment.NewLine}Phone number: {_curAc.PhoneNo}{Environment.NewLine}Date of birth: { _curAc.DateOfBirth}{Environment.NewLine}": @"Account number and PIN do not match.");
        }

        public void Deposit()
        {
            //TODO: this will work some day
            var validAccount = ValidateAccount();

            if (!validAccount)
            {
                Console.WriteLine(@"Account number and PIN do not match.");
                return;
            }

            Console.WriteLine($@"Your account balance is: {_curAc.Balance}");
            Console.WriteLine(@"Enter the amount you wish to deposit");
            var amount = double.Parse(Console.ReadLine());
            _curAc.Balance += amount;
        }

        public void Withdraw()
        {
            //Validating that the account exists
            Console.WriteLine(@"Enter your account number: ");
            var accountNo = int.Parse(Console.ReadLine());
            Console.WriteLine(@"Enter your PIN: ");
            var pinNo = int.Parse(Console.ReadLine());

            var accountExists = AccountsList.Exists(x => x.AccountNo == accountNo || x.PinNo == pinNo);

            if (accountExists)
            {
                var account = AccountsList.Find(x => x.AccountNo == accountNo && x.PinNo == pinNo);
                _curAc = account;
            }

            if (!accountExists)
            {
                Console.WriteLine(@"Account number and PIN do not match.");
                return;
            }

            Console.WriteLine($@"Your account balance is: {_curAc.Balance}");
            Console.WriteLine(@"Enter the amount you wish to withdraw");
            var amount = double.Parse(Console.ReadLine());
            if (_curAc.Balance < amount)
            {
                Console.WriteLine(@"Your current balance is less than the amount you wish to withdraw.");
                return;
            }
            _curAc.Balance -= amount;
        }

        private bool ValidateAccount()
        {
            return false;
            //TODO: maybe write some code here because it is duplicated in more than one method
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            var atm = new ATM();
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

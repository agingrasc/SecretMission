using System;
using System.Collections.Generic;

namespace SecretMission
{
    public class Atm
    {
        private static readonly List<Account> AccountsList;
        private Account currentAccount;         

        static Atm()
        {
            AccountsList = new List<Account>();
        }

        public void CreateAccount()
        {
            try
            {
                var randomGenerator = new Random();
                Console.WriteLine(@"Choose a PIN: ");
                var account = new Account(int.Parse(Console.ReadLine()), randomGenerator.Next(1, 10000));
                account.GenerateAccount(new ConsoleWrapper());
                AccountsList.Add(account);
                currentAccount = account;
                Console.WriteLine($@"Your account number is : {account.AccountNumber}");
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
            var accountNumber = int.Parse(Console.ReadLine());
            Console.WriteLine(@"Enter your PIN: ");
            var pinNumber = int.Parse(Console.ReadLine());

            var accountExists = AccountsList.Exists(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);

            Console.WriteLine(
                accountExists
                    ? $@"{Environment.NewLine}First name: {currentAccount.FirstName}{Environment.NewLine}Last name: {currentAccount.LastName}{Environment.NewLine}Phone number: {currentAccount.PhoneNumber}{Environment.NewLine}Date of birth: { currentAccount.DateOfBirth}{Environment.NewLine}": @"Account number and PIN do not match.");
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

            Console.WriteLine($@"Your account balance is: {currentAccount.Balance}");
            Console.WriteLine(@"Enter the amount you wish to deposit");
            var amount = double.Parse(Console.ReadLine());
            currentAccount.Balance += amount;
        }

        public void Withdraw()
        {
            //Validating that the account exists
            Console.WriteLine(@"Enter your account number: ");
            var accountNumber = int.Parse(Console.ReadLine());
            Console.WriteLine(@"Enter your PIN: ");
            var pinNumber = int.Parse(Console.ReadLine());

            var accountExists = AccountsList.Exists(x => x.AccountNumber == accountNumber || x.PinNumber == pinNumber);

            if (accountExists)
            {
                var account = AccountsList.Find(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
                currentAccount = account;
            }

            if (!accountExists)
            {
                Console.WriteLine(@"Account number and PIN do not match.");
                return;
            }

            Console.WriteLine($@"Your account balance is: {currentAccount.Balance}");
            Console.WriteLine(@"Enter the amount you wish to withdraw");
            var amount = double.Parse(Console.ReadLine());
            if (currentAccount.Balance < amount)
            {
                Console.WriteLine(@"Your current balance is less than the amount you wish to withdraw.");
                return;
            }
            currentAccount.Balance -= amount;
        }

        private bool ValidateAccount()
        {
            return false;
            //TODO: maybe write some code here because it is duplicated in more than one method
        }
    }
}
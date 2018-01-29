using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace SecretMission
{
    public class Atm
    {
        private readonly List<Account> AccountsList;
        private Account currentAccount;
        private readonly ILineReaderWriter console;
        private readonly IAccountFactory accountFactory;

        public Atm(ILineReaderWriter console, IAccountFactory accountFactory)
        {
            AccountsList = new List<Account>();
            this.console = console;
            this.accountFactory = accountFactory;
        }

        public void CreateAccount()
        {
            try
            {
                console.WriteLine(@"Choose a PIN: ");
                var pinNumber = int.Parse(console.ReadLine());
                Account account = accountFactory.CreateAccountFromPinNumber(pinNumber);
                account.GenerateAccount(console);
                AccountsList.Add(account);
                currentAccount = account;
                console.WriteLine($@"Your account number is : {account.AccountNumber}");
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("The pin number can not be empty.");
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
            var validAccount = ValidateAccount(this.currentAccount, 0, 0);

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

        public bool ValidateAccount(Account currentAccount, int accountNumber, int pinNumber)
        {
            return currentAccount.PinNumber == pinNumber && currentAccount.AccountNumber == accountNumber;
        }

        public void AddAccount(Account account)
        {
            AccountsList.Add(account);
        }

        public Account Authentificate()
        {
            var accountNumber = 0;
            var pinNumber = 0;
            try
            {
                console.WriteLine("Enter your account number: ");
                accountNumber = int.Parse(console.ReadLine());
                console.WriteLine("Enter your pin number: ");
                pinNumber = int.Parse(console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("Account number and pin number must not be empty.");
            }

            var accountExist = AccountsList.Exists(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
            if (accountExist)
            {
                return AccountsList.Find(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
            }
            console.WriteLine("The account number and pin number does not match.");
            return null;
        }
    }
}
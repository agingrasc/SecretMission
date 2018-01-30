using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace SecretMission
{
    public class Atm
    {
        private readonly List<Account> AccountsList;
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
                console.WriteLine($@"Your account number is : {account.AccountNumber}");
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("The pin number can not be empty.");
            }
        }

        public void AccountInfo()
        {
            var account = Authentificate();

            if (account != null)
            {
                console.WriteLine(
                    $@"{Environment.NewLine}First name: {account.FirstName}{Environment.NewLine}Last name: {
                            account.LastName
                        }{Environment.NewLine}Phone number: {account.PhoneNumber}{Environment.NewLine}Date of birth: {
                            account.DateOfBirth
                        }{Environment.NewLine}");
            }
        }

        public void Deposit()
        {
            var account = Authentificate();

            if (account == null)
            {
                return;
            }

            console.WriteLine($"Your account balance is: {account.Balance}");
            console.WriteLine("Enter the amount you wish to deposit");

            double amount = 0;
            try
            {
                amount = double.Parse(console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("The deposit must not be empty.");
            }

            account.Balance += amount;
        }

        public void Withdraw()
        {
            var account = Authentificate();

            if (account == null)
            {
                return;
            }

            console.WriteLine($"Your account balance is: {account.Balance}");
            console.WriteLine("Enter the amount you wish to withdraw");

            double amount = 0;
            try
            {
                amount = double.Parse(console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("The amount to withdraw must not be null.");
            }

            if (account.Balance < amount)
            {
                Console.WriteLine("Your current balance is less than the amount you wish to withdraw.");
                return;
            }

            account.Balance -= amount;
        }

        private Account ValidateAccount(int accountNumber, int pinNumber)
        {
            var accountExist = AccountsList.Exists(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
            if (accountExist)
            {
                return AccountsList.Find(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
            }

            console.WriteLine("The account number and pin number does not match.");
            return null;
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

            return ValidateAccount(accountNumber, pinNumber);
        }

        public void AddAccount(Account account)
        {
            AccountsList.Add(account);
        }
    }
}
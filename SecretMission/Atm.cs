using System;
using System.Collections.Generic;

namespace SecretMission
{
    public class Atm
    {
        private readonly List<Account> accountsList;
        private readonly ILineReaderWriter console;
        private readonly IAccountFactory accountFactory;

        public Atm(ILineReaderWriter console, IAccountFactory accountFactory)
        {
            accountsList = new List<Account>();
            this.console = console;
            this.accountFactory = accountFactory;
        }

        public void CreateAccount()
        {
            console.WriteLine(@"Choose a PIN: ");

            var pinNumber = 0;
            try
            {
                pinNumber = int.Parse(console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("The pin number can not be empty.");
            }

            Account account;
            try
            {
                account = accountFactory.CreateAccountFromPinNumber(pinNumber);
                account.GenerateAccount(console);
            }
            catch (Exception)
            {
                console.WriteLine("Invalid information, the account was not created.");
                return;
            }

            accountsList.Add(account);
            console.WriteLine($@"Your account number is : {account.AccountNumber}");
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

            var amount = GetAmount();
            account.Deposit(amount);
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

            var amount = GetAmount();
            account.Withdraw(amount);
        }

        private Account ValidateAccount(int accountNumber, int pinNumber)
        {
            var accountExist = accountsList.Exists(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
            if (accountExist)
            {
                return accountsList.Find(x => x.AccountNumber == accountNumber && x.PinNumber == pinNumber);
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
            accountsList.Add(account);
        }

        private double GetAmount()
        {
            try
            {
                return double.Parse(console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                console.WriteLine("The amount must not be null.");
            }

            return 0;
        }
    }
}
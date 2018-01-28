using System;
using System.Collections.Generic;

namespace SecretMission
{
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
}
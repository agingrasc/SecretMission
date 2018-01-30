using System;

namespace SecretMission
{
    public interface IAccount
    {
        void GenerateAccount(ILineReaderWriter console);
    }

    public class Account : IAccount
    {
        private int pinNumber, accountNumber;
        private string firstName, lastName, dateOfBirth, phoneNumber;
        private double balance;

        public Account()
        {
            // For Moq
        }

        public Account(int accountNumber, int pinNumber)
        {
            PinNumber = pinNumber;
            AccountNumber = accountNumber;
            balance = 0;
        }

        public Account(int accountNumber, int pinNumber, string firstName, string lastName, string dateOfBirth,
            string phoneNumber)
        {
            PinNumber = pinNumber;
            AccountNumber = accountNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
        }

        public string FirstName
        {
            get => firstName;
            private set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                lastName = value;
            }
        }

        public string DateOfBirth
        {
            get => dateOfBirth;
            private set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                dateOfBirth = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            private set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                phoneNumber = value;
            }
        }

        public virtual int AccountNumber
        {
            get => accountNumber;
            private set
            {
                if (accountNumber < 0)
                {
                    throw new Exception();
                }

                accountNumber = value;
            }
        }

        public virtual int PinNumber
        {
            get => pinNumber;
            private set
            {
                if (pinNumber < 0)
                {
                    throw new Exception();
                }

                pinNumber = value;
            }
        }

        public double Balance
        {
            get => balance;
            set
            {
                if (double.IsNaN(value)) throw new Exception();
                balance = value;
            }
        }

        public virtual void GenerateAccount(ILineReaderWriter console)
        {
            console.WriteLine(@"Enter your first name: ");
            FirstName = console.ReadLine();
            console.WriteLine(@"Enter your last name: ");
            LastName = console.ReadLine();
            console.WriteLine(@"Enter your phone number: ");
            PhoneNumber = console.ReadLine();
            console.WriteLine(@"Enter your date of birth: ");
            DateOfBirth = console.ReadLine();
        }

        public virtual void Deposit(double amount)
        {
            if (amount >= 0)
            {
                Balance += amount;
            }
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Your current balance is less than the amount you wish to withdraw.");
            }
        }
    }

    public interface IAccountFactory
    {
        Account CreateAccountFromPinNumber(int pinNumber);
    }

    public class AccountFactory : IAccountFactory
    {
        private readonly IAccountNumberGenerator generator;

        public AccountFactory(IAccountNumberGenerator generator)
        {
            this.generator = generator;
        }

        public Account CreateAccountFromPinNumber(int pinNumber)
        {
            return new Account(generator.Generate(), pinNumber);
        }
    }
}
using System;

namespace SecretMission
{
    public interface IAccount
    {
        //TODO: maybe some day this interface will be used to create other types of accounts... or not
        void GenerateAccount(ILineReaderWriter console);
    }

    public class Account : IAccount
    {
        private string firstName, lastName, dateOfBirth, phoneNumber;
        private double balance;
        private int pinNumber, accountNumber;

        public Account(int pinNumber, int accountNumber)
        {
            this.pinNumber = pinNumber;
            this.accountNumber = accountNumber;
            balance = 0;
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                lastName = value;
            }
        }

        public string DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                dateOfBirth = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                phoneNumber = value;
            }
        }

        public int AccountNumber
        {
            get => accountNumber;
            set
            {
                if (accountNumber < 0) throw new Exception();
                accountNumber = value;
            }
        }

        public int PinNumber
        {
            get => pinNumber;
            set
            {
                if (pinNumber >= 0)
                    pinNumber = value;
                else
                    throw new Exception();
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

        public void GenerateAccount(ILineReaderWriter console)
        {
            console.WriteLine(@"Enter your first name: ");
            firstName = console.ReadLine();
            console.WriteLine(@"Enter your last name: ");
            lastName = console.ReadLine();
            console.WriteLine(@"Enter your phone number: ");
            phoneNumber = console.ReadLine();
            console.WriteLine(@"Enter your date of birth: ");
            dateOfBirth = console.ReadLine();
        }
    }
}
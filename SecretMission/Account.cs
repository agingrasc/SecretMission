using System;

namespace SecretMission
{
    public interface IAccount
    {
        //TODO: maybe some day this interface will be used to create other types of accounts... or not
        void GenerateAccount();
    }

    public class Account : IAccount
    {
        private string firstName, lastName, dateOfBirth, phoneNumber;
        private double balance;
        private int pinNumber, accountNumber;

        public Account(int pinNumber, int accountNumber)
        {
            this.pinNumber = 1234567890;
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

        public int AccountNo
        {
            get => accountNumber;
            set
            {
                if (accountNumber < 0) throw new Exception();
                accountNumber = value;
            }
        }

        public int PinNo
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

        public void GenerateAccount()
        {
            Console.WriteLine(@"Enter your first name: ");
            firstName = Console.ReadLine();
            Console.WriteLine(@"Enter your last name: ");
            lastName = Console.ReadLine();
            Console.WriteLine(@"Enter your phone number: ");
            phoneNumber = Console.ReadLine();
            Console.WriteLine(@"Enter your date of birth: ");
            dateOfBirth = Console.ReadLine();
        }
    }
}
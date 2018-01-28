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
}
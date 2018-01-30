using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using SecretMission;

namespace SecretMissionTest
{
    [TestFixture]
    public class AtmTest
    {
        private const int AnAccountNumber = 0;
        private const int APinNumber = 1234;
        private const int AnInvalidAccountNumber = 13;
        private const int AnInvalidPinNumber = 666;
        private const string AFirstName = "John";
        private const string ALastName = "Smith";
        private const string ADateOfBirth = "01/01/2000";
        private const string APhoneNumber = "418 123-4567";
        private const double ADeposit = 20;
        private const double ASmallerDeposit = 10;
        private readonly List<string> validAuthentificationExchange;
        private readonly List<string> invalidAuthentificationExchange;
        private readonly List<string> validDepositExchange;

        private Mock<IAccountFactory> accountFactoryMock;
        private Mock<ILineReaderWriter> consoleMock;
        private Mock<ILineReaderWriter> consoleAuthentificationMock;
        private Mock<ILineReaderWriter> consoleInvalidAuthentificationMock;
        private Mock<ILineReaderWriter> validDepositMock;
        private Mock<Account> accountMock;
        private Atm atm;
        private int i;
        private Account aValidAccount;

        public AtmTest()
        {
            validAuthentificationExchange = new List<string>
            {
                AnAccountNumber.ToString(),
                APinNumber.ToString()
            };
            invalidAuthentificationExchange = new List<string>
            {
                AnInvalidAccountNumber.ToString(),
                AnInvalidPinNumber.ToString()
            };
            validDepositExchange = new List<string>
            {
                AnAccountNumber.ToString(),
                APinNumber.ToString(),
                ADeposit.ToString()
            };
        }

        [SetUp]
        public void Init()
        {
            consoleMock = new Mock<ILineReaderWriter>();
            consoleMock.Setup(t => t.ReadLine()).Returns(AnAccountNumber.ToString());
            accountMock = new Mock<Account>();
            accountMock.Setup(t => t.GenerateAccount(consoleMock.Object));
            accountFactoryMock = new Mock<IAccountFactory>();
            accountFactoryMock.Setup(t => t.CreateAccountFromPinNumber(AnAccountNumber))
                .Returns(() => accountMock.Object);
            atm = new Atm(consoleMock.Object, accountFactoryMock.Object);
            aValidAccount = new Account(AnAccountNumber, APinNumber, AFirstName, ALastName, ADateOfBirth, APhoneNumber);

            i = 0;

            consoleAuthentificationMock = new Mock<ILineReaderWriter>();
            consoleAuthentificationMock.Setup(t => t.ReadLine()).Returns(() => validAuthentificationExchange[i])
                .Callback(() => i++);
            consoleInvalidAuthentificationMock = new Mock<ILineReaderWriter>();
            consoleInvalidAuthentificationMock.Setup(t => t.ReadLine())
                .Returns(() => invalidAuthentificationExchange[i])
                .Callback(() => i++);

            validDepositMock = new Mock<ILineReaderWriter>();
            validDepositMock.Setup(t => t.ReadLine()).Returns(() => validDepositExchange[i]).Callback(() => i++);
        }

        [Test]
        public void whenCreateAccount_thenAccountIsCreated()
        {
            atm.CreateAccount();

            accountFactoryMock.Verify(t => t.CreateAccountFromPinNumber(AnAccountNumber), Times.Once);
            accountMock.Verify(t => t.GenerateAccount(consoleMock.Object), Times.Once);
        }

        [Test]
        public void givenNoPinNumber_whenCreateAccount_thenNoExceptionIsThrown()
        {
            var noPinNumberConsoleMock = new Mock<ILineReaderWriter>();
            atm = new Atm(noPinNumberConsoleMock.Object, accountFactoryMock.Object);

            Assert.DoesNotThrow(() => atm.CreateAccount());
        }

        [Test]
        public void givenValidCredentials_whenAuthentificate_thenIsAuthentificated()
        {
            atm = new Atm(consoleAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            var authentificatedAccount = atm.Authentificate();

            Assert.AreEqual(aValidAccount, authentificatedAccount);
        }

        [Test]
        public void givenInvalidCredentials_whenAuthentificate_thenIsNotAuthentificated()
        {
            atm = new Atm(consoleInvalidAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            var authentificatedAccount = atm.Authentificate();

            Assert.IsNull(authentificatedAccount);
        }

        [Test]
        public void givenExistingAccount_whenAccountInfo_thenAccountInfoIsDisplayed()
        {
            atm = new Atm(consoleAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            atm.AccountInfo();

            consoleAuthentificationMock.Verify(t => t.WriteLine(It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void givenNonExistingAccount_whenAccountInfo_thenNoDisplayingOccured()
        {
            atm = new Atm(consoleInvalidAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            atm.AccountInfo();

            consoleInvalidAuthentificationMock.Verify(t => t.WriteLine(It.IsAny<string>()), Times.AtMost(3));
            consoleInvalidAuthentificationMock.Verify(
                t => t.WriteLine("The account number and pin number does not match."), Times.AtLeastOnce);
        }

        [Test]
        public void givenExistingAccount_whenDeposit_thenDepositOccured()
        {
            atm = new Atm(validDepositMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            atm.Deposit();

            Assert.AreEqual(ADeposit, aValidAccount.Balance);
        }

        [Test]
        public void givenNonExistingAccount_whenDeposit_thenNoDepositOccured()
        {
            atm = new Atm(consoleInvalidAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            atm.Deposit();

            consoleInvalidAuthentificationMock.Verify(t => t.WriteLine(It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void givenNonExistingAccount_whenDeposit_thenNoExceptionIsThrowed()
        {
            atm = new Atm(consoleInvalidAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            Assert.DoesNotThrow(() => atm.Deposit());
        }

        [Test]
        public void givenNonExistingAccount_whenWithdraw_thenNoWithdrawOccured()
        {
            atm = new Atm(consoleInvalidAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            atm.Withdraw();

            consoleInvalidAuthentificationMock.Verify(t => t.WriteLine(It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void givenNonExistingAccount_whenWithdraw_thenNoExceptionIsThrowed()
        {
            atm = new Atm(consoleInvalidAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(aValidAccount);

            Assert.DoesNotThrow(() => atm.Withdraw());
        }

        [Test]
        public void givenExistingAccountWithABalance_whenWithdraw_thenAmountIsWithdrawed()
        {
            atm = new Atm(validDepositMock.Object, accountFactoryMock.Object);
            aValidAccount.Balance = ADeposit;
            atm.AddAccount(aValidAccount);

            atm.Withdraw();

            Assert.AreEqual(0, aValidAccount.Balance);
        }

        [Test]
        public void givenExistingAccountWithInsufficientBalance_whenWithdraw_thenNoAmountIsWithdrawed()
        {
            atm = new Atm(validDepositMock.Object, accountFactoryMock.Object);
            aValidAccount.Balance = ASmallerDeposit;
            atm.AddAccount(aValidAccount);

            atm.Withdraw();

            Assert.AreEqual(ASmallerDeposit, aValidAccount.Balance);
        }
    }
}
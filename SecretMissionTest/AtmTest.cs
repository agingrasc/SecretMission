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
        private readonly List<string> authentification;

        private Mock<IAccountFactory> accountFactoryMock;
        private Mock<ILineReaderWriter> consoleMock;
        private Mock<Account> accountMock;
        private Atm atm;
        private int i;

        public AtmTest()
        {
            authentification = new List<string>
            {
                AnAccountNumber.ToString(),
                APinNumber.ToString()
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

            i = 0;
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
        public void givenValidAccount_whenValidateAccount_thenAccountIsValid()
        {
            var account = new Account(AnAccountNumber, APinNumber);

            var actual = atm.ValidateAccount(account, AnAccountNumber, APinNumber);

            Assert.IsTrue(actual);
        }

        [Test]
        public void givenInvalidAccount_whenValidateAccount_thenAccountIsInvalid()
        {
            var account = new Account(AnAccountNumber, APinNumber);

            var actual = atm.ValidateAccount(account, AnInvalidAccountNumber, AnInvalidPinNumber);

            Assert.IsFalse(actual);
        }

        [Test]
        public void givenValidCredentials_whenAuthentificate_thenIsAuthentificated()
        {
            var account = new Account(AnAccountNumber, APinNumber);
            var consoleAuthentificationMock = new Mock<ILineReaderWriter>();
            consoleAuthentificationMock.Setup(t => t.ReadLine()).Returns(() => authentification[i]).Callback(() => i++);
            atm = new Atm(consoleAuthentificationMock.Object, accountFactoryMock.Object);
            atm.AddAccount(account);

            var authentificatedAccount = atm.Authentificate();

            Assert.AreEqual(account, authentificatedAccount);
        }
    }
}
using NUnit.Framework;
using Moq;
using SecretMission;

namespace SecretMissionTest
{
    [TestFixture]
    public class AtmTest
    {
        private const int AnAccountNumber = 0;

        private Mock<IAccountFactory> accountFactoryMock;
        private Mock<ILineReaderWriter> consoleMock;
        private Mock<Account> accountMock;
        private Atm atm;
        
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
            atm = new Atm();
        }

        [Test]
        public void whenCreateAccount_thenAccountIsCreated()
        {
            atm.CreateAccount(consoleMock.Object, accountFactoryMock.Object);
            
            accountFactoryMock.Verify(t => t.CreateAccountFromPinNumber(AnAccountNumber), Times.Once);
            accountMock.Verify(t => t.GenerateAccount(consoleMock.Object), Times.Once);
        }
    }
}
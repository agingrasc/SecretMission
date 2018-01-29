using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using SecretMission;

namespace SecretMissionTest
{
    [TestFixture]
    public class AccountTest
    {
        private const int APinNumber = 0;
        private const int AnAccountNumber = 0;
        private const string AFirstName = "John";
        private const string ALastName = "Smith";
        private const string APhoneNumber = "418 666-1234";
        private const string ADateOfBirth = "01/01/2000";
        private readonly List<string> APersonInput;

        private int i;
        private Mock<ILineReaderWriter> consoleMock;
        private ILineReaderWriter console;
        private Account account;

        public AccountTest()
        {
            APersonInput = new List<string>
            {
                AFirstName,
                ALastName,
                APhoneNumber,
                ADateOfBirth
            };
        }

        [SetUp]
        public void Init()
        {
            i = 0;
            consoleMock = new Mock<ILineReaderWriter>();
            consoleMock.Setup(t => t.ReadLine()).Returns(() => APersonInput[i]).Callback(() => i++);
            console = consoleMock.Object;
            account = new Account(AnAccountNumber, APinNumber);
        }
        
        [Test]
        public void givenValidInformation_whenGenerateAccount_thenTheInformationIsCorrectlyRequested()
        {
            account.GenerateAccount(console);
            
            consoleMock.Verify(t => t.WriteLine(It.IsAny<string>()), Times.Exactly(4));
        }

        [Test]
        public void givenValidInformation_whenGenerateAccount_thenAccountHasTheRightInformation()
        {
            account.GenerateAccount(console);
            
            Assert.AreEqual(AFirstName, account.FirstName);
            Assert.AreEqual(ALastName, account.LastName);
            Assert.AreEqual(APhoneNumber, account.PhoneNumber);
            Assert.AreEqual(ADateOfBirth, account.DateOfBirth);
        }
    }
}
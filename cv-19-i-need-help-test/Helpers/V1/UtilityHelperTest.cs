using System;
using AutoFixture;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;
using CV19INeedHelpTest.TestHelpers;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace CV19INeedHelpTest.Helpers.V1
{
    [TestFixture]
    public class UtilityHelperTest
    {
        private IUtilityHelper _classUnderTest;
        private Mock<IINeedHelpGateway> _fakeGateway;
        private Fixture _fixture = new Fixture();

        [TestCase]
        public void GetsTheNextDayIfNotABankHoliday()
        {
            DateTime dt = new DateTime(2020,06,17);
            var bankHoliday = _fixture.Create<BankHoliday>();
            bankHoliday.Date = dt.AddDays(12);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(dt)).Returns(bankHoliday);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(1));
        }
        
        [TestCase]
        public void GetsTheNextDayIfNoBankHolidaysInDb()
        {
            DateTime dt = new DateTime(2020,06,17);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(1));
        }
        
        [TestCase]
        public void GetsTheFollowingDayIfDayIsSaturday()
        {
            DateTime dt = new DateTime(2020,06,20);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            var bankHoliday = _fixture.Create<BankHoliday>();
            bankHoliday.Date = dt.AddDays(50);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(dt)).Returns(bankHoliday);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(2));
        }
        
        [TestCase]
        public void GetsTheFollowingMondayIfDayIsFriday()
        {
            DateTime dt = new DateTime(2020,06,19);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            var bankHoliday = _fixture.Create<BankHoliday>();
            bankHoliday.Date = dt.AddDays(50);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(dt)).Returns(bankHoliday);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(3));
        }
        
        [TestCase]
        public void GetsTheFollowingDayIfNextDayIsBankHoliday()
        {
            DateTime dt = new DateTime(2020,06,17);
            var bankHoliday = _fixture.Create<BankHoliday>();
            bankHoliday.Date = dt.AddDays(1);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(It.IsAny<DateTime>())).Returns(bankHoliday);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(2));
        }
        
        [TestCase]
        public void GetsTheFollowingDayIfNextDayIsFridayAndBankHoliday()
        {
            DateTime dt = new DateTime(2020,06,18);
            var bankHoliday = _fixture.Create<BankHoliday>();
            bankHoliday.Date = dt.AddDays(1);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(It.IsAny<DateTime>())).Returns(bankHoliday);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(4));
        }
        
        [TestCase]
        public void GetsTheFollowingTuesdayIfDayIsFridayAndNextMondayIsBankHoliday()
        {
            DateTime dt = new DateTime(2020,06,19);
            var bankHoliday = _fixture.Create<BankHoliday>();
            bankHoliday.Date = dt.AddDays(3);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(It.IsAny<DateTime>())).Returns(bankHoliday);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(4));
        }
        
        [TestCase]
        public void GetsTheFollowingWednesdayIfDayIsFridayAndNextMondayAndTuesdayAreBankHolidays()
        {
            DateTime dt = new DateTime(2020,06,19);
            var bankHoliday1 = _fixture.Create<BankHoliday>();
            var bankHoliday2 = _fixture.Create<BankHoliday>();
            bankHoliday1.Date = dt.AddDays(3);
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeGateway.Setup(x => x.GetNextBankHoliday(dt.AddDays(3))).Returns(bankHoliday1);
            bankHoliday2.Date = dt.AddDays(4);
            _fakeGateway.Setup(x => x.GetNextBankHoliday(dt.AddDays(4))).Returns(bankHoliday2);
            _classUnderTest = new UtilityHelper(_fakeGateway.Object);
            _classUnderTest.GetNextWorkingDay(dt).Should().Be(dt.AddDays(5));
        }
    }
}
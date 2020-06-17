using System;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V1
{
    public class UtilityHelper : IUtilityHelper
    {
        private readonly IINeedHelpGateway _iHelperGateway;
        public UtilityHelper(IINeedHelpGateway iHelperGateway)
        {
            _iHelperGateway = iHelperGateway;
        }
        public DateTime GetNextWorkingDay(DateTime date)
        {
            var nextDay = date.AddDays(1);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday || IsBankHoliday(nextDay))
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        private bool IsBankHoliday(DateTime date)
        {
            var nextBankHoliday = _iHelperGateway.GetNextBankHoliday(date);
            if (nextBankHoliday == null)
            {
                return false;
            }
            else
            {
                return nextBankHoliday.Date == date;
            }
        }
    }
}
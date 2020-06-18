using System;
using Amazon.Lambda.Core;
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
            LambdaLogger.Log($"Date requested: {date}");
            var nextDay = date.AddDays(1);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday || IsBankHoliday(nextDay))
            {
                nextDay = nextDay.AddDays(1);
            }
            LambdaLogger.Log($"Next working day: {nextDay}");
            return nextDay;
        }

        private bool IsBankHoliday(DateTime date)
        {
            LambdaLogger.Log($"Checking db for next bank holiday after: {date}");
            var nextBankHoliday = _iHelperGateway.GetNextBankHoliday(date);
            LambdaLogger.Log($"Next bank holiday returned: {nextBankHoliday}");
            if (nextBankHoliday == null)
            {
                return false;
            }
            else
            {
                return nextBankHoliday.Date == date.Date;
            }
        }
    }
}
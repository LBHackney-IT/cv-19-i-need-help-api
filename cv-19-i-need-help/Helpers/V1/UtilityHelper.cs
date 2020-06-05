using System;

namespace CV19INeedHelp.Helpers.V1
{
    public class UtilityHelper : IUtilityHelper
    {
        public DateTime GetNextWorkingDay()
        {
            var nextDay = DateTime.Now.AddDays(1);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
}
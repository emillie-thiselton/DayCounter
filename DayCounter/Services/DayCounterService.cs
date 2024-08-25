using DayCounter.Config;
using DayCounter.Models;
using DayCounter.Util;

namespace DayCounter.Services
{
    public static class DayCounterService {
        public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate) {
            // disregard starting/ending days
            firstDate = firstDate.AddDays(1);
            secondDate = secondDate.AddDays(-1);

            int totalDays = (secondDate.Date - firstDate.Date).Days + 1;

            // return 0 if secondDate is before firstDate 
            if (totalDays < 0) {
                return 0;
            }

            // calculate exact weeks + additional days in date range
            int weeks = (int)Math.Floor((decimal)totalDays / Constants.DAYS_IN_WEEK);
            int additionalDays = totalDays % Constants.DAYS_IN_WEEK;

            int weekDays = Constants.WEEKDAYS_IN_WEEK * weeks;

            if (additionalDays == 0) {
                return weekDays;
            }

            for (var i = 0; i < additionalDays; i++) {
                if (DateTimeHelper.IsWeekday(secondDate)) {
                    weekDays++;
                }
                secondDate = secondDate.AddDays(-1);
            }

            return weekDays;
        }

        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var weekDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (weekDays == 0) return weekDays;

            foreach (var holiday in publicHolidays)
            {
                if (holiday < secondDate && holiday > firstDate) {
                    weekDays--;
                }
            }

            return weekDays;
        }

        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {
            var weekDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (weekDays == 0) return weekDays;

            // get years in date range
            IList<int> years = [firstDate.Year];
            while (years[years.Count - 1] < secondDate.Year) {
                years.Add(years[years.Count - 1] + 1);
            }

            // call CalculateDate for each publicHolidays for each year
            IList<DateTime> publicHolidaysDates = [];

            foreach (var holiday in publicHolidays)
            {
                foreach (var year in years) {
                    publicHolidaysDates.Add(holiday.CalculateDate(year));
                }
            }

            foreach (var holiday in publicHolidaysDates)
            {
                if (holiday < secondDate && holiday > firstDate) {
                    weekDays--;
                }
            }

            return weekDays;
        }
    }
}
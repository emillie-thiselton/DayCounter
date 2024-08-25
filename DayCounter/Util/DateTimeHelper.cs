using DayCounter.Config;

namespace DayCounter.Util
{
    public static class DateTimeHelper {
        public static bool IsWeekday(DateTime date) {
            return (date.DayOfWeek != DayOfWeek.Saturday) && (date.DayOfWeek != DayOfWeek.Sunday);
        }

        public static int NumDaysBetweenDaysOfWeek(DayOfWeek firstDayOfWeek, DayOfWeek secondDayOfWeek) {
            var diff = secondDayOfWeek - firstDayOfWeek;
            if (firstDayOfWeek > secondDayOfWeek) {
                diff = Constants.DAYS_IN_WEEK - (-1 * diff);
            }

            return diff;
        }
    }
}

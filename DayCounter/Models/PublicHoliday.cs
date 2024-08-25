using DayCounter.Config;
using DayCounter.Util;

namespace DayCounter.Models
{
    public abstract class PublicHoliday
    {
        public int Month { get; set; }
        public abstract DateTime CalculateDate(int Year);
    }

    public class SimplePublicHoliday : PublicHoliday
    {
        public int Day { get; set; }
        public bool ObserveOnWeekend { get; set; }
        public SimplePublicHoliday(int Day, int Month, bool ObserveOnWeekend = false) {
            this.Day = Day;
            this.Month = Month;
            this.ObserveOnWeekend = ObserveOnWeekend;
        }
        public override DateTime CalculateDate(int Year) {
            DateTime publicHoliday = new(Year, Month, Day);

            if (ObserveOnWeekend) {
                if (publicHoliday.DayOfWeek == DayOfWeek.Saturday) {
                    publicHoliday = publicHoliday.AddDays(2);
                } else if (publicHoliday.DayOfWeek == DayOfWeek.Sunday) {
                    publicHoliday = publicHoliday.AddDays(1);
                }
            }

            return publicHoliday;
        }
    }

    public class ComplexPublicHoliday : PublicHoliday
    {
        public DayOfWeek WeekDay { get; set; }
        public int Week { get; set; }
        public ComplexPublicHoliday(DayOfWeek WeekDay, int Month, int Week) {
            this.WeekDay = WeekDay;
            this.Month = Month;
            this.Week = Week;
        }
        public override DateTime CalculateDate(int Year) {
            DateTime publicHoliday = new(Year, Month, 1);

            var daysToAdd = DateTimeHelper.NumDaysBetweenDaysOfWeek(publicHoliday.DayOfWeek, WeekDay);
            publicHoliday = publicHoliday.AddDays(daysToAdd + (Constants.DAYS_IN_WEEK * (Week - 1)));

            return publicHoliday;
        }
    }
}

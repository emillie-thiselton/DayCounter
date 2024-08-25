using DayCounter.Models;
using DayCounter.Services;

namespace DayCounter;

class Program
{
    static void Main(string[] args)
    {
        DateTime firstDate = DateTime.Parse("Apr 1, 2024");
        DateTime secondDate = DateTime.Parse("Apr 30, 2024");

        Console.WriteLine(DayCounterService.WeekdaysBetweenTwoDates(firstDate, secondDate));

        IList<DateTime> publicHolidayList = [
            DateTime.Parse("Dec 25, 2013"),
            DateTime.Parse("Dec 26, 2013"),
            DateTime.Parse("Jan 1, 2012"),
        ];

        Console.WriteLine(DayCounterService.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidayList));

        IList<PublicHoliday> publicHolidays = [
            new SimplePublicHoliday(25, 4),
            new SimplePublicHoliday(1, 1, true),
            new ComplexPublicHoliday(DayOfWeek.Monday, 6, 2),
            new SimplePublicHoliday(25, 12, true),
            new SimplePublicHoliday(26, 12, true),
        ];

        Console.WriteLine(DayCounterService.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays));
    }
}

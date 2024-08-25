using DayCounter.Models;
using DayCounter.Services;
using FluentAssertions;
using Xunit;

namespace DayCounterTests.Services
{
    public class DayCounterServiceTests
    {
        [Theory]
        [InlineData("Oct 7, 2013", "Oct 9, 2013", 1)]
        [InlineData("Oct 5, 2013", "Oct 14, 2013", 5)]
        [InlineData("Oct 7, 2013", "Jan 1, 2014", 61)]
        [InlineData("Oct 7, 2013", "Oct 5, 2013", 0)]
        [InlineData("Aug 15, 2024", "Aug 9, 2024", 0)] // first date comes after second date
        [InlineData("Aug 5, 2024", "Aug 9, 2024", 3)]  // no weekends
        [InlineData("Aug 9, 2024", "Aug 12, 2024", 0)] // start/end dates either side of weekend
        [InlineData("Aug 10, 2024", "Aug 13, 2024", 1)]
        [InlineData("Aug 9, 2024", "Aug 16, 2024", 4)] // 8 day period with weekend
        [InlineData("Aug 1, 2024", "Aug 31, 2024", 21)]
        [InlineData("Jan 1, 2024", "Dec 31, 2024", 260)] // leap year
        [InlineData("Jan 1, 2025", "Dec 31, 2025", 259)] // non-leap year
        public void WeekdaysBetweenTwoDatesTest_ReturnsExpectedValue(string firstDate, string secondDate, int expected)
        {
            // Arrange + Act
            int result = DayCounterService.WeekdaysBetweenTwoDates(
                DateTime.Parse(firstDate), DateTime.Parse(secondDate));

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData("Oct 7, 2013", "Oct 9, 2013", 1)]
        [InlineData("Dec 24, 2013", "Dec 27, 2013", 0)]
        [InlineData("Oct 7, 2013", "Jan 1, 2014", 59)]
        public void BusinessDaysBetweenTwoDatesTest_ReturnsExpectedValue(string firstDate, string secondDate, int expected)
        {
            // Arrange
            IList<DateTime> publicHolidays = [
                DateTime.Parse("Dec 25, 2013"),
                DateTime.Parse("Dec 26, 2013"),
                DateTime.Parse("Jan 1, 2012"),
            ];

            //Act
            int result = DayCounterService.BusinessDaysBetweenTwoDates(
                DateTime.Parse(firstDate), DateTime.Parse(secondDate), publicHolidays);

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData("Oct 7, 2013", "Oct 9, 2013", 1)]
        [InlineData("Dec 24, 2013", "Dec 27, 2013", 0)]
        [InlineData("Oct 7, 2013", "Jan 1, 2014", 59)]
        [InlineData("Apr 21, 2024", "Apr 27, 2024", 4)]
        public void BusinessDaysBetweenTwoDates_WithPublicHolidayDataStructure_ReturnsExpectedValue(string firstDate, string secondDate, int expected)
        {
            // Arrange
            IList<PublicHoliday> publicHolidays = [
                new SimplePublicHoliday(25, 4),
                new SimplePublicHoliday(1, 1, true),
                new ComplexPublicHoliday(DayOfWeek.Monday, 6, 2),
                new SimplePublicHoliday(25, 12, true),
                new SimplePublicHoliday(26, 12, true),
            ];

            // Act
            int result = DayCounterService.BusinessDaysBetweenTwoDates(
                DateTime.Parse(firstDate), DateTime.Parse(secondDate), publicHolidays);


            // Assert
            result.Should().Be(expected);
        }
    }
}

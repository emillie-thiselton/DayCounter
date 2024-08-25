using DayCounter.Util;
using FluentAssertions;
using Xunit;

namespace DayCounterTests.Util
{
    public class DateTimeHelperTests
    {
        [Fact]
        public void IsWeekDayTest_ReturnsTrue_WhenDateIsWeekday()
        {
            // Arrange
            DateTime date = DateTime.Parse("Aug 1, 2024");

            // Act
            bool result = DateTimeHelper.IsWeekday(date);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsWeekDayTest_ReturnsFalse_WhenDateIsNotWeekday()
        {
            // Arrange
            DateTime date = DateTime.Parse("Aug 10, 2024");

            // Act
            bool result = DateTimeHelper.IsWeekday(date);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, DayOfWeek.Monday, 1)]
        [InlineData(DayOfWeek.Monday, DayOfWeek.Saturday, 5)]
        [InlineData(DayOfWeek.Friday, DayOfWeek.Tuesday, 4)]
        [InlineData(DayOfWeek.Saturday, DayOfWeek.Monday, 2)]
        public void NumDaysBetweenDaysOfWeek_ReturnsExpectedValue(DayOfWeek firstDay, DayOfWeek secondDay, int expected)
        {
            // Arrange
            DateTime date = DateTime.Parse("Aug 10, 2024");

            // Act
            int result = DateTimeHelper.NumDaysBetweenDaysOfWeek(firstDay, secondDay);

            // Assert
            result.Should().Be(expected);
        }
    }
}

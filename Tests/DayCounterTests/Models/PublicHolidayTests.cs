using DayCounter.Models;
using DayCounter.Util;
using FluentAssertions;
using Xunit;

namespace DayCounterTests.Models
{
    public class PublicHolidayTests
    {
        [Fact]
        public void SimplePublicHoliday_ShouldReturnExpectedValues_ForAnzacDay()
        {
            // Arrange
            DateTime expected = new(2024, 4, 25);

            // Act
            SimplePublicHoliday AnzacDay = new(25, 4);
            DateTime result = AnzacDay.CalculateDate(2024);

            // Assert
            AnzacDay.Day.Should().Be(25);
            AnzacDay.Month.Should().Be(4);
            AnzacDay.ObserveOnWeekend.Should().Be(false);
            result.Should().Be(expected);
        }

        [Fact]
        public void SimplePublicHoliday_ShouldReturnExpectedValues_ForNewYears()
        {
            // Arrange
            DateTime expected = new(2023, 1, 2);

            // Act
            SimplePublicHoliday NewYears = new(1, 1, true);
            DateTime result = NewYears.CalculateDate(2023);

            // Assert
            NewYears.Day.Should().Be(1);
            NewYears.Month.Should().Be(1);
            NewYears.ObserveOnWeekend.Should().Be(true);
            result.Should().Be(expected);
        }

        [Fact]
        public void ComplexPublicHoliday__ShouldReturnExpectedValues_ForQueensBirthday()
        {
            // Arrange
            DateTime expected = new(2024, 6, 10);

            // Act
            ComplexPublicHoliday QueensBirthday = new(DayOfWeek.Monday, 6, 2);
            DateTime result = QueensBirthday.CalculateDate(2024);

            // Assert
            QueensBirthday.WeekDay.Should().Be(DayOfWeek.Monday);
            QueensBirthday.Month.Should().Be(6);
            QueensBirthday.Week.Should().Be(2);
            result.Should().Be(expected);
        }
    }
}

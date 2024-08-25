# DayCounter

A simple utility class that provides the following behaviour:

## WeekdaysBetweenTwoDates
`public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)`

Calculates the weekdays between a given first and second dates

## BusinessDaysBetweenTwoDates
`public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)`

`public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)`

Calculates the weekdays between a given first and second dates while also accounting for the supplied public holidays. Public Holidays can be supplied as a list of `DateTime` or `PublicHoliday`

### PublicHoliday Classes

There are two classes available to handle for more extensive public holiday requirements. Both support the `public abstract DateTime CalculateDate(int Year);` method which can be used to return the public holiday date for the provided year.

#### SimplePublicHoliday
`public SimplePublicHoliday(int Day, int Month, bool ObserveOnWeekend = false)`

- For public holidays which are always on the same day, e.g. Anzac Day on April 25th every year.
- For public holidays which are always on the same day, except when that falls on a weekend. e.g. New Year's Day on January 1st every year, unless that is a Saturday or Sunday, in which case the holiday is the next Monday.

#### ComplexPublicHoliday
`public ComplexPublicHoliday(DayOfWeek WeekDay, int Month, int Week)`

- For public holidays on a certain occurrence of a certain day in a month. e.g. Queen's Birthday on the second Monday in June every year

## Requirements

Download and install the `.NET 8` SDK. Link [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Running Locally

### Runs the solution

```bash
cd DayCounter

dotnet run
```

### Runs all unit tests

```bash
cd Tests/DayCounterTests

dotnet test
```

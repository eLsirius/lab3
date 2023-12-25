using Xunit;

public class CalendarTests
{
    private readonly Calendar calendar;

    public CalendarTests()
    {
        calendar = new Calendar();
    }

    [Fact]
    public void LeapCheck_ReturnsTrue_ForLeapYear()
    {
        // Arrange
        int year = 2020;

        // Act
        bool isLeapYear = calendar.LeapCheck(year);

        // Assert
        Assert.True(isLeapYear);
    }

    [Fact]
    public void LeapCheck_ReturnsFalse_ForNonLeapYear()
    {
        // Arrange
        int year = 2021;

        // Act
        bool isLeapYear = calendar.LeapCheck(year);

        // Assert
        Assert.False(isLeapYear);
    }

    [Theory]
    [InlineData(2020, 2022, 2)]
    [InlineData(2000, 2000, 0)]
    [InlineData(1990, 2000, 10)]
    public void GetYearIntervalLength_ReturnsCorrectLength(int year1, int year2, int expectedLength)
    {
        // Act
        int length = calendar.GetYearIntervalLength(year1, year2);

        // Assert
        Assert.Equal(expectedLength, length);
    }

    [Theory]
    [InlineData(2021, 1, 1, DayOfWeek.Friday)]
    [InlineData(2022, 12, 25, DayOfWeek.Sunday)]
    [InlineData(2023, 6, 15, DayOfWeek.Thursday)]
    public void GetNameOfDayOfTheWeek_ReturnsCorrectDayOfWeek(int year, int month, int day, DayOfWeek expectedDayOfWeek)
    {
        // Act
        DayOfWeek dayOfWeek = calendar.GetNameOfDayOfTheWeek(year, month, day);

        // Assert
        Assert.Equal(expectedDayOfWeek, dayOfWeek);
    }
}

using Microsoft.VisualBasic;

public class Calendar : ICalendar
{
    public bool LeapCheck(Int32 year)
    {
        if(DateTime.IsLeapYear(year))
            return true;
        return false;
    }

    public int GetYearIntervalLength(int year1, int year2)
    {
        if (year1 >= year2)
             return year1 - year2;
        return year2 - year1;
    }

    public DayOfWeek GetNameOfDayOfTheWeek(Int32 year, Int32 month, Int32 day)
    {
        DateTime date = new DateTime(year, month, day);
        var result = date.DayOfWeek;
        return result;
    }
}
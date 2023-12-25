public interface ICalendar
{
    public bool LeapCheck(Int32 year);
    public int GetYearIntervalLength(int year1, int int2);
    public DayOfWeek GetNameOfDayOfTheWeek(int year, int month, int day);
}
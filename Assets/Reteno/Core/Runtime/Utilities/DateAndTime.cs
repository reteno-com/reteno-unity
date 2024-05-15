using UnityEngine;

public class DateAndTime
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }
    public int Second { get; private set; }
    public bool UseCurrentTime { get; private set; } = false;

    private DateAndTime()
    {
        UseCurrentTime = true;
    }

    private DateAndTime(int Year, int Month, int Day, int Hour, int Minute, int Second)
    {
        this.Year = Year;
        this.Month = Month;
        this.Day = Day;
        this.Hour = Hour;
        this.Minute = Minute;
        this.Second = Second;
    }

    public static DateAndTime Now()
    {
        return new DateAndTime();
    }

    public static DateAndTime Of(int Year, int Month, int Day, int Hour, int Minute, int Second)
    {
        return new DateAndTime(Year, Month, Day, Hour, Minute, Second);
    }
}

using UnityEngine;

public class DateAndTime
{
    private int Year;
    private int Month;
    private int Day;
    private int Hour;
    private int Minute;
    private int Second;
    private bool UseCurrentTime = false;

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

    public AndroidJavaObject toAndroidJavaObject()
    {
        if (UseCurrentTime)
        {
            AndroidJavaClass zonedDateTime = new AndroidJavaClass("java.time.ZonedDateTime");
            return zonedDateTime.CallStatic<AndroidJavaObject>("now");
        } else
        {
            AndroidJavaClass zoneId = new AndroidJavaClass("java.time.ZoneId");
            AndroidJavaObject defaultZoneId = zoneId.CallStatic<AndroidJavaObject>("systemDefault");
            AndroidJavaClass zonedDateTime = new AndroidJavaClass("java.time.ZonedDateTime");
            return zonedDateTime.CallStatic<AndroidJavaObject>(
                    "of", Year, Month, Day, Hour, Minute, Second, 0, defaultZoneId
                );
        }
    }
}

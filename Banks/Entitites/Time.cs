namespace Banks.Entitites;

public class Time
{
    public Time()
    {
        DateTime time = DateTime.Now;
        CurrentTime = time;
    }

    public DateTime CurrentTime { get; internal set; }

    public void SkipTimeForDays(int days)
    {
        CurrentTime.AddDays(days);
    }

    // ss
}
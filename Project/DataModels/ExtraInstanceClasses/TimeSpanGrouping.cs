public class TimeSpanGrouping : IPairable<TimeSpan, TimeSpan>
{
    private TimeSpan _startTM;
    private TimeSpan _endTM;
    public TimeSpan StartTM
    {
        get => _startTM;
        set {_startTM = value;}
    }
    public TimeSpan EndTM
    {
        get => _endTM;
        set {_endTM = value;}
    }
    public TimeSpan Item1
    {
        get => _startTM;
        set {_startTM = value;}
    }
    public TimeSpan Item2
    {
        get => _endTM;
        set {_endTM = value;}
    }
    public TimeSpanGrouping(TimeSpan startSpan, TimeSpan endSpan)
    {
        StartTM = startSpan;
        EndTM = endSpan;
    }
}

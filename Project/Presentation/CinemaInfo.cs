public static class CinemInfo
{
    public static void CinemInformation()
    {
        ConsoleE.Clear();
        Console.WriteLine("");
        Console.WriteLine("📌   Wijnhaven 107, 3011 WN Rotterdam");
        Console.WriteLine("📞   010 794 4000");
        Console.WriteLine("🎥   3 Rooms: Auditorium 1 (150), Auditorium 2 (300), Auditorium 3 (500)");
        Console.WriteLine("🕓   Opening hours");

        DateTime today = DateTime.Today;
        DayOfWeek dayOfWeek = today.DayOfWeek;
        string closingTime;

        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
            case DayOfWeek.Tuesday:
            case DayOfWeek.Thursday:
                closingTime = "22:00";
                break;
            case DayOfWeek.Wednesday:
                closingTime = "20:00";
                break;
            case DayOfWeek.Friday:
                closingTime = "00:00";
                break;
            case DayOfWeek.Saturday:
                closingTime = "21:00";
                break;
            case DayOfWeek.Sunday:
                closingTime = "17:00";
                break;
            default:
                closingTime = "Unknown";
                break;
        }
            
        Console.WriteLine($" Closes regularily today at {closingTime} (see the schedule for precise details)");
        Console.WriteLine("");
        Console.WriteLine(@"Monday      12:00-22:00
Tuesday     12:00-22:00
Wednesday   12:00-20:00
Thursday    12:00-22:00
Friday      12:00-00:00
Saturday    13:00-21:00
Sunday      13:00-17:00");

    }
}
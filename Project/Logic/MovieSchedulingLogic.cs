using System.Globalization;
class MovieSchedulingLogic
{
    private List<MovieScheduleModel> _movieSchedule;
    public List<MovieScheduleModel> movieSchedules => _movieSchedule;

    public MovieSchedulingLogic() => _movieSchedule = MovieSchedulingAccess.LoadAll();

    private List<Tuple<TimeSpan, TimeSpan>> GetTimeRanges(List<TimeSpan> timeSlots)
    {
        var timeRanges = new List<Tuple<TimeSpan, TimeSpan>>();
        if (timeSlots.Count == 0)
            return timeRanges;

        TimeSpan startTime = timeSlots[0];
        TimeSpan endTime = timeSlots[0];

        for (int i = 1; i < timeSlots.Count; i++)
        {
            if (timeSlots[i] - timeSlots[i - 1] == TimeSpan.FromHours(2))
            {
                // If the difference between the current time slot and the previous one is 1 hour, extend the end time
                endTime = timeSlots[i];
            }
            else
            {
                // If there's a gap, add the current time range and start a new one
                timeRanges.Add(new Tuple<TimeSpan, TimeSpan>(startTime, endTime));
                startTime = timeSlots[i];
                endTime = timeSlots[i];
            }
        }

        // Add the last time range
        timeRanges.Add(new Tuple<TimeSpan, TimeSpan>(startTime, endTime));

        return timeRanges;
    }

    public Dictionary<int, List<TimeSpan>> DetermineScheduleForSpecificDayofWeek(int daysInAdvance = 0)
    {
        DateTime date = DateTime.Today.AddDays(daysInAdvance);
        DayOfWeek dayOfWeek = date.DayOfWeek;

        Dictionary<int, List<TimeSpan>> weekendTimeSlots = new Dictionary<int, List<TimeSpan>>()
        {
            {0, new List<TimeSpan>(){TimeSpan.Parse("13:00"), TimeSpan.Parse("15:00")}},
            {1, new List<TimeSpan>(){TimeSpan.Parse("15:00"), TimeSpan.Parse("17:00")}},
        };
        Dictionary<int, List<TimeSpan>> workDaysTimeSlots = new Dictionary<int, List<TimeSpan>>()
        {
            {0, new List<TimeSpan>(){TimeSpan.Parse("12:00"), TimeSpan.Parse("14:00")}},
            {1, new List<TimeSpan>(){TimeSpan.Parse("14:00"), TimeSpan.Parse("16:00")}},
            {2, new List<TimeSpan>(){TimeSpan.Parse("16:00"), TimeSpan.Parse("18:00")}},
            {3, new List<TimeSpan>(){TimeSpan.Parse("18:00"), TimeSpan.Parse("20:00")}},
        };

        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
                workDaysTimeSlots.Add(4, new List<TimeSpan>{TimeSpan.Parse("20:00"), TimeSpan.Parse("22:00")});
                return workDaysTimeSlots;
            case DayOfWeek.Tuesday:
                workDaysTimeSlots.Add(4, new List<TimeSpan>{TimeSpan.Parse("20:00"), TimeSpan.Parse("22:00")});
                return workDaysTimeSlots;
            case DayOfWeek.Wednesday:
                return workDaysTimeSlots;
            case DayOfWeek.Thursday:
                workDaysTimeSlots.Add(4, new List<TimeSpan>{TimeSpan.Parse("20:00"), TimeSpan.Parse("22:00")});
                return workDaysTimeSlots;
            case DayOfWeek.Friday:
                workDaysTimeSlots.Add(4, new List<TimeSpan>{TimeSpan.Parse("20:00"), TimeSpan.Parse("22:00")});
                workDaysTimeSlots.Add(5, new List<TimeSpan>{TimeSpan.Parse("22:00"), TimeSpan.Parse("00:00")});
                return workDaysTimeSlots;
            case DayOfWeek.Saturday:
                weekendTimeSlots.Add(2, new List<TimeSpan>{TimeSpan.Parse("17:00") - TimeSpan.Parse("19:00")});
                weekendTimeSlots.Add(3, new List<TimeSpan>{TimeSpan.Parse("19:00") - TimeSpan.Parse("21:00")});
                return weekendTimeSlots;
            default:
                return weekendTimeSlots;
        }
    }

    public void StartupUpdateList()
    {
        // For auto-increment
        int moviesCount = _movieSchedule.Count > 0 ? _movieSchedule.Max(m => m.Id) + 1 : 0;
        // Room number counter
        int roomNumber = 0;
        // To add new schedules based on how many are expired
        int expiredSchedules = 0;

        foreach (MovieScheduleModel objMovieScheduleModel in _movieSchedule)
        {
            // Remove the movies if they are at least 7 days past the current day
            if (objMovieScheduleModel.Date < DateTime.Today.AddDays(-7)) _movieSchedule.Remove(objMovieScheduleModel);
            // Otherwise add to expiredSchedules
            else if (objMovieScheduleModel.Date < DateTime.Today) expiredSchedules ++;
        }

        // 120 + 1 because the movies can be scheduled on an exact timeslot only 4 months in advance. + expiredSchedules to add new schedules.
        for (int i = moviesCount; i < 120+1 + expiredSchedules; i++)
        {
            DateTime date = DateTime.Today.AddDays(i);
            Dictionary<int, List<TimeSpan>> availableTimeSlots = DetermineScheduleForSpecificDayofWeek(i);

            foreach (var kvp in availableTimeSlots)
            {
                // kvp.Key is the room number, kvp.Value is the TimeSpan
                // Assign a unique room number for each movie
                int currentRoom = roomNumber % 3;
                roomNumber++;

                MovieLogic objMovieLogic = new MovieLogic();
                MovieModel randomMovie = objMovieLogic.SelectRandomMovie();
                MovieDetailsModel newMovieDetails = new MovieDetailsModel(randomMovie.Id);

                // Create the dictionary for the movie schedule model
                Dictionary<string, List<MovieDetailsModel>> scheduleDetails = new Dictionary<string, List<MovieDetailsModel>>();

                // Group consecutive time slots into ranges
                var timeRanges = GetTimeRanges(kvp.Value);

                // Iterate over the time ranges
                foreach (var timeRange in timeRanges)
                {
                    string key = $"{timeRange.Item1:hh\\:mm\\:ss} - {timeRange.Item2:hh\\:mm\\:ss}";
                    scheduleDetails.Add(key, new List<MovieDetailsModel> { newMovieDetails });
                }

                MovieScheduleModel newMovieScheduleModel = new MovieScheduleModel(i, currentRoom + 1, date, scheduleDetails);
                _movieSchedule.Add(newMovieScheduleModel);
                
                // Increment moviesCount
                moviesCount++;
            }
        }

        MovieSchedulingAccess.WriteAll(_movieSchedule);
    }

    public void AglorthythmicallyRescheduleList(DateTime parsedDate)
    {
        // based on an algorhythm that decides which movies are most popular
        throw new NotImplementedException("This method has not been implemented yet.");
    }

    public void RandomRescheduleList(DateTime parsedDate)
    {
        // Check if the parsed date is valid and falls within the range of scheduled dates
        if (parsedDate < DateTime.Today || parsedDate > DateTime.Today.AddDays(120))
        {
            throw new ArgumentOutOfRangeException(nameof(parsedDate), "The parsed date is not within the valid range.");
        }

        // Retrieve the existing movie schedules for the parsed date
        var schedulesForDate = _movieSchedule.Where(s => s.Date.Date == parsedDate.Date).ToList();

        // If there are no schedules for the parsed date, there's nothing to reschedule
        if (schedulesForDate.Count == 0)
        {
            Console.WriteLine("There are no schedules for the specified date to reschedule.");
            return;
        }

        // Iterate over each existing schedule for the parsed date and reshuffle the time slots
        foreach (var schedule in schedulesForDate)
        {
            // Retrieve the available time slots for the specific day of the week
            Dictionary<int, List<TimeSpan>> availableTimeSlots = DetermineScheduleForSpecificDayofWeek((int)(parsedDate - DateTime.Today).TotalDays);

            // Select a random room number
            int randomRoom = new Random().Next(1, 4); // Assuming rooms are numbered from 1 to 3
            schedule.Room = randomRoom;

            // Iterate over the schedule details and update the room number and time slots
            foreach (var scheduleDetail in schedule.Time) // Time is the movie details
            {
                // Reshuffle the time slots
                var timeRanges = GetTimeRanges(availableTimeSlots[randomRoom]);
                var newTimeRange = timeRanges[new Random().Next(timeRanges.Count)];
                scheduleDetail.Value.Clear();
                if(scheduleDetail.Value.Count > 0)
                {
                    scheduleDetail.Value.Add(new MovieDetailsModel(scheduleDetail.Value[0].Id));
                }
            }
        }

        MovieSchedulingAccess.WriteAll(_movieSchedule);
    }

    public void ManualRescheduleList(DateTime parsedDate)
    {
        throw new NotImplementedException("This method has not been implemented yet.");
    }

    public void RescheduleList(string dateInput)
    {
        int moviesCount = _movieSchedule.Count > 0 ? _movieSchedule.Max(m => m.Id) + 1 : 0;
        int roomNumber = 0;

        DateTime parsedDate;
        if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            foreach(MovieScheduleModel movieSchedule in _movieSchedule)
            {
                if (movieSchedule.Date == parsedDate) Console.WriteLine(movieSchedule);
            }
            string manualOrRandom = ConsoleE.Input("Reschedule manually [M] (enter an ID until quit), randomly [R] , or algorhythmically [A]?").ToUpper();
            if (manualOrRandom == "M") ManualRescheduleList(parsedDate);
            else if (manualOrRandom == "R") RandomRescheduleList(parsedDate);
            else if (manualOrRandom == "A") AglorthythmicallyRescheduleList(parsedDate);
            else Console.WriteLine("Invalid input");
        }
        return;
    }

    public void Print(string date)
    {
        if (date.Contains(","))
        {
            string[] dateRanges = date.Split(",");
            if (dateRanges.Length == 2) Print(dateRanges);
            else Console.WriteLine("Invalid input");
        }
        else
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                foreach(MovieScheduleModel movieSchedule in _movieSchedule)
                {
                    if (movieSchedule.Date == parsedDate) Console.WriteLine(movieSchedule);
                }
            }
            else Console.WriteLine("Invalid input");
        }
    }

    public void Print(DateTime dateTimeToday, string untilDate)
    {
        DateTime parsedDate;
        if (DateTime.TryParseExact(untilDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json", dateTimeToday, parsedDate);
        }
        else Console.WriteLine("Invalid input"); return;
    }

    public void Print(string[] dateRanges)
    {
        DateTime[] parsedDates = new DateTime[2];
        foreach (string date in dateRanges)
        {
            DateTime parsedDate;
            int i = 0;
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                parsedDates[i] = parsedDate;
            }
            else Console.WriteLine("Invalid input"); return;
        }

        SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json", parsedDates[0], parsedDates[1]);
    }
}

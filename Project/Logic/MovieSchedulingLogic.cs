using System.Globalization;
using System.Runtime.CompilerServices;
class MovieSchedulingLogic
{
    private List<MovieModel> _movies;
    private List<MovieScheduleModel> _movieSchedule;
    public List<MovieScheduleModel> movieSchedules => _movieSchedule;

    public MovieSchedulingLogic()
    {
        _movieSchedule = GenericAccess<MovieScheduleModel>.LoadAll();
        _movies = GenericAccess<MovieModel>.LoadAll();
    }

    /// RESOURCES
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

    public Dictionary<int, List<TimeSpan>> DetermineScheduleForSpecificDayOfWeek(int daysInAdvance = 0)
    {
        DateTime date = DateTime.Today.AddDays(daysInAdvance);
        return DetermineScheduleForSpecificDayOfWeek(date);
    }

    public Dictionary<int, List<TimeSpan>> DetermineScheduleForSpecificDayOfWeek(DateTime date)
    {
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
                weekendTimeSlots.Add(2, new List<TimeSpan>{TimeSpan.Parse("17:00"), TimeSpan.Parse("19:00")});
                weekendTimeSlots.Add(3, new List<TimeSpan>{TimeSpan.Parse("19:00"), TimeSpan.Parse("21:00")});
                return weekendTimeSlots;
            default:
                return weekendTimeSlots;
        }
    }

    private int DaysToAddDeterminer()  // See line 145 (`int daysToAdd = DateTime.DaysInMonth(year, month) + DaysToAddDeterminer();`)
    {
        DateTime date = DateTime.Today;
        int daysToAdd = 0;
        if (date.DayOfWeek == DayOfWeek.Sunday) /*=>*/ daysToAdd = 2;
        else if (date.DayOfWeek == DayOfWeek.Monday) /*=>*/ daysToAdd = 0;
        else if (date.DayOfWeek == DayOfWeek.Tuesday) /*=>*/ daysToAdd = 0;
        else if (date.DayOfWeek == DayOfWeek.Wednesday) /*=>*/ daysToAdd = 0;
        else if (date.DayOfWeek == DayOfWeek.Thursday) /*=>*/ daysToAdd = 0;
        else if (date.DayOfWeek == DayOfWeek.Friday) /*=>*/ daysToAdd = 0;
        else if (date.DayOfWeek == DayOfWeek.Saturday) /*=>*/ daysToAdd = 2;

        return daysToAdd;
    }
    // Made this one to hard-code determined days to test whether they'd work
    private int DaysToAddDeterminer(int dateNum)
    {
        int daysToAdd = 0;
        if (dateNum == 0) /*=>*/ daysToAdd = 2;
        else if (dateNum == 1) /*=>*/ daysToAdd = 0;
        else if (dateNum == 2) /*=>*/ daysToAdd = 0;
        else if (dateNum == 3) /*=>*/ daysToAdd = 0;
        else if (dateNum == 4) /*=>*/ daysToAdd = 0;
        else if (dateNum == 5) /*=>*/ daysToAdd = 0;
        else if (dateNum == 6) /*=>*/ daysToAdd = 2;

        return daysToAdd;
    }

    /// FINITE FUNCTIONAL LOGIC
    public void StartupUpdateList()
    {
        // For auto-increment
        int moviesSchedulesCount = _movieSchedule.Count > 0 ? _movieSchedule.Max(m => m.Id) + 1 : 0;
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
        int totalNewSchedules = (int)(120 * 3.5) + 1 + expiredSchedules;
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int daysToAdd = DateTime.DaysInMonth(year, month) + DaysToAddDeterminer();
        // 120 + 1 because the movies can be scheduled on an exact timeslot only 4 months in advance. + expiredSchedules to add new schedules.
        // i2 += 7 because a week has 7 days
        /* daysToAdd = DateTime.DaysInMonth(year, month)+2.
            Why +2? Probably because the start index is 0 and after a month the index needs to increment with 1.*/
        for (int i = moviesSchedulesCount, i2 = moviesSchedulesCount; i < totalNewSchedules; i+= daysToAdd, i2+= 7)
        {
            // To avoid skipping days equal to the amount of movies
            int daysOffSet = i;
            for (int j = 0; j < 8; j++)
            {
                DateTime date = DateTime.Today.AddDays(i2 + j);
                Dictionary<int, List<TimeSpan>> availableTimeSlots = DetermineScheduleForSpecificDayOfWeek(date);
                int lastKey = availableTimeSlots.Keys.Last();

                foreach (var kvp in availableTimeSlots)
                {
                    // kvp.Key is the room number, kvp.Value is the TimeSpan
                    // Assign a unique room number for each movie
                    int currentRoom = roomNumber % 3;
                    roomNumber++;

                    MovieLogic objMovieLogic = new MovieLogic();
                    MovieModel randomMovie = objMovieLogic.SelectRandomMovie();
                    MovieDetailsModel newMovieDetails = new MovieDetailsModel(randomMovie.Id, randomMovie.Name, randomMovie.Duration);

                    // Create the dictionary for the movie schedule model
                    Dictionary<string, MovieDetailsModel> scheduleDetails = new Dictionary<string, MovieDetailsModel>();

                    // Group consecutive time slots into ranges
                    var timeRanges = GetTimeRanges(kvp.Value);

                    // Iterate over the time ranges
                    foreach (var timeRange in timeRanges)
                    {
                        string key = $"{timeRange.Item1:hh\\:mm\\:ss} - {timeRange.Item2:hh\\:mm\\:ss}";
                        scheduleDetails.Add(key, newMovieDetails);
                    }

                    MovieScheduleModel newMovieScheduleModel = new MovieScheduleModel(daysOffSet+j, currentRoom + 1, date, scheduleDetails);
                    _movieSchedule.Add(newMovieScheduleModel);
                    
                    if (kvp.Key != lastKey) daysOffSet ++;
                }
            }
        }

        GenericAccess<MovieScheduleModel>.WriteAll(_movieSchedule);
    }

    public void RescheduleListLogic(DateTime parsedDate, string arg)
    {
        MovieLogic objMovieLogic = new MovieLogic();
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

        List<(int, int, int)> roomAndMovieIdTupleList = new(); // roomNum, movieId, scheduleId
        if (arg == "M")
        {
            string userInput = ConsoleE.Input("Please enter an id('s; comma [,] separated)");
            roomAndMovieIdTupleList = ManuallyChangeDate(userInput);
            if (roomAndMovieIdTupleList is null)
            {
                Console.WriteLine("No id's found");
                return;
            }
        }

        int moviesCount = GenericAccess<MovieModel>.LoadAll().Count;
        Random random = new Random();
        // Iterate over each existing schedule for the parsed date and reshuffle the time slots
        foreach (MovieScheduleModel scheduleModel in schedulesForDate)
        {
            // Retrieve the available time slots for the specific day of the week
            Dictionary<int, List<TimeSpan>> availableTimeSlots = DetermineScheduleForSpecificDayOfWeek((int)(parsedDate - DateTime.Today).TotalDays);

            // Select a random room number
            int randomRoom = random.Next(1, availableTimeSlots.Count); // Assuming rooms are numbered from 1 to 3
            scheduleModel.Room = randomRoom;

            // Iterate over the schedule details and update the room number and time slots
            foreach (var scheduleDetailPair in scheduleModel.TimeIdPair)
            {
                string scheduleMovieId = scheduleDetailPair.Value;
                // Reshuffle the time slots
                var timeRanges = GetTimeRanges(availableTimeSlots[randomRoom]);
                var newTimeRange = timeRanges[new Random().Next(timeRanges.Count)];
                if (arg == "M")
                {
                    foreach ((int, int, int) roomAndMovieId in roomAndMovieIdTupleList)
                    {
                        if (new int[] {1, 2, 3}.Contains(roomAndMovieId.Item1) && roomAndMovieId.Item2 >= 0 && roomAndMovieId.Item2 <= moviesCount)
                        {
                            foreach (var kvp in scheduleModel.TimeIdPair)
                            {
                                if (scheduleModel.Id == roomAndMovieId.Item3)
                                {
                                    scheduleModel.Room = roomAndMovieId.Item1;
                                    scheduleModel.TimeIdPair[kvp.Key] = $"Duration: {objMovieLogic.GetBySearch(roomAndMovieId.Item2).Duration}";
                                    scheduleModel.MovieId = roomAndMovieId.Item2;
                                }
                            }
                        }
                    }
                }
                else if (arg == "R")
                {
                    int movieId = random.Next(0, moviesCount);
                    Dictionary<string, string> updatedTimeIdPair = new Dictionary<string, string>();
                    foreach (var kvp in scheduleModel.TimeIdPair)
                    {
                        updatedTimeIdPair[kvp.Key] = $"Duration: {objMovieLogic.GetBySearch(movieId).Duration}";
                    }
                    scheduleModel.TimeIdPair = updatedTimeIdPair;
                    scheduleModel.MovieId = movieId;
                }
                else if (arg == "A")
                {
                    MovieModel popularMovie = AlgorhythmDecider.findSinglePopularMovie();
                    foreach (var kvp in scheduleModel.TimeIdPair)
                    {
                        int duration = popularMovie.Duration;
                        scheduleModel.TimeIdPair[kvp.Key] = $"Duration: {duration}";
                    }
                    scheduleModel.MovieId = popularMovie.Id;
                }
            }
        }
        Console.WriteLine("Rescheduled succesfully");
        GenericAccess<MovieScheduleModel>.WriteAll(_movieSchedule);
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
                if (movieSchedule.Date == parsedDate)
                {
                    Console.Write(movieSchedule);
                    MovieScheduleModel convertedSchedule = (MovieScheduleModel) movieSchedule;
                    foreach (var kvp in convertedSchedule.TimeIdPair) Console.WriteLine(kvp.Value);
                    Console.WriteLine();
                }
            }
            string manualOrRandom = ConsoleE.Input("Reschedule manually [M] (enter an ID until quit), randomly [R], or algorhythmically [A]?").ToUpper();
            if (manualOrRandom == "M") RescheduleListLogic(parsedDate, manualOrRandom);
            else if (manualOrRandom == "R") RescheduleListLogic(parsedDate, manualOrRandom);
            else if (manualOrRandom == "A") RescheduleListLogic(parsedDate, manualOrRandom);
            else Console.WriteLine("Invalid input (M, R, A)");
        }
        else
        {
            Console.WriteLine("Invalid date input");
        }
        return;
    }

    public List<(int, int, int)> ManuallyChangeDate(string timeStr)
    {
        timeStr = String.Concat(timeStr.Where(c => !Char.IsWhiteSpace(c)));
        bool splittable1 = true;
        bool splittable2 = false;
        foreach (char ch in timeStr)
        {
            if (!(char.IsDigit(ch) || ch == ',' || ch == ' '))
            {
                splittable1 = false;
            }
            else if (ch == ',') splittable2 = true;
        }

        if (!splittable1) return null;

        List<(int, int, int)> newRoomAndMovieIdList = new List<(int, int, int)>();
        if (splittable2) 
        {
            string[] times = timeStr.Split(",");
            for (int i = 0; i < times.Length; i++)
            {
                int timeId = Convert.ToInt16(times[i]);
                int roomNum;
                int movieId;
                string room = ConsoleE.Input("Room? Blank if same");
                string movie = ConsoleE.Input("MovieId? Blank if same.");

                if (string.IsNullOrWhiteSpace(room) || string.IsNullOrWhiteSpace(movie))
                {
                    Console.WriteLine("Invalid input: Room or movie ID cannot be blank.");
                    return null;
                }
                else if (!int.TryParse(room, out roomNum) || !int.TryParse(movie, out movieId))
                {
                    Console.WriteLine("Invalid input: Please enter valid numeric values for room and movie ID.");
                    return null;
                }
                newRoomAndMovieIdList.Add((roomNum, movieId, timeId));
            }
        }
        else
        {
            int roomNum;
            int movieId;
            Console.WriteLine("A single date it is!");
            string room = ConsoleE.Input("Room?");
            string movie = ConsoleE.Input("MovieId?");

            if (string.IsNullOrWhiteSpace(room) || string.IsNullOrWhiteSpace(movie))
            {
                Console.WriteLine("Invalid input: must not be blank or a white space");
                return null;
            }
            else if (!int.TryParse(room, out roomNum) || !int.TryParse(movie, out movieId))
            {
                Console.WriteLine("Invalid input: Please enter valid numeric values for room and movie ID.");
                return null;
            }
            newRoomAndMovieIdList.Add((roomNum, movieId, Convert.ToInt16(timeStr)));
        }
        return newRoomAndMovieIdList;
    }

    /// PRINT OVERLOADS
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
        int i = 0;
        foreach (string date in dateRanges)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                parsedDates[i] = parsedDate;
                i++;
            }
            else 
            {
                Console.WriteLine("Invalid input");
                return;
            }
        }

        SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json", parsedDates[0], parsedDates[1]);
    }
}

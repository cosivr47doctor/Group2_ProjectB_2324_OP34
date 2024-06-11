using System.Globalization;
using System.Runtime.CompilerServices;
public class MovieSchedulingLogic
{
    private List<MovieModel> _movies;
    private List<MovieScheduleModel> _movieSchedule;
    public List<MovieScheduleModel> movieSchedules => _movieSchedule;

    // To keep the autoincrement in check, distract this from _currMovieId
    private static int _skipIdInt = 0;
    // As a placeholder so that the id's don't get lost when the if- statement at line 183 returns false
    private static int _currMovieId;
    // Because you want 3 rooms for each time range (roughly like 2 hours, like 12:00-14:00), you need this as an addent to _currMovieId
    private static int _endOfTriadCounter = 0;  // module 3 (% 3)
    // As a placeholder so the roomId won't get lost at line 183
    private static int _currentRoom;
    // Same story, but used in AlgorhythymDecider which returns the timeranges
    private static TimeSpanGrouping[] _prevTimeRanges = null;

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
            case DayOfWeek.Tuesday:
            case DayOfWeek.Thursday:
                workDaysTimeSlots.Add(4, new List<TimeSpan>{TimeSpan.Parse("20:00"), TimeSpan.Parse("22:00")});
                return workDaysTimeSlots;
            case DayOfWeek.Wednesday:
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
        if (date.DayOfWeek == DayOfWeek.Sunday) /*=>*/ daysToAdd = 5;
        else if (date.DayOfWeek == DayOfWeek.Monday) /*=>*/ daysToAdd = 6;
        else if (date.DayOfWeek == DayOfWeek.Tuesday) /*=>*/ daysToAdd = 6;
        else if (date.DayOfWeek == DayOfWeek.Wednesday) /*=>*/ daysToAdd = 6;
        else if (date.DayOfWeek == DayOfWeek.Thursday) /*=>*/ daysToAdd = 6;
        else if (date.DayOfWeek == DayOfWeek.Friday) /*=>*/ daysToAdd = 6;
        else if (date.DayOfWeek == DayOfWeek.Saturday) /*=>*/ daysToAdd = 5;

        return daysToAdd;
    }
    // Made this one to hard-code determined days to test whether they'd work.
    // This test will only work until commit 5eb6afbf5e6a5b43b78790b8a9ebb5efa27215c4
    private int DaysToAddDeterminer(int dateNum)
    {
        int daysToAdd = 0;
        if (dateNum == 0) /*=>*/ daysToAdd = 5;
        else if (dateNum == 1) /*=>*/ daysToAdd = 6;
        else if (dateNum == 2) /*=>*/ daysToAdd = 6;
        else if (dateNum == 3) /*=>*/ daysToAdd = 6;
        else if (dateNum == 4) /*=>*/ daysToAdd = 6;
        else if (dateNum == 5) /*=>*/ daysToAdd = 6;
        else if (dateNum == 6) /*=>*/ daysToAdd = 5;

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
        DateTime startDate = DateTime.Now;
        int endOfWeekAddend = 0;  // For autoincrement, after a week an extra int must be added
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int daysToAdd = DateTime.DaysInMonth(year, month) + DaysToAddDeterminer();
        // 120 days + 1 because the movies can be scheduled on an exact timeslot only 3.5 months in advance. + expiredSchedules to add new schedules.
        // i2 += 7 because a week has 7 days
        for (int i = moviesSchedulesCount, i2 = moviesSchedulesCount; i < totalNewSchedules; i+= daysToAdd, i2+= 7)
        {
            // To avoid skipping days equal to the amount of movies
            int daysOffSet = i;
            for (int j = 0; j < 8; j++)
            {
                DateTime date = DateTime.Today.AddDays(i2 + j + endOfWeekAddend);
                if ((daysOffSet+j) != 0 && (daysOffSet+j) % daysToAdd == 0) {date = date.AddDays(1); endOfWeekAddend++;}
                Dictionary<int, List<TimeSpan>> availableTimeSlots = DetermineScheduleForSpecificDayOfWeek(date);
                // List<MutuablePair<TimeSpanGrouping, MovieModel>>
                int lastKey = availableTimeSlots.Keys.Last();
                int movieIndex = 0;

                foreach (var kvp in availableTimeSlots)
                {
                    // kvp.Key is the room number, kvp.Value is the TimeSpan
                    // Assign a unique room number for each movie
                    TimeSpanGrouping[] prevTimeRangesArr = new TimeSpanGrouping[3];
                    for (int k = 0; k < 3; k++)  // To add 3 rooms for each timespan
                    {
                        var list_TimeSlotId_TimeSlot_Movie = AlgorhythmDecider.SessionsBasedOnMoviesDurationDecider
                            (availableTimeSlots, date, _prevTimeRanges);
                        _currentRoom = (roomNumber % 3) + 1;  // Must be either 1, 2, or 3
                        roomNumber++;
                        _currMovieId = daysOffSet+j+k+_endOfTriadCounter-_skipIdInt;  // See notes atop file
                        if (list_TimeSlotId_TimeSlot_Movie[movieIndex].Item1 != null)
                        {
                            MovieModel randomMovie = list_TimeSlotId_TimeSlot_Movie[kvp.Key].Item2;
                            // Because of (de)serialisation issues I think. I forgot, but it is important.
                            MovieDetailsModel newMovieDetails = new MovieDetailsModel(randomMovie.Id, randomMovie.Name, randomMovie.Duration);

                            // Create the dictionary for the movie schedule model
                            Dictionary<string, MovieDetailsModel> scheduleDetails = new Dictionary<string, MovieDetailsModel>();

                            // Group consecutive time slots into ranges

                            var timeRanges = list_TimeSlotId_TimeSlot_Movie[movieIndex].Item1;
                            string key = $"{timeRanges.StartTM:hh\\:mm\\:ss} - {timeRanges.EndTM:hh\\:mm\\:ss}";
                            scheduleDetails.Add(key, newMovieDetails);

                            MovieScheduleModel newMovieScheduleModel = new MovieScheduleModel
                                (_currMovieId, _currentRoom, date, scheduleDetails);

                            _movieSchedule.Add(newMovieScheduleModel);

                            prevTimeRangesArr[k] = timeRanges;
                        }
                        else _skipIdInt++;  // For autoincrement
                        if (kvp.Key != lastKey && k == 2) daysOffSet ++;
                    }
                _prevTimeRanges = prevTimeRangesArr;
                movieIndex ++;  // Check later whether right or wrong
                _endOfTriadCounter += 2;
                }
                _prevTimeRanges = null;
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

        int moviesCount = _movies.Count();
        Random random = new Random();
        // Iterate over each existing schedule for the parsed date and reshuffle the time slots
        foreach (MovieScheduleModel scheduleModel in schedulesForDate)
        {
            // Retrieve the available time slots for the specific day of the week
            Dictionary<int, List<TimeSpan>> availableTimeSlots = DetermineScheduleForSpecificDayOfWeek((int)(parsedDate - DateTime.Today).TotalDays);
            var banaantje = AlgorhythmDecider.SessionsBasedOnMoviesDurationDecider(availableTimeSlots, parsedDate, null);

            // Iterate over the schedule details and update the room number and time slots
            foreach (var scheduleDetailPair in scheduleModel.TimeIdPair)
            {
                string scheduleMovieId = scheduleDetailPair.Value;
                // Reshuffle the time slots
                var timeRanges = GetTimeRanges(availableTimeSlots[0]);
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
                                    scheduleModel.TimeIdPair[kvp.Key] = $"Movie duration: {objMovieLogic.GetBySearch(roomAndMovieId.Item2).Duration}";
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
                        updatedTimeIdPair[kvp.Key] = $"Movie duration: {objMovieLogic.GetBySearch(movieId).Duration}";
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
                        scheduleModel.TimeIdPair[kvp.Key] = $"Movie duration: {duration}";
                    }
                    scheduleModel.MovieId = popularMovie.Id;
                }
            }
        }
        Console.WriteLine("Rescheduled succesfully");
        GenericAccess<MovieScheduleModel>.WriteAll(_movieSchedule);
    }

    public List<MovieScheduleModel> DeleteSession(string dateInput)
    {
        DateTime parsedDate;
        if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            Print(dateInput);
            int? deleteInp = ConsoleE.IntInput("Please select a session id");
            MovieScheduleModel foundSched = _movieSchedule.Where(r => r.Id == deleteInp).FirstOrDefault();
            if (foundSched != null)
            {
                _movieSchedule.Remove(foundSched);
                GenericMethods.DeleteFromList(_movieSchedule, foundSched);
                Console.WriteLine($"{string.Join(",",foundSched)}\n Session succesfully deleted");
            }
            else Console.WriteLine($"Error: did you enter a wrong id? Input: {deleteInp}");
        }
        return _movieSchedule;
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
            // Nah, you can't reshuffle the times of the sessions, you'd have to change the whole date's schedule instead
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

    private DateTime dateParser(string dateStr)
    {
        DateTime parsedDate;
        if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            return parsedDate;
        }
        return parsedDate;
    }
}

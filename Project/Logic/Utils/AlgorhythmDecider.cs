static class AlgorhythmDecider
{
    static Random random = new Random();
    static TimeSpan defInterval = TimeSpan.FromMinutes(10);

    static private int _SBOMDDC_Calls_Counter = 0; //_sessionsBasedOnMoviesDurationDeciderCallsCounter;
    static private int _currRoom = 0; // To memorise the current room
    static private TimeSpan _dayClosureTime; // Same story
    static private DateTime _date;
    static private List<MovieModel> _movies;
    static private TimeSpanGrouping[] _prevTimeRanges = null;
    static private TimeSpan[] _midnightHours = new TimeSpan[2] {new TimeSpan(0, 1, 0), new TimeSpan(3, 0, 0)};
    static private TimeSpan[] validTPs = new TimeSpan[]
        {new TimeSpan(0,-20,0), new TimeSpan(0,-10,0), new TimeSpan(0, 0, 0), new TimeSpan(0,10,0), new TimeSpan(0,20,0)};

    public static MovieModel findSinglePopularMovie()
    {
        MovieLogic objMovieLogic = new MovieLogic();
        int finalInt = 0;
      
        List<int> popularMovies = findPopularMovies();
        int totalSum = popularMovies.Sum();

        int randomValue = random.Next(0, totalSum);

        int cumulativeSum = 0;
        for (int i = 0; i < popularMovies.Count; i++)
        {
            cumulativeSum += popularMovies[i];
            if (randomValue < cumulativeSum)
            {
                finalInt = popularMovies[i];
                break;
            }
        }

        return objMovieLogic.GetBySearch(finalInt);
    }

    public static int findSinglePopularMovieInt()
    {
        int finalInt = 0;
      
        List<int> popularMovies = findPopularMovies();
        int totalSum = popularMovies.Sum();

        int randomValue = random.Next(0, totalSum);

        int cumulativeSum = 0;
        for (int i = 0; i < popularMovies.Count; i++)
        {
            cumulativeSum += popularMovies[i];
            if (randomValue < cumulativeSum)
            {
                finalInt = popularMovies[i];
                break;
            }
        }

        return finalInt;
    }

    public static List<int> findPopularMovies(List<MovieModel> moviesList = null)  // Predefined list possible, but not used for now.
    {
        if (moviesList is null) moviesList = GenericAccess<MovieModel>.LoadAll();
        List<ReservationModel> reservationsList = GenericAccess<ReservationModel>.LoadAll();

        List<int> moviesByPopularity = new();
        foreach (MovieModel movie in moviesList)
        {
            int moviePopularityCount = 0;
            foreach (ReservationModel reservation in reservationsList)
            {
                if (movie.Id == reservation.Id) moviePopularityCount ++;
            }
            moviesByPopularity.Add(moviePopularityCount);
        }

        return moviesByPopularity;
    }


    //////////
    public static List<MutablePair<TimeSpanGrouping/*session's primitive start time & end time*/, MovieModel>>
        SessionsBasedOnMoviesDurationDecider (Dictionary<int, List<TimeSpan>> sessionsOnDate, DateTime date, TimeSpanGrouping[] prevTimeRangesArr)
    {
        _SBOMDDC_Calls_Counter = (_SBOMDDC_Calls_Counter + 1) % 4;
        _currRoom = (_currRoom % 3) + 1;
        _prevTimeRanges = prevTimeRangesArr;
        _date = date;

        List<MovieModel> randomMovies = new List<MovieModel>();
        var newSessionsOnDate = new List<MutablePair<TimeSpanGrouping, MovieModel>>();  // The Dicts have a length of 1
        _movies = GenericAccess<MovieModel>.LoadAll();

        for (int i = 0; i < sessionsOnDate.Keys.Count(); i++)
        {
            MovieModel randMovie = _movies[random.Next(0, _movies.Count())];
            randomMovies.Add(randMovie);
        }
        
        for (int i = 0; i < sessionsOnDate.Count(); i++)
        {
            TimeSpan prevSchedEndTime;
            TimeSpan startTime;
            TimeSpan movieEndTime;
            TimeSpan endTime;  // After cleaning for 20 minutes

            // for instance `13:00` becomes `13:00 - 14:17` if the movie duration is an integer with value 77 (embodying minutes)
            TimeSpan movieDuration = TimeSpan.FromMinutes(randomMovies[i].Duration);
            TimeSpanGrouping timeSpansGrp;
            if (i > 0)
            {
                prevSchedEndTime = newSessionsOnDate[i-1].Item1.EndTM;
                startTime = prevSchedEndTime - TimeSpan.FromMinutes(20);
                movieEndTime = startTime + movieDuration;
                endTime = RoundUpTimeSpan(movieEndTime) + TimeSpan.FromMinutes(20);
                timeSpansGrp = new(prevSchedEndTime, endTime);
                // Console.WriteLine(prevSchedEndTime + " " + endTime);
            }
            else
            {
                startTime = sessionsOnDate[0].First();
                movieEndTime = startTime + movieDuration;
                endTime = RoundUpTimeSpan(movieEndTime) + TimeSpan.FromMinutes(20);
                timeSpansGrp = new(startTime, endTime);
                // Console.WriteLine(startTime + " " + endTime);
            }
            var timeSlotId_TimeSlot_Movie = new MutablePair<TimeSpanGrouping, MovieModel>(timeSpansGrp, randomMovies[i]);
            newSessionsOnDate.Add(timeSlotId_TimeSlot_Movie);
        }
         // newSessionsOnDate.ForEach(x => Console.WriteLine(x.Item2)); < Works
        var correctedSessionsOnDate = RandMoviesBasedOnDayOfWeek(newSessionsOnDate);
        return correctedSessionsOnDate;
        // Latest update
    }

    private static List<MutablePair<TimeSpanGrouping, MovieModel>>
        RandMoviesBasedOnDayOfWeek(List<MutablePair<TimeSpanGrouping, MovieModel>> NSOD)
    {
        // NSOD = newSessionsOnDate
        TimeSpan dayClosureTime;  // The duration of the last movie must not exceed this one
        switch (_date.DayOfWeek)
        {
            case DayOfWeek.Monday:
                dayClosureTime = TimeSpan.Parse("22:00");
                break;
            case DayOfWeek.Tuesday:
                dayClosureTime = TimeSpan.Parse("22:00");;
                break;
            case DayOfWeek.Wednesday:
                dayClosureTime = TimeSpan.Parse("20:00");
                break;
            case DayOfWeek.Thursday:
                dayClosureTime = TimeSpan.Parse("22:00");
                break;
            case DayOfWeek.Friday:
                dayClosureTime = TimeSpan.Parse("00:00");  // 23:59
                break;
            case DayOfWeek.Saturday:
                dayClosureTime = TimeSpan.Parse("21:00");
                break;
            default:
                dayClosureTime = TimeSpan.Parse("17:00");
                break;
        }
        _dayClosureTime = dayClosureTime;

        List<MutablePair<TimeSpanGrouping, MovieModel>> adjustedTimeSpans;
        if (_SBOMDDC_Calls_Counter > 1)
        {
            adjustedTimeSpans = TimeSpansAdjuster(NSOD);
            NSOD = adjustedTimeSpans;
        }
        var correctedSessionsOnDate = RecursiveTimespanBoundsChecker(NSOD, NSOD.Count()-1, dayClosureTime);
        var filteredSessionsOnDate = filterUnSchedulableSessions(correctedSessionsOnDate);
        return filteredSessionsOnDate;
    }
    
    // Recursive method to ensure the movie timespans with the same roomId won't overlap and limit the chance for the last movie duration
    // To exceed that of the closure time. `filterUnschedulableSessions` on that part completes the picure and removes all unschedulable
    // sessions.
    private static List<MutablePair<TimeSpanGrouping, MovieModel>> RecursiveTimespanBoundsChecker
        (List<MutablePair<TimeSpanGrouping, MovieModel>> NSOD, int index, TimeSpan dayClosureTime)
    {        
        if (index < 0 || index >= NSOD.Count())
        {
            return NSOD;
        }

        var MP = NSOD[index];  // MutuablePair<TimeSpanGrouping, MovieModel>

        if (NSOD[NSOD.Count()-1].Item1.EndTM <= dayClosureTime)
        {
            return NSOD;
        }

        if (!_movies.Any(m => MP.Item1.StartTM + TimeSpan.FromMinutes(m.Duration) <= dayClosureTime))
        {
            List<MovieModel> shorterMoviesCandidates = _movies.Where(m => m.Duration < MP.Item2.Duration).ToList();

            if (shorterMoviesCandidates.Count() > 0)
            {
                MovieModel randMovie = shorterMoviesCandidates[random.Next(0, shorterMoviesCandidates.Count)];
                MP.Item1.EndTM = MP.Item1.StartTM + RoundUpTimeSpan(TimeSpan.FromMinutes(randMovie.Duration + 20));
                MP.Item2 = randMovie;
                index++;
                return RecursiveTimespanBoundsChecker(NSOD, index, dayClosureTime);
            }
            index --;
            return RecursiveTimespanBoundsChecker(NSOD, index, dayClosureTime);
        }
        else
        {
            List<MovieModel> randMovieCandidates = _movies.Where(m => MP.Item1.StartTM + TimeSpan.FromMinutes(m.Duration) <= dayClosureTime).ToList();
            if (randMovieCandidates.Count() > 0)
            {
                MovieModel randMovie = randMovieCandidates[random.Next(0, randMovieCandidates.Count)];
                MP.Item1.EndTM = MP.Item1.StartTM + RoundUpTimeSpan(TimeSpan.FromMinutes(randMovie.Duration + 20));
                MP.Item2 = randMovie;
            }
            return RecursiveTimespanBoundsChecker(NSOD, index - 1, dayClosureTime);
        }
    }

    // Instead of having all rooms on the first session start at the same time (like 12:00), give the option to alter the times but not if it
    // Collides with the bounds of the days opening time, closure time, or start and/or end of a session with the same roomId.
    private static List<MutablePair<TimeSpanGrouping, MovieModel>> TimeSpansAdjuster(List<MutablePair<TimeSpanGrouping, MovieModel>> NSOD)
    {
        int randomTimeSpanIndx;
        TimeSpan randomTSPN;
        if (_SBOMDDC_Calls_Counter > 1)
        {
            for (int i = 0; i < NSOD.Count(); i++)
            {
                if (i == 0)
                {
                    randomTimeSpanIndx = random.Next(2, validTPs.Length);
                    randomTSPN = validTPs[randomTimeSpanIndx];
                    NSOD[i].Item1.StartTM += randomTSPN;     NSOD[i].Item1.EndTM = NSOD[i].Item1.EndTM.Add(randomTSPN);
                }
                else if (i == NSOD.Count()-1)
                {
                    randomTimeSpanIndx = random.Next(0, validTPs.Length-2);
                    randomTSPN = validTPs[randomTimeSpanIndx];
                    NSOD[i].Item1.StartTM = NSOD[i].Item1.StartTM.Add(randomTSPN);   NSOD[i].Item1.EndTM = NSOD[i].Item1.EndTM.Add(randomTSPN);
                }
                else
                {
                    randomTimeSpanIndx = random.Next(0, validTPs.Length);
                    randomTSPN = validTPs[randomTimeSpanIndx];
                    NSOD[i].Item1.StartTM = NSOD[i].Item1.StartTM.Add(randomTSPN);  NSOD[i].Item1.EndTM =  NSOD[i].Item1.EndTM.Add(randomTSPN);  
                }
            }
        }
        return NSOD;
    }
    
    // Final method to roughly remove all sessions that don't fit in the schedule.
    private static List<MutablePair<TimeSpanGrouping, MovieModel>> filterUnSchedulableSessions(List<MutablePair<TimeSpanGrouping, MovieModel>> NSOD)
    {
        if (NSOD != null && _prevTimeRanges != null)
        {
            NSOD.ForEach(MP =>
            {
                if (MP != null && MP.Item1 != null)
                {
                    if (_prevTimeRanges[_currRoom-1] != null)
                    {
                        if (MP.Item1.StartTM < _prevTimeRanges[_currRoom-1].EndTM) MP.Item1 = null;
                    }
                }
            });
            NSOD.ForEach(MP =>
            {
                if (MP != null && MP.Item1 != null)
                {
                    if (_date.DayOfWeek == DayOfWeek.Friday)
                    {
                        if (MP.Item1.EndTM < TimeSpan.FromHours(1)) MP.Item1 = null;
                    }
                    else
                    {
                        if (MP.Item1.StartTM > _dayClosureTime || MP.Item1.EndTM > _dayClosureTime + TimeSpan.FromMinutes(1)) MP.Item1 = null;
                    }
                }
            });
        }
        return NSOD;
    }

    // 14:17 becomes 14:20 so a session isn't ending at 14:17...
    private static TimeSpan RoundUpTimeSpan(TimeSpan time)
    {
        TimeSpan interval = defInterval;
        long ticks = (time.Ticks + interval.Ticks - 1) / interval.Ticks * interval.Ticks;
        return new TimeSpan(ticks);
    }
}

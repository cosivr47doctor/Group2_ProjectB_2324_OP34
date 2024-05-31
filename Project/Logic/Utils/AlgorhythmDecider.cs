static class AlgorhythmDecider
{
    static Random random = new Random();
    static TimeSpan defInterval = TimeSpan.FromMinutes(10);

    static private List<MovieModel> _movies;

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

    public static List<int> findPopularMovies()
    {
        List<MovieModel> moviesList = GenericAccess<MovieModel>.LoadAll();
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

    public static List<MutablePair<TimeSpanGrouping/*session's primitive start time & end time*/, MovieModel>>
        SessionsBasedOnMoviesDurationDecider (Dictionary<int, List<TimeSpan>> sessionsOnDate, DateTime date)
    {
        List<MovieModel> randomMovies = new List<MovieModel>();
        var newSessionsOnDate = new List<MutablePair<TimeSpanGrouping, MovieModel>>();  // The Dicts have a length of 1

        for (int i = 0; i < sessionsOnDate.Keys.Count(); i++)
        {
            _movies = GenericAccess<MovieModel>.LoadAll();
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
        var correctedSessionsOnDate = RandMoviesBasedOnDayOfWeek(newSessionsOnDate, date);
        return correctedSessionsOnDate;
        // Latest update
    }

    private static List<MutablePair<TimeSpanGrouping, MovieModel>>
        RandMoviesBasedOnDayOfWeek(List<MutablePair<TimeSpanGrouping, MovieModel>> NSOD, DateTime date)
    {
        // NSOD = newSessionsOnDate
        TimeSpan dayClosureTime;  // The duration of the last movie must not exceed this one
        switch (date.DayOfWeek)
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
                dayClosureTime = TimeSpan.Parse("00:00");
                break;
            case DayOfWeek.Saturday:
                dayClosureTime = TimeSpan.Parse("21:00");
                break;
            default:
                dayClosureTime = TimeSpan.Parse("17:00");
                break;
        }
        // TimeSpan dayClosurePlus20Mins = dayClosureTime + TimeSpan.FromMinutes(20);  // The duration of the last session must not exceed this one

        var correctedSessionsOnDate = RecursiveTimespanBoundsChecker(NSOD, NSOD.Count()-1, dayClosureTime);
        return correctedSessionsOnDate;
    }
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
    
    private static TimeSpan RoundUpTimeSpan(TimeSpan time)
    {
        TimeSpan interval = defInterval;
        long ticks = (time.Ticks + interval.Ticks - 1) / interval.Ticks * interval.Ticks;
        return new TimeSpan(ticks);
    }
}

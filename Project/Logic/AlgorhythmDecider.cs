static class AlgorhythmDecider
{
    static Random random = new Random();

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
}

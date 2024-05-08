static class AlgorhythmDecider
{
    static Random random = new Random();
    public static int findSinglePopularMovie()
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
        List<MovieModel> moviesList = MovieAccess.LoadAll();
        List<AccountModel> accountsList = AccountsAccess.LoadAll();

        List<int> moviesByPopularity = new();
        foreach (MovieModel movie in moviesList)
        {
            int moviePopularityCount = 0;
            foreach (AccountModel account in accountsList)
            {
                foreach (var reservation in account.Reservations)
                {
                    if (movie.Name == reservation.Name) moviePopularityCount ++;
                }
            }
        }

        return moviesByPopularity;
    }
}

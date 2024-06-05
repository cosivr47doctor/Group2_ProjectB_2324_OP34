static class Search
{
    static private MovieLogic movieLogic = new MovieLogic();


    public static void searchMovie(int accId)
    {
        Console.WriteLine("Pleas enter the movie name or id or [Q] to go back: ");
        string searchBy = Console.ReadLine();
        if (ConsoleE.BackContains(searchBy)) UserMenu.Start(accId);
        List<MovieModel> movies = movieLogic.GetAllBySearch(searchBy);

        if (movies.Count > 0)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Movie found (name): {movie.Name}");
                Console.WriteLine($"Id: {movie.Id}");
                Console.WriteLine($"Genre: {string.Join(", ", movie.Genre)}");
                Console.WriteLine($"Year: {movie.Year}");
                Console.WriteLine($"Description: {movie.Description}");
                Console.WriteLine($"Director: {movie.Director}");
                Console.WriteLine($"Duration: {movie.Duration} minutes");
                Console.WriteLine("-----------------------------------");

            }
        }
        else
        {
            Console.WriteLine("Movie not found");
            Console.WriteLine("Press enter to go back to the menu");
            Console.ReadKey();
            UserMenu.Start(accId);
        }
    }

}
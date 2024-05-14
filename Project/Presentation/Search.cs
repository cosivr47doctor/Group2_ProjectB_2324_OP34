static class Search
{
    static private MovieLogic movieLogic = new MovieLogic();


    public static void searchMovie()
    {
        Console.Clear();
        Console.WriteLine("Pleas enter the movie name or id: ");
        string searchBy = Console.ReadLine();
        MovieModel movie = movieLogic.GetBySearch(searchBy);

        if (movie != null)
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
        else
        {
            Console.WriteLine("Movie not found");
            UserMenu.Start();
        }
    }

}
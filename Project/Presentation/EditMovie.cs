public static class EditMovie
{
    static private MovieLogic movieLogic = new MovieLogic();

    public static void ChangeMovie()
    {
        ConsoleE.Clear();
        SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
        Console.WriteLine("\nPlease enter the movie ID you want to change: ");
        string searchBy = ConsoleE.Input();
        if (!ConsoleE.IsDigitsOnly(searchBy)) {Console.WriteLine($"Invalid input {searchBy}; must be integers only"); return;}
        movieLogic.ChangeMovie(searchBy);
    }

    public static void RemoveMovie()
    {
        ConsoleE.Clear();
        SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
        Console.WriteLine("\nPlease enter the movie ID you want to remove: ");
        string searchBy = ConsoleE.Input();
        if (!ConsoleE.IsDigitsOnly(searchBy)) {Console.WriteLine($"Invalid input {searchBy}; must be integers only"); return;}
        movieLogic.RemoveMovie(searchBy);
    }

    public static void CloneMovie()
    {
        ConsoleE.Clear();
        SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
        Console.WriteLine("\nPlease enter the movie ID you want to change: ");
        string searchBy = ConsoleE.Input();
        if (!ConsoleE.IsDigitsOnly(searchBy)) {Console.WriteLine($"Invalid input {searchBy}; must be integers only"); return;}
        movieLogic.CloneMovie(searchBy);
    }

    // unittest
    /*
    public static void UnitChangeMovie()
    {
        // NOTE: https://chatgpt.com/c/de2794a3-98da-43da-9e32-f452f398651e
        string searchByAndNewDetails = TestEnvironmentUtils.ReadLine();
        movieLogic.CloneMovie(searchByAndNewDetails);
    }
    */
}
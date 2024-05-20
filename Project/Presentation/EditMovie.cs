public static class EditMovie
{
    static private MovieLogic movieLogic = new MovieLogic();

    public static void ChangeMovie()
    {
        Console.Clear();
        Console.WriteLine("Please enter the movie you want to change: ");
        string searchBy = ConsoleE.Input();
        Console.WriteLine(searchBy);
        movieLogic.ChangeMovie(searchBy);
    }

    public static void RemoveMovie()
    {
        Console.Clear();
        Console.WriteLine("Please enter the movie you want to remove: ");
        string searchBy = ConsoleE.Input();
        movieLogic.RemoveMovie(searchBy);
    }

    public static void CloneMovie()
    {
        Console.Clear();
        Console.WriteLine("Please enter the movie you want to change: ");
        string searchBy = ConsoleE.Input();
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
static class EditMovie
{
    static private MovieLogic movieLogic = new MovieLogic();

    public static void ChangeMovie()
    {
        Console.WriteLine("Pleas enter the movie you want to change: ");
        string searchBy = Console.ReadLine();
        movieLogic.ChangeMovie(searchBy);
    }

    public static void RemoveMovie()
    {
        Console.WriteLine("Pleas enter the movie you want to remove: ");
        string searchBy = Console.ReadLine();
        movieLogic.RemoveMovie(searchBy);
    }
}
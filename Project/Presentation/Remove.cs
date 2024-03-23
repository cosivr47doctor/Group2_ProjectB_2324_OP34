static class Remove
{
    static private MovieLogic movieLogic = new MovieLogic();

    public static void removeMovie()
    {
        Console.WriteLine("Pleas enter the movie you want to remove: ");
        string searchBy = Console.ReadLine();
        movieLogic.RemoveMovie(searchBy);
    }
}
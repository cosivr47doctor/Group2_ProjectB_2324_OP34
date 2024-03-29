static class UserMenu
{
    static public void Start(bool isAdmin=false)
    {
        if (isAdmin) Console.WriteLine("Enter 0 to switch back to admin menu");
        Console.WriteLine("Enter 1 to make a reservation");
        Console.WriteLine("Enter 2 to see all available movies");
        Console.WriteLine("Enter 3 to search a movie");
        Console.WriteLine("Enter 4 to see reservation history");
        Console.WriteLine("Enter 5 to logout");

        string user_input = Console.ReadLine();
        switch (user_input)
        {
            case "0":
                if (isAdmin) AdminMenu.Start();
                else goto default;
                break;
            case "1":
                Console.WriteLine("Reservation function");
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "2":
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "3":
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "4":
                Console.WriteLine("Reservation history function");
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "5":
                Menu.Start();
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }



    }
}
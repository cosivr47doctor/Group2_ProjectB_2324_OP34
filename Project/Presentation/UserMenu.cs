static class UserMenu
{
    static public void Start(int accId=0, bool isAdmin=false)
    {
        if (isAdmin) Console.WriteLine("Enter 0 to switch back to admin menu");
        Console.WriteLine("Enter 1 to logout\n");
        Console.WriteLine("Enter 2 to make a reservation");
        Console.WriteLine("Enter 3 to see all available movies");
        Console.WriteLine("Enter 4 to search a movie");
        Console.WriteLine("Enter 5 to see reservation history");

        string user_input = Console.ReadLine();
        switch (user_input)
        {
            case "0":
                if (isAdmin) AdminMenu.Start(accId);
                else goto default;
                break;
            case "1":
                Menu.Start();
                break;
            case "3":
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "4":
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "5":
                Console.WriteLine("Reservation history function");
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "2":
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("Enter the id of the movie you want to reserve:");
                string movieToReserve = Console.ReadLine();
                Reservation.ReserveMovie(movieToReserve, accId);
                Console.WriteLine($"Successfully reserved the movie: {movieToReserve}");
                
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId);
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }



    }
}
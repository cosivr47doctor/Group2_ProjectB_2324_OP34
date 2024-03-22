static class UserMenu
{
    static public void  Start()
    {
        Console.WriteLine("Enter 1 to make a reservation");
        Console.WriteLine("Enter 2 to see all available movies");
        Console.WriteLine("Enter 3 to search a movie");
        Console.WriteLine("Enter 4 to see reservation history");
        Console.WriteLine("Enter 5 to logout");

        string user_input = Console.ReadLine();
        switch (user_input)
        {
            case "1":
                Console.WriteLine("Reservation function");
                break;
            case "2":
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "3":
                Console.WriteLine("search function");
                break;
            case "4":
                Console.WriteLine("Reservation history function");
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
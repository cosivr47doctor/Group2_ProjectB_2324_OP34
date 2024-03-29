static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        AccountsLogic objAccountsLogic = new AccountsLogic(); objAccountsLogic.StartupUpdateList();
        Console.WriteLine("Enter [Q] to quit program");
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to register a new user");
        Console.WriteLine("Enter 3 to see all available movies");
        Console.WriteLine("Enter 4 to see cinema informations");

        string input = Console.ReadLine();
        if (input.ToLower() == "q" || input.ToLower() == "[q]")
        {
            return;
        }
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Adding.addUser(objAccountsLogic);
            Console.WriteLine("User registered successfully!");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();
        }
        else if (input == "3")
        {
            SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();

        }
        else if (input == "4")
        {
            Console.WriteLine("");
            Console.WriteLine("📌   Wijnhaven 107, 3011 WN Rotterdam");
            Console.WriteLine("📞   010 794 4000");
            Console.WriteLine("🎥   3 Rooms: Auditorium 1 (150), Auditorium 2 (300), Auditorium 3 (500)");
            Console.WriteLine("🕓   Opening hours");

            DateTime today = DateTime.Today;
            DayOfWeek dayOfWeek = today.DayOfWeek;
            string closingTime;

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Thursday:
                    closingTime = "22:00";
                    break;
                case DayOfWeek.Wednesday:
                    closingTime = "20:00";
                    break;
                case DayOfWeek.Friday:
                    closingTime = "00:00";
                    break;
                case DayOfWeek.Saturday:
                    closingTime = "21:00";
                    break;
                case DayOfWeek.Sunday:
                    closingTime = "17:00";
                    break;
                default:
                    closingTime = "Unknown";
                    break;
            }
            
            Console.WriteLine($" Closes today at {closingTime}");
            Console.WriteLine("");
            Console.WriteLine(@"Monday      12:00-22:00
Tuesday     12:00-22:00
Wednesday   12:00-20:00
Thursday    12:00-22:00
Friday      12:00-00:00
Saturday    13:00-21:00
Sunday      13:00-17:00");

            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();

        }
        else if (input == "5")
        {
            Console.WriteLine("This feature is not yet implemented");
            //Adding.addFood();
            //Adding.addMovie();
            //Search.searchMovie();
            //Remove.removeMovie();

            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }
    }
}
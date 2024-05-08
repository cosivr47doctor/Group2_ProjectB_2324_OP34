using System.Globalization;
static class UserMenu
{
    static public void Start(int accId=0, bool isAdmin=false)
    {
        if (isAdmin) Console.WriteLine("Enter 0 to switch back to admin menu");
        Console.WriteLine("Enter 1 to logout");
        Console.WriteLine("Enter 2 to make a reservation");
        Console.WriteLine("Enter 3 to order some food");
        Console.WriteLine("Enter 4 to see all available movies");
        Console.WriteLine("Enter 5 to see the movies schedule");
        Console.WriteLine("Enter 6 to search a movie");
        Console.WriteLine("Enter 7 to see reservation history");

        string user_input = Console.ReadLine();
        switch (user_input)
        {
            case "0":
                if (isAdmin) AdminMenu.Start(accId);
                else goto default;
                break;
            case "1":
                MainMenu.Start();
                break;
            case "2":
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                //Reservation.ReserveMovie(accId);
                AddReservation.addMovieResv();

                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId);
                break;
            case "3":
                SeeJsons.PrintFoodJson(@"DataSources/food.json");
                //Reservation.ReserveFood(accId);

                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();

                break;
            case "4":
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "5":
                MovieSchedulingLogic objMovieSchedulingLogic = new();
                Console.WriteLine(@"Make a choice:
See entire schedule [0]
See schedule for a specific date [1]
See schedule for up until a specific date [2]
See schedule for specific range of dates [3]");
                string seeScheduleInput = Console.ReadLine();
                switch (seeScheduleInput)
                {
                    case "0":
                        SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json");
                        break;
                    case "1":
                        string specificDateInput = ConsoleE.Input("Which date? (yyyy-MM-dd)");
                        objMovieSchedulingLogic.Print(specificDateInput);
                        Start();
                        break;
                    case "2":
                        string untilSpecificDateInput = ConsoleE.Input("Until which date? (yyyy-MM-dd)");
                        objMovieSchedulingLogic.Print(untilSpecificDateInput);
                        Start();
                        break;
                    case "3":
                        string dateRangesInput = ConsoleE.Input("Which dates? (yyyy-MM-dd); comma separated (,)");
                        objMovieSchedulingLogic.Print(dateRangesInput);
                        Start();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Start();
                        break;
                }
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "6":
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "7":
                Console.WriteLine("Reservation history function");
                Console.WriteLine("");
                ResvDetails.ResvHistory(accId);
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }



    }
}
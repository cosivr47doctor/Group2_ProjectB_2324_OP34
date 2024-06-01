using System.Globalization;
static class UserMenu
{
    static public void Start(int accId=0, bool isAdmin=false)
    {
        if (accId < 0) {Console.WriteLine("Invalid acc id"); Thread.Sleep(500); MainMenu.Start();}
        
        GenericMethods.Reload();
            List<string> options = new(){
                "Make a reservation",
                //"Order some food",
                "Cancel a reservation",
                "See all available movies",
                "See the movies schedule",
                "Search a movie",
                "See reservation history",
                "Logout"
            };
        

        if (isAdmin)
        {
            options.Add("Enter admin menu");
        }

        int selectedOption = DisplayUtil.MenuDisplay(options);
        switch (selectedOption)
        {
            case 0:
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                AddReservation.addMovieResv(accId);

                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId, isAdmin);
                break;
            // case 1:
            //     SeeJsons.PrintFoodJson(@"DataSources/food.json");
            //     //Reservation.ReserveFood(accId);

            //     Console.WriteLine("");
            //     Console.WriteLine("Press enter to go back.");
            //     Console.ReadLine();

            //     Start(accId, isAdmin);
            //     break;

            /*case 1: // If "Cancel a reservation" is the second option
                Console.WriteLine("");
                bool validInput = false;
                while (!validInput)
                {
                    Console.WriteLine("Enter the reservation code of the reservation you want to cancel:");
                    string input = Console.ReadLine();
                    int sessionId;
                    if (int.TryParse(input, out sessionId))
                    {
                        AddReservation.CancelReservation(accId, sessionId);
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a a valid code.");
                    }
                }
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId, isAdmin);
                break;*/

            case 1: 
            Console.WriteLine("");
            AddReservation.CancelReservation(accId);
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start(accId, isAdmin);
            break;


            case 2:
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId, isAdmin);
                break;
            case 3:
                MovieSchedulingLogic objMovieSchedulingLogic = new();
                List<string> scheduleOptions = new(){
                "See entire schedule",
                "See schedule for a specific date",
                "See schedule for up until a specific date",
                "See schedule for specific range of dates "
                };

                int seeScheduleInput = DisplayUtil.MenuDisplay(scheduleOptions);
                switch (seeScheduleInput)
                {
                    case 0:
                        SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json");
                        break;
                    case 1:
                        string specificDateInput = ConsoleE.Input("Which date? (yyyy-MM-dd)");
                        objMovieSchedulingLogic.Print(specificDateInput);
                        Console.WriteLine("Press enter to go back to the option menu");
                        Console.ReadLine();
                        Start(accId, isAdmin);
                        break;
                    case 2:
                        string untilSpecificDateInput = ConsoleE.Input("Until which date? (yyyy-MM-dd)");
                        objMovieSchedulingLogic.Print(DateTime.Today, untilSpecificDateInput);
                        Console.WriteLine("Press enter to go back to the option menu");
                        Console.ReadLine();
                        Start(accId, isAdmin);
                        break;
                    case 3:
                        string dateRangesInput = ConsoleE.Input("Which dates? (yyyy-MM-dd); comma separated (,)");
                        objMovieSchedulingLogic.Print(dateRangesInput);
                        Console.WriteLine("Press enter to go back to the option menu");
                        Console.ReadLine();
                        Start(accId, isAdmin);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        Start(accId, isAdmin);
                        break;
                }
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId, isAdmin);
                break;
            case 4:
                Search.searchMovie();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId, isAdmin);
                break;
            case 5:
                Console.ResetColor();
                Console.WriteLine("Reservation history function");
                Console.WriteLine("");
                ResvDetails.ResvHistory(accId);
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId, isAdmin);
                break;
            case 6:
                Console.ResetColor();
                MainMenu.Start();
                break;
            case 7:
                if (isAdmin) 
                {
                    Console.ResetColor();
                    AdminMenu.Start(accId);
                }
                else 
                {
                    Console.ResetColor();
                    Console.WriteLine("I don't think you have the facilities for that big man");
                    Thread.Sleep(1000);
                    Start(accId, isAdmin);
                }
                break;
            default:
                Console.WriteLine("Invalid input");
                Start(accId, isAdmin);
                break;
        }
    }
}
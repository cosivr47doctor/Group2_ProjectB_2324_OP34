using System.Globalization;
static class UserMenu
{
    static public void Start(int accId=0, bool isAdmin=false)
    {
        if (accId < 0) {Console.WriteLine("Invalid acc id"); Thread.Sleep(500); MainMenu.Start();}
        
        GenericMethods.Reload();
            List<string> options = new(){
                "Make a reservation",
                "Manage reservations",
                "See all available movies",
                "See the movies schedule",
                "Search a movie",
                "Logout"
            };
        

        AccountModel acc = GenericAccess<AccountModel>.LoadAll().Find(a => a.Id == accId);
        if (isAdmin || acc.isAdmin)
        {
            options.Add("Enter admin menu");
        }

        int selectedOption = DisplayUtil.MenuDisplay(options, "DisplayHeader");
        ConsoleE.Clear();
        if (selectedOption == 0)
        {
            SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
            Console.WriteLine("");
            AddReservation.addMovieResv(accId);

            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start(accId, isAdmin);
        }
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

        else if (selectedOption == 1)
        {
            ConsoleE.Clear();
            Console.WriteLine("");
            AccountsLogic.AccReservations(accId);
            AddReservation.CancelReservation(accId);
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start(accId, isAdmin);
        }
        else if (selectedOption == 2)
        {
            SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
            Console.WriteLine("");
            ConsoleE.Input("Press [Q] to go back.");
            Start(accId, isAdmin);
        }
        else if (selectedOption == 3)
        {
            MovieSchedulingLogic objMovieSchedulingLogic = new();
            List<string> scheduleOptions = new(){
            "See entire schedule",
            "See today's schedule",
            "See schedule for a specific date",
            "See schedule for up until a specific date",
            "See schedule for specific range of dates "
            };

            int seeScheduleInput = DisplayUtil.MenuDisplay(scheduleOptions);
            if (seeScheduleInput == 0)
            {
                SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json");
            }
            else if (seeScheduleInput == 1)
            {
                SeeJsons.PrintSchedulesJson("@DateSources/movieSessions.json", DateTime.Today, DateTime.Today);
            }
            else if (seeScheduleInput == 2)
            {
                string specificDateInput = ConsoleE.Input("Which date? (yyyy-MM-dd)");
                objMovieSchedulingLogic.Print(specificDateInput);
                Console.WriteLine("Press enter to go back to the option menu");
                Console.ReadLine();
                Start(accId, isAdmin);
            }
            else if (seeScheduleInput == 3)
            {
                string untilSpecificDateInput = ConsoleE.Input("Until which date? (yyyy-MM-dd)");
                objMovieSchedulingLogic.Print(DateTime.Today, untilSpecificDateInput);
                Console.WriteLine("Press enter to go back to the option menu");
                Console.ReadLine();
                Start(accId, isAdmin);
            }
            else if (seeScheduleInput == 4)
            {
                string dateRangesInput = ConsoleE.Input("Which dates? (yyyy-MM-dd); comma separated (,)");
                objMovieSchedulingLogic.Print(dateRangesInput);
                Console.WriteLine("Press enter to go back to the option menu");
                Console.ReadLine();
                Start(accId, isAdmin);
            }
            else
            {
                Console.WriteLine("Invalid input");
                Start(accId, isAdmin);
            }
            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start(accId, isAdmin);
        }
        else if (selectedOption == 4)
        {
            Search.searchMovie(accId);
            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start(accId, isAdmin);
        }
        else if (selectedOption == 5)
        {
            Console.ResetColor();
            MainMenu.Start();
        }
        else if (selectedOption == 6)
        {
            if (isAdmin) 
            {
                Console.ResetColor();
                AdminMenu.Start(accId);
            }
            else 
            {
                Console.ResetColor();
                Console.WriteLine("Error: no admin rights on account found.");
                Thread.Sleep(1000);
                Start(accId, isAdmin);
            }
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start(accId, isAdmin);

        }
    }
}
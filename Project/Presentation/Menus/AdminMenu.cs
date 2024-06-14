static class AdminMenu
{
    static List<string> options = new(){
    "Switch to user menu",
    "Logout",
    "Add food to the menu",
    "Add/Edit/remove/clone a movie",
    "Manage sessions",
    "Access extra options",
    };

    static List<string> extraOptions = new(){
    "Go back",
    "Test automatic reservation with the dummy account",
    "Test email with dummy account",
    "Search in all movies, sessions and food"
    };

    static List<string> movieEditor = new(){
    "Go back",
    "Add movie",
    "Edit movie",
    "Remove movie",
    "Clone movie",
    };
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start(int accId=-1)
    {
        GenericMethods.Reload();
        if (accId < 0)
        {
            Console.WriteLine("Not logged in");
            MainMenu.Start();
        }

        MovieSchedulingLogic objMovieSchedulingLogic = new MovieSchedulingLogic();

        Console.WriteLine("WELCOME BACK ADMIN👋");
        Console.WriteLine(" -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -");
        int selectedOption = DisplayUtil.Display(options, "DisplayHeader");
        Console.WriteLine(" -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -\n");


        if (selectedOption == 0)
        {

            UserMenu.Start(accId, true);
        }
        else if (selectedOption == 1)
        {
            MainMenu.Start();
        }
        else if (selectedOption == 2)
        {
            Adding.addFood(accId);
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start(accId);
        }
        else if (selectedOption == 3)
        {
            int movieEditorOptions = DisplayUtil.Display(movieEditor);
            if (movieEditorOptions == 0)
            {
                Start(accId);
            }
            else if (movieEditorOptions == 1)
            {
                Adding.addMovie(accId);
                Start(accId);
            }
            else if (movieEditorOptions == 2)
            {
                EditMovie.ChangeMovie();
                Start(accId);
            }
            else if (movieEditorOptions == 3)
            {
                EditMovie.RemoveMovie();
                Start(accId);
            }
            else if (movieEditorOptions == 4)
            {
                EditMovie.CloneMovie();
                Start(accId);
            }
        }
        else if (selectedOption == 4)
        {
            string dateInput = ConsoleE.Input("Enter a date (yyyy-MM-dd) or [Q] to go back");
            if (ConsoleE.BackContains(dateInput)) Start(accId);
            // string rescheduleOrDelete = ConsoleE.Input("Reschedule [R] or Delete [D] a session or date?");
            // if (new[]{"D", "d", "Delete", "delete"}.Contains(rescheduleOrDelete)) objMovieSchedulingLogic.DeleteSession(dateInput);
            objMovieSchedulingLogic.DeleteSession(dateInput);
            ConsoleE.Input("Press enter to go back", true);
            Start(accId);
        }
        else if (selectedOption == 5)
        {
            Console.WriteLine("Enter [Q] to go back");
            int extraInput = DisplayUtil.Display(extraOptions);
            if (extraInput == 0)
            {
                Start(accId);
            }
            else if (extraInput == 1)
            {
                AddReservation.addMovieResv(accId, 3);
                Console.WriteLine("Dummy movie succesfully added to reservations. Press enter to go back.");
                Console.ReadLine();
                Start(accId);
            }
            else if (extraInput == 2)
            {
                ReservationModel newReservation = new ReservationModel("ABC123", 3, 0, 7, "0, 0, 0", new[] {"null", "0"}, 0, DateTime.Now) {Id = 0};
                EmailConf.GenerateEmailBody(3, newReservation);
                Start(accId);
            }
            else if (extraInput == 3)
            {
                string searchBy = ConsoleE.Input("Please enter a string argument to search from");
                CombinedInfo.PrintEachModelContainingInfo(searchBy);
                ConsoleE.Input("Press enter to go back", true);
                Start(accId);
            }
        }
    }
}

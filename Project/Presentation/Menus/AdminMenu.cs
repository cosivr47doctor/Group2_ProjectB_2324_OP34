static class AdminMenu
{
    static List<string> options = new(){
    "Switch to user menu",
    "Logout",
    "Add food to the menu",
    "Add/Edit/remove/clone a movie",
    "Reschedule a date",
    "Access extra options",
    };

    static List<string> extraOptions = new(){
    "Go back",
    "Test automatic reservation with the dummy account",
    "Test email with dummy account",
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
        int selectedOption = DisplayUtil.Display(options);
        Console.WriteLine(" -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -\n");


        switch (selectedOption)
        {
            case 0:
                UserMenu.Start(accId, true);
                break;
            case 1:
                MainMenu.Start();
                break;
/*
            case 2:
                AccountsLogic obj_AccountsLogic = new();
                string findByInput = ConsoleE.Input("Please submit the ID or Email address of a user, or [Q] to go back");
                if (ConsoleE.BackContains(findByInput)) Start(accId);
                if (obj_AccountsLogic.GetByArg(findByInput) != null)
                {
                    Console.WriteLine(@"What would you like to do?
[1] ban user
[2] unban user
[3] promote to admin
[4] depromote to user");
                    string change_user_status_input = Console.ReadLine();
                    if (Enumerable.Range(1, 4).Contains(Convert.ToInt16(change_user_status_input)))
                    {
                        obj_AccountsLogic.ChangeUserStatus(change_user_status_input, findByInput, accId);
                    }
                }
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId);
                break;
*/
            case 2:
                Adding.addFood(accId);
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start(accId);
                break;
            case 3:
                int movieEditorOptions = DisplayUtil.Display(movieEditor);
                switch (movieEditorOptions)
                {
                    case 0:
                        Start(accId);
                        break;
                    case 1:
                        Adding.addMovie(accId);
                        break;
                    case 2:
                        EditMovie.ChangeMovie();
                        break;
                    case 3:
                        EditMovie.RemoveMovie();
                        break;
                    case 4:
                        EditMovie.CloneMovie();
                        break;
                }
                break;
            case 4:
                string dateInput = ConsoleE.Input("Enter a date (yyyy-MM-dd) or [Q] to go back");
                if (ConsoleE.BackContains(dateInput)) Start(accId);
                objMovieSchedulingLogic.RescheduleList(dateInput);
                ConsoleE.Input("Press enter to go back", true);
                Start(accId);
                break;
            case 5:
                Console.WriteLine("Enter [Q] to go back");
                Console.WriteLine("Enter `TR` to test the TEST_RESERVE function that will automatically add a reservation to the dummy account");
                int extraInput = DisplayUtil.Display(extraOptions);
                switch (extraInput)
                {
                    case 0:
                        break;
                    case 1:
                        AddReservation.addMovieResv(accId, 3);
                        Console.WriteLine("Dummy movie succesfully added to reservations. Press enter to go back.");
                        Console.ReadLine();
                        Start(accId);
                        break;
                    case 2:
                        
                        ReservationModel newReservation = new ReservationModel("ABC123", 3, 0, 7, "0, 0, 0", new[] {"null", "0"}, 0, DateTime.Now);
                        newReservation.Id = 0;
                        EmailConf.GenerateEmailBody(3, newReservation);
                        Start(accId);
                        break;
                }
                break;
            //{}
        }
    }
}

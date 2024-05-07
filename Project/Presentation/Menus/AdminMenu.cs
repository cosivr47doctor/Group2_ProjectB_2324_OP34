static class AdminMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start(int accId=0)
    {
        AccountsLogic objAccountsLogic = new AccountsLogic(); objAccountsLogic.StartupUpdateList();
        
        Console.WriteLine(" -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -");
        Console.WriteLine("Welcom back admin 👋");
        Console.WriteLine("Enter 0 to switch to user menu");
        Console.WriteLine("Enter 1 to logout\n");

        Console.WriteLine("Enter 2 to change the status of a user");
        Console.WriteLine("Enter 3 to add food to the menu");
        Console.WriteLine("Enter 4 to add a movie");
        Console.WriteLine("Enter 5 to edit/remove a movie");
        Console.WriteLine("Enter 6 to reschedule a date");

        Console.WriteLine(" -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -\n");


        string user_input = Console.ReadLine();
        switch (user_input)
        {
            case "0":
                UserMenu.Start(accId, true);
                break;
            case "1":
                MainMenu.Start();
                break;
            case "2":
                AccountsLogic obj_AccountsLogic = new();
                Console.WriteLine("Please submit the ID or Email address of a user");
                string find_by_input = Console.ReadLine();
                if (obj_AccountsLogic.GetByArg(find_by_input) != null)
                {
                    Console.WriteLine(@"What would you like to do?
[1] ban user
[2] unban user
[3] promote to admin
[4] depromote to user");
                    string change_user_status_input = Console.ReadLine();
                    if (Enumerable.Range(1, 4).Contains(Convert.ToInt16(change_user_status_input)))
                    {
                        obj_AccountsLogic.ChangeUserStatus(change_user_status_input, find_by_input);
                    }
                }
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();

                break;
            case "3":
                Adding.addFood();
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "4":
                Adding.addMovie();
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "5":
                Console.WriteLine("Want to change (1) or remove (2) a movie?");
                string editOrRemove = Console.ReadLine();
                if (new[] { "1", "change" }.Contains(editOrRemove))
                {
                    EditMovie.ChangeMovie();
                }
                else if (new[] { "2", "remove" }.Contains(editOrRemove))
                {
                    EditMovie.RemoveMovie();
                }
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case "6":
                MovieSchedulingLogic objMovieSchedulingLogic = new MovieSchedulingLogic();
                string dateInput = ConsoleE.Input("Enter a date (yyyy-MM-dd)");
                objMovieSchedulingLogic.RescheduleList(dateInput);
                ConsoleE.Input("Press enter to go back", true);
                Start();
                break;
            case "TEST_RESERVE":
                // Console.WriteLine("Please enter the name of the movie you want to reserve");  // Continues to show all movies with that title or similar
                Reservation.ReserveMovie();
                Console.WriteLine("Dummy movie succesfully added to reservations. Press enter to go back.");
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
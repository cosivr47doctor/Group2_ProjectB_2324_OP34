static class AdminMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("Enter 1 to logout");
        Console.WriteLine("Enter 2 to do something else in the future");  // UNFINISHED
        Console.WriteLine("Enter 3 to change the status of a user");
        Console.WriteLine("Enter 4 to add food to the menu");
        Console.WriteLine("Enter 5 to add a movie");


        string user_input = Console.ReadLine();
        switch (user_input)
        {
            case "1":
                Menu.Start();
                break;
            case "2":
                Console.WriteLine("This feature is not yet implemented");
                break;
            case "3":
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
                break;
            case "4":
                Adding.addFood();
                break;
            case "5":
                Adding.addMovie();
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }

    }
}

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
                    // 
                }
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }

    }
}

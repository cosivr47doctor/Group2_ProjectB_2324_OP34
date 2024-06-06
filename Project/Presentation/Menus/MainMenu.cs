static class MainMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        GenericMethods.Reload();
        MovieSchedulingLogic objMovieScheduling = new MovieSchedulingLogic(); objMovieScheduling.StartupUpdateList();
        AccountsLogic objAccountsLogic = new AccountsLogic();

        List<string> options = new(){
            "Login",
            "Register",
            "See available movies",
            "See cinema information",
            "Quit program",
            // "Test room seats"
        };

        int selectedOption = DisplayUtil.MenuDisplay(options);
        if (selectedOption == 0)
        {
            UserLogin.Start();
        }
        else if (selectedOption == 1)
        {
            Adding.addUser(objAccountsLogic);
            Console.WriteLine("User registered successfully!");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();
        }
        else if (selectedOption == 2)
        {
            SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();
        }
        else if (selectedOption == 3)
        {
            CinemInfo.CinemInformation();
            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();
        }
        else if (selectedOption == 4)
        {
            Console.ResetColor();
            Environment.Exit(0);
        }
        else throw new Exception("error");
    }
}
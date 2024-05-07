static class MainMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        MovieSchedulingLogic objMovieScheduling = new MovieSchedulingLogic(); objMovieScheduling.StartupUpdateList();
        AccountsLogic objAccountsLogic = new AccountsLogic(); objAccountsLogic.StartupUpdateList();

        List<string> options = new(){
            "Login",
            "Register",
            "See available movies",
            "See cinema information",
            "Quit program",
            "Test room seats"
        };

        int selectedOption = DisplayUtil.Display(options);
        switch (selectedOption)
        {
            case 0:
                UserLogin.Start();
                break;
            case 1: 
                Adding.addUser(objAccountsLogic);
                Console.WriteLine("User registered successfully!");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case 2:
                SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case 3:
                CinemInfo.CinemInformation();
                Console.WriteLine("");
                Console.WriteLine("Press enter to go back.");
                Console.ReadLine();
                Start();
                break;
            case 4:
                break;
            case 5:
                RoomSeats.Room1();
                break;
            default:
                throw new Exception("error");
        }
    }
}
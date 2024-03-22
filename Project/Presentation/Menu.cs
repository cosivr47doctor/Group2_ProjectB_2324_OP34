static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        AccountsLogic objAccountsLogic = new AccountsLogic(); objAccountsLogic.StartupUpdateList();
        Console.WriteLine("Enter [Q] to quit program");
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to do something else in the future");
        Console.WriteLine("enter 3 to see all available movies");
        Console.WriteLine("Enter 4 to see cinema informations");

        string input = Console.ReadLine();
        if (input.ToLower() == "q" || input.ToLower() == "[q]")
        {
            return;
        }
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Console.WriteLine("This feature is not yet implemented");
            //Adding.addFood();
            //Adding.addMovie();
        }
        else if (input == "3")
        {
            SeeJsons.PrintMoviesJson(@"DataSources/movies.json");
            Console.WriteLine("");
            Console.WriteLine("Press enter to go back.");
            Console.ReadLine();
            Start();

        }
        else if (input == "4")
        {
            Console.WriteLine("");
            Console.WriteLine("📌   Wijnhaven 107, 3011 WN in Rotterdam");
            Console.WriteLine("📞   0611DeRestVerzinJeZelf");
            Console.WriteLine("🎥   3 Rooms: Auditorium 1 (150), Auditorium 2 (300), Auditorium 3 (500)");
            Console.WriteLine("🕓   Openingstijden");
            Console.WriteLine("");
            Console.WriteLine(" Sluit vandaag om 22:00\nMaandag      12:00-22:00\nDinsdag      12:00-22:00\nWoensdag     12:00-22:00\nDonderdag    12:00-22:00\nVrijdag      12:00-22:00\nZaterdag     13:00-22:00\nZondag       13:00-22:00\n");
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }


    }
}
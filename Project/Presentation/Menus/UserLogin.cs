static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        // ConsoleE.Clear();
        Console.WriteLine(@"
  _                _         ____                  
 | |    ___   __ _(_)_ __   |  _ \ __ _  __ _  ___ 
 | |   / _ \ / _` | | '_ \  | |_) / _` |/ _` |/ _ \
 | |__| (_) | (_| | | | | | |  __/ (_| | (_| |  __/
 |_____\___/ \__, |_|_| |_| |_|   \__,_|\__, |\___|
             |___/                      |___/                                                                                                                        
");        
        Console.WriteLine("Welcome to the login page\n enter [Q] to go back\n");
        Console.Write("Please enter your email address: ");
        string email = Console.ReadLine();
        if (ConsoleE.BackContains(email)) MainMenu.Start();
        Console.WriteLine("Please enter your password");
        string password = PasswordInput.InputPassword();
        if (ConsoleE.BackContains(password)) MainMenu.Start();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email is " + acc.EmailAddress);
            ConsoleE.Input("\nPlease press enter to continue", true);

            //Write some code to go back to the menu
            //Menu.Start();
            if (acc.isAdmin)
            {
                AdminMenu.Start(acc.Id);
            }
            else
            {
                UserMenu.Start(acc.Id);
            }
        }
    }
}
static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        AccountsLogic objAccountsLogic = new AccountsLogic(); objAccountsLogic.StartupUpdateList();
        
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            //Write some code to go back to the menu
            //Menu.Start();
            if (acc.isAdmin)
            {
                AdminMenu.Start();
            }
            else
            {
                UserMenu.Start();
            }
        }
    }
}
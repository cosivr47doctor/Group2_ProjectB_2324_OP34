using System.Text.Json;

static class UserRegister
{

    public static void addUser(AccountsLogic accountsLogic)
    {

        Console.WriteLine("Welcome to the registration page");
        Console.WriteLine("Enter your emailaddress: ");
        string emailAddress = Console.ReadLine();
        Console.WriteLine("Enter your password");
        string password = Console.ReadLine();
        Console.WriteLine("Enter your fullname");
        string fullName = Console.ReadLine();

        string HashedPassword = PasswordHasher.HashPassword(password);

        AccountModel newAccount = new AccountModel(0 , emailAddress, HashedPassword, fullName);

        accountsLogic.UpdateList(newAccount);
    }
}
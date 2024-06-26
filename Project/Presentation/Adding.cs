using System.Text.Json;
using System.Text.RegularExpressions;

public static class Adding
{
    public static void addFood(int accId=-1)
    {
        ConsoleE.Clear();
        SeeJsons.PrintFoodJson(@"DataSources/food.json");
        Console.WriteLine("\n^^^ These are the existing food items ^^^\n");
        //need id
        Console.WriteLine("Add food to the menu:");
        Console.WriteLine("Please enter the name of the food item or [Q] to go back");
        string name = Console.ReadLine();
        if (ConsoleE.BackContains(name)) AdminMenu.Start(accId);

        decimal price;
        while (true)
        {
            Console.WriteLine("Please enter the price of the food item");
            string priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out price))
            {
                break;
            }
            else
            {   
                Console.WriteLine("Invalid input. Please enter a valid price.");
            }
        }
        
        FoodModel foodItem = new FoodModel(name, price);
        GenericMethods.UpdateList(foodItem);
        Thread.Sleep(1000);

    }

    public static void addMovie(int accId)
    {
        ConsoleE.Clear();
        Console.WriteLine("Add movie to the cinema:");
        Console.WriteLine("Please enter the name of the movie or [Q] to go back");
        string name = Console.ReadLine();
        if (ConsoleE.BackContains(name)) AdminMenu.Start(accId);
        Console.WriteLine("Please enter the genre of the movie");
        string[] genre = Console.ReadLine().Split(",");

        int year;
        while (true)
        {
            Console.WriteLine("Please enter the year of the movie");
            string yearInput = Console.ReadLine();
            if (int.TryParse(yearInput, out year))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid year.");
            }
        }

        Console.WriteLine("Please enter the description of the movie");
        string description = Console.ReadLine();
        Console.WriteLine("Please enter the director of the movie");
        string director = Console.ReadLine();

        int duration;
        while (true)
        {
            Console.WriteLine("Please enter the duration of the movie (minutes)");
            string durationInput = Console.ReadLine();
            if (int.TryParse(durationInput, out duration))
            {
                break; 
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid duration.");
            }
        }

        MovieModel movies = new MovieModel(name, genre, year, description, director, duration);
        GenericMethods.UpdateList(movies);

    }

    public static void addUser(AccountsLogic accountsLogic)
    {
        ConsoleE.Clear();
        Console.WriteLine("Welcome to the registration page");
        Console.WriteLine("Enter [Q] to go back or type enter to continue\n-    -   -   -   -   -   -    -\n");
        string Choice = Console.ReadLine();
        if (!ConsoleE.BackContains(Choice))
        {
            string emailAddress = "";
            while(true)
            {
                Console.WriteLine("Enter your email address: ");
                emailAddress = Console.ReadLine();
                string pattern = @"^[^\.][\w\.-]+@[\w\.-]+\.[\w]+$";
                if (emailAddress.Contains("@")&& emailAddress.Contains(".")&&Regex.IsMatch(emailAddress, pattern))
                {

                    /*string path = @"DataSources\accounts.json";
                    var jsonString = File.ReadAllText(path);
                    List<AccountModel> accounts = JsonSerializer.Deserialize<List<AccountModel>>(jsonString);

                    bool emailExists = false;
                    foreach (var account in accounts)
                    {
                        if (account.EmailAddress == emailAddress)
                        {
                            emailExists = true;
                            break;
                        }
                    }*/

                    if (accountsLogic.EmailExists(emailAddress))
                    {
                        Console.WriteLine("An account with this email address already exists.");
                        Console.WriteLine("Please enter a different email address.");
                        continue;
                    }
                    break;
                }
                else if (ConsoleE.BackContains(emailAddress)) MainMenu.Start();
                else
                {
                    Console.WriteLine("Invalid email address format. Please make sure your email address contains a @ and a . [Q] to quit");
                }
            }      
            string password = "";
            string confirmPassword = "";
            while(true)
            {
                Console.WriteLine("Enter your password:");
                password = PasswordInput.InputPassword();

                if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d).{8,}$"))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid password format.");
                    Console.WriteLine("Password should contain:");
                    Console.WriteLine("- At least one capital letter");
                    Console.WriteLine("- At least 8 characters long");
                    Console.WriteLine("- One number");
                    Console.WriteLine();
                    continue;
                }
                Console.WriteLine("Confirm your password:");
                confirmPassword = PasswordInput.InputPassword();

                if (password == confirmPassword)
                {
                    break;
                }
                else if (ConsoleE.BackContains(password)) MainMenu.Start();
                else
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                }
            }  
        
            string HashedPassword = PasswordHasher.HashPassword(password);
            Console.WriteLine("Enter your fullname");
            string fullName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(fullName))
            {
                if (ConsoleE.BackContains(password)) MainMenu.Start();
                Console.WriteLine("Fullname can not be blank. Please enter your fullname");
                fullName = Console.ReadLine();
            }
            /*
            try
            {
            Console.WriteLine("Enter your phonenumber:");
            string Pnumber = Console.ReadLine();
            int PhoneNumber = Convert.ToInt32(Pnumber);
            }
            catch
            {
                Console.WriteLine("Invalid phonenumber. Pleas make sure your phonenumber should only contain numbers");
            }*/
            int PhoneNumber = 0;
            while (true)
            {
                Console.Write("Enter your phonenumber: 06 ");
                string Pnumber = $"06{Console.ReadLine()}";
                
                
                try
                {
                    PhoneNumber = Convert.ToInt32(Pnumber);
                    break;
                }
                catch
                {
                    if (ConsoleE.BackContains(Pnumber)) MainMenu.Start();
                    else Console.WriteLine("Invalid phonenumber. Please make sure your phonenumber should only contain numbers.");
                }
                while (Pnumber.Length != 10)
                {
                    Console.WriteLine("Invalid phonenumber. Please make sure your phonenumber is 10 digits long.");
                    Console.Write("Enter your phonenumber: 06 ");
                    Pnumber = $"06{Console.ReadLine()}";
                }
                
            }

            AccountModel newAccount = new AccountModel(0 , emailAddress, HashedPassword, fullName, PhoneNumber);

            GenericMethods.UpdateList(newAccount);
            MainMenu.Start();
        }
        else
        {
            MainMenu.Start();
        }
    }
}
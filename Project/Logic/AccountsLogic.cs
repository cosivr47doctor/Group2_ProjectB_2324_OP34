using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Globalization;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = GenericAccess<AccountModel>.LoadAll();
    }

    public static void UpdateAccount(AccountModel updatedAccount)
    {
        List<AccountModel> accounts = GenericAccess<AccountModel>.LoadAll(); // Load all accounts from the file

        // Find the index of the account to update
        int index = accounts.FindIndex(a => a.Id == updatedAccount.Id);

        if (index != -1)
        {
            // Update the account at the found index
            accounts[index] = updatedAccount;

            GenericAccess<AccountModel>.WriteAll(accounts);
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }


    public AccountModel GetByArg(string input)
    {
        if (input.All(char.IsDigit))
        {
            return GetByArg(Convert.ToInt16(input));
        }
        else
        {
            return _accounts.Find(i => i.EmailAddress == input);
        }
    }
    public AccountModel GetByArg(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && PasswordHasher.ValidatePassword(password, i.Password));
        if (CurrentAccount is not null)
        {
            // Now check if the account has been banned
            if (CurrentAccount.Suspense.HasValue && CurrentAccount.Suspense.Value > DateTime.Today)
            {
                Console.WriteLine($"This account has been banned until: {CurrentAccount.Suspense}");
                return null;
            }
            else
            {
                CurrentAccount.Suspense = null;
                UpdateAccount(CurrentAccount);
                return CurrentAccount;
            }
        }
        else
        {
            Console.WriteLine("Account not found");
            Console.WriteLine("press enter to head back to the login menu");
            Console.ReadLine();
            MainMenu.Start();
            return null;
        }
    }

    public void ChangeUserStatus(string change_status_input, string find_by_arg, int accId)
    {
        CurrentAccount = GetByArg(find_by_arg);
        if (Convert.ToInt16(change_status_input) == 1)
        {
            Console.WriteLine("For how many days would you like the user to be suspended / until which date? ('permanent' if permanent)");
            string user_input = Console.ReadLine();
            int suspenseDays;
            DateTime suspenseDate;
            if (int.TryParse(user_input, out suspenseDays))
            {
                if (CurrentAccount.Suspense.HasValue)
                    CurrentAccount.Suspense = CurrentAccount.Suspense.Value.AddDays(suspenseDays);
                else
                    CurrentAccount.Suspense = DateTime.Today.AddDays(suspenseDays);
                Console.WriteLine($"User {CurrentAccount.Id} suspended until {CurrentAccount.Suspense}");
            }
            // This checks whether an input matches a valid datetime object format
            else if (DateTime.TryParseExact(user_input, new[] {"dd-MM-yyyy", "yyyy-MM-dd"}, CultureInfo.InvariantCulture, DateTimeStyles.None, out suspenseDate))
            {
                CurrentAccount.Suspense = suspenseDate;
                Console.WriteLine($"User {CurrentAccount.Id} suspended until {CurrentAccount.Suspense?.ToString("yyyy-MM-dd")}");
            }
            else if (user_input == "permanent" || user_input == "'permanent")
            {
                CurrentAccount.Suspense = DateTime.MaxValue;
                Console.WriteLine($"User {CurrentAccount.Id} suspended permanently.");
            }
            else
            {
                Console.WriteLine("Invalid input. Should be a datetime format, a number or 'permanent'");
            }
        //  //
        }
        else if (Convert.ToInt16(change_status_input) == 2)
        {
            CurrentAccount.Suspense = null;
        }
        else if (Convert.ToInt16(change_status_input) == 3)
        {
            CurrentAccount.isAdmin = true;
        }
        else if (Convert.ToInt16(change_status_input) == 4)
        {
            CurrentAccount.isAdmin = false;
        }
        GenericMethods.UpdateList(CurrentAccount);
        AdminMenu.Start(accId);
    }
}





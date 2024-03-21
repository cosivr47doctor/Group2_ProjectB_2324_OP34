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
        _accounts = AccountsAccess.LoadAll();
    }


    public void StartupUpdateList()
    {
        AccountsAccess.WriteAll(_accounts);
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already a model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public static void UpdateAccount(AccountModel updatedAccount)
    {
        List<AccountModel> accounts = AccountsAccess.LoadAll(); // Load all accounts from the file

        // Find the index of the account to update
        int index = accounts.FindIndex(a => a.Id == updatedAccount.Id);

        if (index != -1)
        {
            // Update the account at the found index
            accounts[index] = updatedAccount;

            // Serialize the modified list of accounts
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(accounts, options);

            // Write the serialized JSON back to the file
            File.WriteAllText(@"DataSources/accounts.json", json);
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
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        // Now check if the account has been banned
        if (Convert.ToString(CurrentAccount.Suspense) == "permanent" || CurrentAccount.Suspense > DateTime.Today)
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

    public void ChangeUserStatus(string change_status_input, string find_by_arg)
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
                CurrentAccount.Suspense?.AddDays(suspenseDays);
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
                CurrentAccount.Suspense = new DateTime(9999, 01, 01);
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
        UpdateList(CurrentAccount);
        AdminMenu.Start();
    }
}





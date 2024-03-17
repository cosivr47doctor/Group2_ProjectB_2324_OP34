using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


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

    public AccountModel GetByArg(string input)
    {
        if (input.All(char.IsDigit))
        {
            GetByArg(Convert.ToInt16(input));
        }
        else
        {
            foreach (char chr in input)
            {
                if (chr == '@')
                {
                    return _accounts.Find(i => i.EmailAddress == input);
                }
            }
        }
        return null;
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
            return CurrentAccount;
        }
    }
}





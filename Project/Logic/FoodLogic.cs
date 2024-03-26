using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class FoodLogic
{
    private List<FoodModel> _food;
    public List<FoodModel> foodItems => _food;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself

    //static public FoodModel? CurrentFoodItem { get; private set; }

    public FoodLogic()
    {
        _food = FoodAccess.LoadAll();
    }


    public void UpdateList(FoodModel foodItem)
    {
        // For auto-increment
        int maxId = _food.Count > 0 ? _food.Max(m => m.Id) : 0;
        //Find if there is already an model with the same id
        int index = _food.FindIndex(s => s.Name == foodItem.Name);

        if (index != -1)
        {
            //update existing model
            _food[index] = foodItem;
        }
        else
        {
            //add new model
            foodItem.Id = maxId + 1;
            _food.Add(foodItem);
        }
        FoodAccess.WriteAll(_food);

    }

    // public AccountModel GetById(int id)
    // {
    //     return _accounts.Find(i => i.Id == id);
    // }

    // public AccountModel CheckLogin(string email, string password)
    // {
    //     if (email == null || password == null)
    //     {
    //         return null;
    //     }
    //     CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
    //     return CurrentAccount;
    // }
}

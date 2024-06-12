using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class FoodLogic : AbstractLogic<FoodModel>
{
    private List<FoodModel> _food;
    public List<FoodModel> foodItems => _food;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself

    //static public FoodModel? CurrentFoodItem { get; private set; }

    public FoodLogic()
    {
        _food = GenericAccess<FoodModel>.LoadAll();
    }

    public override List<FoodModel> GetAllBySearch(string searchBy)
    {
        FoodModel singleFoodModel = SelectForResv(searchBy);
        return new List<FoodModel> {singleFoodModel};
    }

    public FoodModel SelectForResv(string searchBy)
    {
        string searchLower = searchBy.ToLower();

        return _food.Find(food =>
            food.Id.ToString() == searchBy ||
            food.Name.ToLower().Contains(searchLower));  // This is unhandy in case of movies with duplicate directors
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

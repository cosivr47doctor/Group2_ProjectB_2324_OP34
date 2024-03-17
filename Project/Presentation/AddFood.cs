static class AddFood
{
    static private FoodLogic foodLogic = new FoodLogic();


    public static void addFood()
    {
        Console.WriteLine("Add food to the menu:");
        Console.WriteLine("Please enter the name of the food item");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter the price of the food item");
        decimal price = Convert.ToDecimal(Console.ReadLine());
        FoodModel foodItem = new FoodModel(name, price);
        foodLogic.UpdateList(foodItem);

    }
}
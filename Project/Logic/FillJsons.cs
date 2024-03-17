using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class FillJsons
{
    public static void FillFoodJson(string filePath)
    {
        List<FoodModel> existingFoodItems = FoodAccess.LoadAll();

        List<FoodModel> standardItems = new List<FoodModel>
        {
            new FoodModel("popcorn", 2.99m),
            new FoodModel("chips", 3.50m),
            new FoodModel("cola", 2.50m)
        };

        foreach (var item in standardItems)
        {
            int index = existingFoodItems.FindIndex(food => food.Name == item.Name);

            if (index == -1)
            {
                existingFoodItems.InsertRange(0, standardItems);
            }
        }
        FoodAccess.WriteAll(existingFoodItems);
    }
}
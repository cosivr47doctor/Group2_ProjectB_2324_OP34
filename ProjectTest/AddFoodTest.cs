using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ProjectTest;

namespace FoodTest
{
    [TestClass]
    public class FoodTests
    {
        [TestMethod]
        public void TestAddFood_ValidInput()
        {
            // Arrange
            int accId = 1;
            string input = "Pizza\n10,99\n";
            using (StringReader sr = new StringReader(input))
            using (StringWriter sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);

                // Act
                Adding.addFood(accId);

                // Assert
                var foodLogic = new FoodLogic();
                var lastFoodItem = foodLogic.foodItems.LastOrDefault();  // Get the last added food item
                Assert.IsNotNull(lastFoodItem);  // Ensure there is an item to check
                Assert.AreEqual("Pizza", lastFoodItem.Name);
                Assert.AreEqual(10.99m, lastFoodItem.Price);
            }
        }

        [TestMethod]
        public void TestAddFood_InvalidPrice()
        {
            // Arrange
            int accId = 1;
            string input = "Pizza\ninvalidPrice\n10,99\n";
            using (StringReader sr = new StringReader(input))
            using (StringWriter sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);

                // Act
                Adding.addFood(accId);

                // Assert
                var output = sw.ToString();
                StringAssert.Contains(output, "Invalid input. Please enter a valid price.");
            }
        }
    }
}

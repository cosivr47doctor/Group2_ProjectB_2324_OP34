using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using ProjectTest;  

namespace MovieTest
{
    [TestClass]
    public class MovieTests
    {
        // [TestInitialize]
        // public void TestInitialize()
        // {
        //     // Optional: Clean up the JSON file or initialize a clean state before each test
        //     var movieFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataSources/movies.json");
        //     if (File.Exists(movieFilePath))
        //     {
        //         File.Delete(movieFilePath);
        //     }
        // }

        [TestMethod]
        public void TestAddMovie_ValidInput()
        {
            // Arrange
            int accId = 1;
            string input = "InceptionTest\nAction\n2010\nA mind-bending thriller\nChristopher Nolan\n148\n";
            using (StringReader sr = new StringReader(input))
            using (StringWriter sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);

                // Act
                Adding.addMovie(accId);

                // Assert
                MovieLogic movieLogic = new MovieLogic();
                var addedMovie = movieLogic.Movies.LastOrDefault();
                Assert.IsNotNull(addedMovie);
                Assert.AreEqual("InceptionTest", addedMovie.Name);
                CollectionAssert.AreEqual(new string[] {"Action"}, addedMovie.Genre);
                Assert.AreEqual(2010, addedMovie.Year);
                Assert.AreEqual("A mind-bending thriller", addedMovie.Description);
                Assert.AreEqual("Christopher Nolan", addedMovie.Director);
                Assert.AreEqual(148, addedMovie.Duration);
            }
        }

        [TestMethod]
        public void TestAddMovie_InvalidYear()
        {
            // Arrange
            int accId = 1;
            string input = "InceptionTest\nAction\ninvalidYear\n2000\nA mind-bending thriller\nChristopher Nolan\n148\n";
            using (StringReader sr = new StringReader(input))
            using (StringWriter sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);

                // Act
                Adding.addMovie(accId);

                // Assert
                var output = sw.ToString();
                StringAssert.Contains(output, "Invalid input. Please enter a valid year.");
            }
        }

        [TestMethod]
        public void TestAddMovie_InvalidDuration()
        {
            // Arrange
            int accId = 1;
            string input = "InceptionTest\nAction\n2010\nA mind-bending thriller\nChristopher Nolan\ninvalidDuration\n148\n";
            using (StringReader sr = new StringReader(input))
            using (StringWriter sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);

                // Act
                Adding.addMovie(accId);

                // Assert
                var output = sw.ToString();
                StringAssert.Contains(output, "Invalid input. Please enter a valid duration.");
            }
        }
    }
}

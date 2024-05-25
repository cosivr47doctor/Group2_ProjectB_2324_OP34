using Moq;
using System.IO;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions; using Xunit;

namespace ProjectTest;


[TestClass]
public class TestEditMovie
{
    private static readonly MovieLogic movieLogic = new MovieLogic();
    private static Mock<IFileWrapper> fileMock;
    private const string mockFilePath = "mock_movies.json";

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // Setup the mock
        fileMock = new Mock<IFileWrapper>();

        // Initial mock file content
        var initialMovies = new List<MovieModel>
        {
            new MovieModel { Id = 12, Name = "InitialMovie", Director = "InitialDirector" }
        };

        string initialJson = JsonSerializer.Serialize(initialMovies);
        fileMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(initialJson);
        fileMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
            .Callback<string, string>((path, json) => {
                // Overwrite the ReadAllText setup to return the updated json
                fileMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(json);
            });

        // Inject the mock into MovieAccess
        MovieAccess.FileWrapper = fileMock.Object;
    }

    [TestMethod]
    public void TestChangeMovie()
    {
        /* 
        NOTES:
        1. Ask teacher about the changes not being saved to the JSON file.
        2. Ask teacher about ICloneable not working.
        */

        // Make sure you communicate in the details that you changed them.
        List<MovieModel> movies = MovieAccess.LoadAllJson();
        MovieModel unitTestChangeDetails = movies.FirstOrDefault(m => m.Id == 12);
        unitTestChangeDetails.Should().NotBeNull("The movie with Id 12 does not exist.");

        MovieModel cloneOfOriginalMovie = DeepCloner.DeepClone(unitTestChangeDetails);
        cloneOfOriginalMovie.Name = "ChangedYo";
        unitTestChangeDetails.Should().NotBe(cloneOfOriginalMovie.Name);
        
        string originalString = 
            @$"ID: 12{"\n"}Name: UnitTestChangeDetails{"\n"}Genre: drama, horror, triller{"\n"}Year: 2023{"\n"}Description: The details must be changed...
Director: Thh{"\n"}Duration: 120";
        Console.WriteLine(originalString);
        
        

        string[] unitInput = new string[]
        { 
            "12",
            "UnitChangedDetailsSuccessfully", // New title
            "Unit,Test", // New genres
            null, // No year changes
            "Changed successfully", // New description
            null, // No change in director
            null // No change in duration
        };
        movieLogic.ChangeMovie(unitInput);

        List<MovieModel> updatedMovies = MovieAccess.LoadAllJson();
        MovieModel changedMovie = updatedMovies.FirstOrDefault(m => m.Id == 12);
        changedMovie.Should().NotBeNull("The changed movie with Id 12 does not exist.");
        changedMovie.Name.Should().Be("UnitChangedDetailsSuccessfully");
        changedMovie.Description.Should().BeEquivalentTo("Changed successfully");

        changedMovie.Should().NotBeEquivalentTo(originalString, changedMovie.ToString());
        fileMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}

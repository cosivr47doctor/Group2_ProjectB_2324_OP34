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
        MovieAccessForJson.FileWrapper = fileMock.Object;
    }

    // BUG: SOMEHOW RUNS INDEFINITELY DURING UNITTEST RUNTIME, SO INSTEAD DO `DOTNET RUN`
    [TestMethod]
    public void TestChangeMovie()
    {
        // Make sure you communicate in the details that you changed them.
        List<MovieModel> movies = MovieAccessForJson.LoadAllJson();
        MovieModel movieToChange = movies.FirstOrDefault(m => m.Id == 12);
        movieToChange.Should().NotBeNull();  // Forget about ToString() as it arouses issues in the unittest run environment

        MovieModel cloneOfOriginalMovie = TestEnvironmentUtils.DeepClone(movieToChange);
        cloneOfOriginalMovie.Name = "ChangedYo";
        movieToChange.Name.Should().NotBe(cloneOfOriginalMovie.Name);
        
        // Can't ask for actual input in a unittest or dotnet test run environment
        string unitInput = "12;ChangedName;Drama, Horror;2022;New description;New Director;150";
        movieLogic.ChangeMovie(unitInput);

        List<MovieModel> updatedMovies = MovieAccessForJson.LoadAllJson();
        MovieModel changedMovie = updatedMovies.FirstOrDefault(m => m.Id == 12);
        changedMovie.Should().NotBeNull();
        changedMovie.Name.Should().Be("UnitChangedDetailsSuccessfully");
        changedMovie.Description.Should().BeEquivalentTo("Changed description successfully");

        // changedMovie.Should().NotBeEquivalentTo(originalString, changedMovie.ToString()); Unneccessary
        fileMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}

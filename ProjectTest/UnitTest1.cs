using Moq;
using System.Text.Json;
using FluentAssertions;

namespace ProjectTest;

[TestClass]
public class TestAdmin
{
    [TestMethod]
    public void IsAdmin()
    {
        AccountModel adminAcc = GenericAccess<AccountModel>.LoadAllJson().Where(acc => acc.Id == 1).FirstOrDefault();
        adminAcc.Should().NotBeNull(); adminAcc.isAdmin.Should().BeTrue();
        AccountModel dummyAcc = GenericAccess<AccountModel>.LoadAllJson().Where(acc => acc.Id == 3).FirstOrDefault();
        dummyAcc.Should().NotBeNull(); dummyAcc.isAdmin.Should().BeFalse();

        adminAcc.EmailAddress.Should().NotBe(dummyAcc.EmailAddress);
    }
}

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
                Console.WriteLine("WriteAllText Callback invoked");
                // Overwrite the ReadAllText setup to return the updated json
                fileMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(json);
            });

        // Inject the mock into MovieAccess
        MovieAccessForJson.FileWrapper = fileMock.Object;
    }

    // BUG: SOMEHOW RUNS INDEFINITELY DURING UNITTEST RUNTIME, SO INSTEAD DO `DOTNET RUN`
    [TestMethod]
    public void TestChangeMovie()  // Sp2
    {
        TestEnvironmentUtils.UserInput = new MockUserInput(new[] {
        "UnitChangedDetailsSuccessfully", "Drama,Horror", "2022", "Changed successfully", "New Director", "150"
        });
        // Make sure you communicate in the details that you changed them.
        List<MovieModel> movies = MovieAccessForJson.LoadAllJson();
        MovieModel movieToChange = movies.FirstOrDefault(m => m.Id == 12);
        movieToChange.Should().NotBeNull();  // Forget about ToString() as it arouses issues in the unittest run environment
        movieToChange.Name.Should().Be("InitialMovie");
        movieToChange.Director.Should().Be("InitialDirector");

        MovieModel cloneOfOriginalMovie = TestEnvironmentUtils.DeepClone(movieToChange);
        cloneOfOriginalMovie.Name = "ChangedYo";
        movieToChange.Name.Should().NotBe(cloneOfOriginalMovie.Name);
        
        // Can't ask for actual input in a unittest or dotnet test run environment
        string unitInput = "12;UnitChangedDetailsSuccessfully;Drama, Horror;2022;Changed successfully;New Director;150";
        movieLogic.ChangeMovie(unitInput);

        List<MovieModel> updatedMovies = MovieAccessForJson.LoadAllJson();
        MovieModel changedMovie = updatedMovies.FirstOrDefault(m => m.Id == 12);
        changedMovie.Should().NotBeNull();
        changedMovie.Name.Should().Be("UnitChangedDetailsSuccessfully");
        changedMovie.Description.Should().BeEquivalentTo("Changed successfully");

        // changedMovie.Should().NotBeEquivalentTo(originalString, changedMovie.ToString()); Unneccessary
        fileMock.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}

[TestClass]
public class TestSchedule
{
    private static readonly MovieSchedulingLogic objScheduleLogic = new MovieSchedulingLogic();
    private static Mock<IFileWrapper> fileMock;
}

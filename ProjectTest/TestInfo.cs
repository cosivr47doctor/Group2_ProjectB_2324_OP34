// /*
//     Contents:
//     - How to run the example Jukebox test project
//     - Setting up a new test project and running unit tests
//     - Test attributes
//     - Asserts
//     - Data-driven unit tests
//     - Best practices & references

//     ## How to run the example Jukebox test project ##
//     The 'Jukebox' folder contains the project with the source code to be tested. The 'JukeboxTest' folder contains the test project with the unit test class. The 'JukeboxTest.csproj' contains a reference to the location of the Jukebox project so mstest knows where to find the source code, so make sure this points to the right location!
//     To run the JukeboxTest project, execute the 'dotnet test' command inside the JukeboxTest project folder in the terminal of Visual Studio Code or another command line interface. The test execution will have run correctly if you see in the output that the 4 tests have 'Passed'.

//     ## Setting up a new test project and running unit tests  ##
//     1. The following instructions will create a new mstest project in a new folder (e.g. SampleTest):
//         /> dotnet new mstest -o SampleTest
//     2. Change the current directory to go into the newly created folder for the mstest project:
//         /> cd SampleTest
//     3. Add a reference to the project which contains the source code you want to test (e.g. a console application named SampleProject):
//         /> dotnet add reference ../SampleProject/SampleProject.csproj
//     4. Execute the test command:
//         /> dotnet test

//     ##  Test attributes ##
//     Some examples of test attributes are:
//     - [TestClass]
//     - [TestMethod]
//     - [DataTestMethod]
//     - [DataRow]
//     See the CheatsheetTest.cs file for more examples of test attributes.

//     ## Asserts ## 
//     Some examples of Assert methods are:
//     - Assert.AreEqual
//     - Assert.IsTrue
//     - Assert.IsNull
//     - Assert.IsNotNull
//     See the CheatsheetTest.cs file for more examples of Assert class methods.

//     ## Data-driven unit tests ##
//     The following methods are examples of data-driven unit tests:
//     - TestAddSongs
//     - TestAddSongsDynamicDataMethod
//     See the CheatsheetTest.cs file for more examples of Data-driven unit tests.

//     ## Best practices & references ##
//     - https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
//     - https://learn.microsoft.com/en-us/dotnet/core/tutorials/testing-library-with-visual-studio-code?pivots=dotnet-6-0
//     - https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.testtools.unittesting.assert?view=visualstudiosdk-2022

//     Now try to create some more tests for yourself! (W03.2.C01)
// */

// namespace JukeboxTest;

// /*
//     Each method tagged with [TestMethod] in a test class tagged with [TestClass] is run automatically when the unit test is invoked.
// */
// [TestClass]
// public class JukeboxTest
// {

//     /*
//         Example of a TestMethod, that tests if a song is correctly added to the playlist of the jukebox.
//     */
//     [TestMethod]
//     public void TestAddSong()
//     {
//         MyJukebox jukebox = new MyJukebox();
//         jukebox.AddSong("Best song in the world", "Unkown artist", 120);
//         Song song = jukebox.Playlist.Last();

//         String expected = "'Best song in the world' performed by 'Unkown artist' - 02m:00s";
//         String actual = song.Info();
//         Assert.AreEqual(expected, actual,
//                 string.Format("Expected: {0}; Actual: {1}",
//                                 expected, actual));
//     }

//     /*
//         Example of a DataTestMethod, that test if multiple songs are correctly added to the playlist of the jukebox. The test is run for every DataRow attribute defined for this test.
//     */
//     [DataTestMethod]
//     [DataRow("Best song in the world", "Unkown artist", 120,
//         "'Best song in the world' performed by 'Unkown artist' - 02m:00s")]
//     [DataRow("Rain in blood", "Slayer", 340,
//         "'Rain in blood' performed by 'Slayer' - 05m:40s")]
//     [DataRow("Wanted, Dead or Alive", "Bon Jovi", 460,
//         "'Wanted, Dead or Alive' performed by 'Bon Jovi' - 07m:40s")]
//     public void TestAddSongs(string title, string artist, int duration, string expected)
//     {
//         MyJukebox jukebox = new MyJukebox();
//         jukebox.AddSong(title, artist, duration);
//         Song song = jukebox.Playlist.Last();

//         String actual = song.Info();
//         Assert.AreEqual(expected, actual,
//                 string.Format("Expected: {0}; Actual: {1}",
//                                 expected, actual));
//     }
// }
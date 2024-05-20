using System;
using System.Linq;

public static class TestEnvironmentUtils
{
    private static IUserInput _userInput = new ConsoleUserInput();

    public static IUserInput UserInput
    {
        get => _userInput;
        set => _userInput = value;
    }
    public static bool IsRunningInUnitTest()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                   .Any(a => a.FullName.ToLowerInvariant().StartsWith("microsoft.testplatform"));
    }

    public static string ReadLine()
    {
        return _userInput.ReadLine();
    }
}

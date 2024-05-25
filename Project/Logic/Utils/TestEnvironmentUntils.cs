using System;
using System.Linq;
using Newtonsoft.Json;

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

    public static T DeepClone<T>(this T self)
    {
        var serialized = JsonConvert.SerializeObject(self);
        return JsonConvert.DeserializeObject<T>(serialized);
    }
  
}

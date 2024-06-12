public static class CombinedInfo
{
    public static void PrintEachModelContainingInfo(string searchBy)
    {
        var logicClasses = new List<object> { new MovieLogic(), new MovieSchedulingLogic(), new FoodLogic() };

        foreach (var logic in logicClasses)
        {
            var method = logic.GetType().GetMethod("GetAllBySearch");
            var result = method.Invoke(logic, new object[] { searchBy }) as IEnumerable<object>;
            if (result != null)
            {
                foreach (var item in result)
                {
                    if (!ConsoleE.IsNullOrEmptyOrWhiteSpace(item)) Console.WriteLine($"Found:\n{item}\n");
                }
            }
        }
    }
}

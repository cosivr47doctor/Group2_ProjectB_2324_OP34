// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to this amazing program");
FillJsons.FillFoodJson(@"DataSources/food.json");
FillJsons.FillMoviesJson(@"DataSources/movies.json");
Menu.Start();

public static class GenericMethods
{
    // Update any DataModel JSON with this method other than AccountModel which has its own method.
    public static void UpdateList<TModel>(TModel modelItem) where TModel : IModel
    {
        List<TModel> _list = GenericAccess<TModel>.LoadAll();
        // For auto-increment
        int maxId = _list.Count > 0 ? _list.Max(m => m.Id) : 0;
        //Find if there is already an model with the same id
        int index = _list.FindIndex(s => s.Id == modelItem.Id);

        if (index != -1)
        {
            //update existing model
            _list[index] = modelItem;
        }
        else
        {
            //add new model
            modelItem.Id = maxId + 1;
            _list.Add(modelItem);
        }
        GenericAccess<TModel>.WriteAll(_list);
        GenericAccess<TModel>.LoadAll();
    }

    public static List<TModel> DeleteFromList<TModel>(List<TModel> modelList, TModel modelItem) where TModel : IModel
    {
        modelList.Remove(modelItem); GenericAccess<TModel>.WriteAll(modelList);
        return modelList;
    }

/*
    // Nevermind, won't work with the DataModel classes...
    // Reloads the JSON files.
    public static void Reload()
    {
        List<Type> types = new List<Type> {typeof(AccountModel), typeof(FoodModel), typeof(MovieModel), typeof(MovieScheduleModel), typeof(ReservationModel)};

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod("LoadAll");
            method.Invoke(instance, null);
        }
    }
*/
    public static void Reload()
    {
        GenericAccess<AccountModel>.LoadAll();
        GenericAccess<FoodModel>.LoadAll();
        GenericAccess<MovieModel>.LoadAll();
        GenericAccess<MovieScheduleModel>.LoadAll();
        GenericAccess<ReservationModel>.LoadAll();
        GenericAccess<RoomModel>.LoadAll();
    }
}

public static class GenericMethods
{
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
    }
}

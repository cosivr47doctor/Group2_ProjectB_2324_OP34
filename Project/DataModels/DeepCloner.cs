
using Newtonsoft.Json;

public static class DeepCloner
{

  public static T DeepClone<T>(this T self)
  {
  
    var serialized = JsonConvert.SerializeObject(self);
    return JsonConvert.DeserializeObject<T>(serialized);
    
  }
  
}

/*
public class DeepCloner<T>
{
  public object selff {get; set;}

  public DeepCloner(T self)
  {
    var serialized = JsonConvert.SerializeObject(self);
    selff = JsonConvert.DeserializeObject<T>(serialized);
    
  }
  
}
*/

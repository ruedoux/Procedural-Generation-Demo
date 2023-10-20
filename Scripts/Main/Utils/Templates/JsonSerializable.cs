using Newtonsoft.Json;

namespace ProceduralGeneration;

[JsonObject(MemberSerialization.OptIn)]
public partial class JsonSerializable
{
  public T FromJsonString<T>(string jsonString)
    where T : JsonSerializable
  {
    JsonConvert.PopulateObject(jsonString, this);
    return this as T;
  }
  public override string ToString()
  {
    return JsonConvert.SerializeObject(this);
  }

  public string ToJsonString()
  {
    return ToString();
  }
}
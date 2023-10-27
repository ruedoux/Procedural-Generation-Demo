using Newtonsoft.Json;

namespace ProceduralGeneration;

[JsonObject(MemberSerialization.OptIn)]
public partial class JsonSerializable
{
  private readonly JsonSerializerSettings jsonSettingsOverwrite = new();

  protected JsonSerializable(JsonSerializerSettings jsonSettingsOverwrite = null)
  {
    if (jsonSettingsOverwrite != null)
      this.jsonSettingsOverwrite = jsonSettingsOverwrite;
  }

  public T FromJsonString<T>(string jsonString)
    where T : JsonSerializable
  {
    JsonConvert.PopulateObject(jsonString, this, jsonSettingsOverwrite);
    return this as T;
  }

  public override string ToString()
  {
    return JsonConvert.SerializeObject(this, jsonSettingsOverwrite);
  }

  public string ToJsonString()
  {
    return ToString();
  }
}
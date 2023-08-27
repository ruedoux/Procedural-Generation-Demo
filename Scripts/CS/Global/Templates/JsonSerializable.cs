using Godot;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public partial class JsonSerializable : RefCounted
{
  public JsonSerializable FromJsonString(string jsonString)
  {
    JsonConvert.PopulateObject(jsonString, this);
    return this;
  }

  public override string ToString()
  {
    return JsonConvert.SerializeObject(this);
  }
}
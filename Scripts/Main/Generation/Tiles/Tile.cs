using Newtonsoft.Json;

public partial class Tile : JsonSerializable
{

  [JsonProperty] public int id;
  [JsonProperty] public int layer;

  public Tile(int id = default, int layer = default)
  {
    this.id = id;
    this.layer = layer;
  }

  public Tile() { }
}

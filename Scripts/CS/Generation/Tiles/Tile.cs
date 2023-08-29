using Godot;
using Newtonsoft.Json;

public partial class Tile : JsonSerializable
{

  [JsonProperty] public int tileId;
  [JsonProperty] public int layer;

  public Tile(int tileId = default, int layer = default)
  {
    this.tileId = tileId;
    this.layer = layer;
  }

  public Tile() { }
}

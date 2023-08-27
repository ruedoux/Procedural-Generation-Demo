using Godot;
using Newtonsoft.Json;

public partial class Tile : JsonSerializable
{

  [JsonProperty] public int TileId;
  [JsonProperty] public int Layer;

  public Tile(int TileId = default, int Layer = default)
  {
    this.TileId = TileId;
    this.Layer = Layer;
  }

  public Tile() { }
}

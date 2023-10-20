using Godot;
using Newtonsoft.Json;

namespace ProceduralGeneration;

public class Tile : JsonSerializable
{
  [JsonProperty] public int tileId;
  [JsonProperty] public Color color;

  public Tile(int tileId = default, Color color = default)
  {
    this.tileId = tileId;
    this.color = color;
  }

  public Tile() { }

  public override bool Equals(object obj)
  {
    if (obj == null || GetType() != obj.GetType())
      return false;

    Tile other = obj as Tile;

    return (tileId == other.tileId) && (color == other.color);
  }

  public override int GetHashCode()
    => System.HashCode.Combine(tileId, color);

}

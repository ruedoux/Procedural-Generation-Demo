using Newtonsoft.Json;

public class Tile : JsonSerializable
{

  [JsonProperty] public int id;
  [JsonProperty] public int layer;

  public Tile(int id = default, int layer = default)
  {
    this.id = id;
    this.layer = layer;
  }

  public Tile() { }

  public override bool Equals(object obj)
  {
    if (obj == null || GetType() != obj.GetType())
      return false;

    Tile other = obj as Tile;

    return (id == other.id) && (layer == other.layer);
  }

  public override int GetHashCode()
  {
    return System.HashCode.Combine(id, layer);
  }
}

using System.Text.Json;

public partial class Tile
{
  public int TileID { get; private set; }
  public int Layer { get; private set; }

  public Tile(int TileID = -1, int Layer = -1)
  {
    this.TileID = TileID;
    this.Layer = Layer;
  }
  public override string ToString()
  {
    return JsonSerializer.Serialize(this);
  }
}

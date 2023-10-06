using System.Linq;
using Godot;

public partial class MapRoot : Node2D
{
  private readonly TileDatabase tileDatabase = new();

  public override void _Ready()
  {
    Color[] colors = tileDatabase.entries
      .OrderBy(keyPair => (int)keyPair.Key)
      .Select(keyPair => keyPair.Value.color).ToArray();

    var tileMap = ProceduralTileMapCreator.GenerateTileMap(new Vector2I(4, 4), colors);
    AddChild(tileMap);
  }
}
using System.Linq;
using Godot;

public partial class MapRoot : Node2D
{
  public override void _Ready()
  {
    var tileMap = ProceduralTileMapCreator.GenerateTileMap(
      new Vector2I(4, 4), GlobalData.DefaultTileColors.Values.ToArray());
    AddChild(tileMap);
  }
}
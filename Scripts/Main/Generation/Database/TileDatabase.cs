using Godot;


public class TileDatabase : InMemoryDatabase<Tile>
{
  public enum TileType { GRASS, FOREST, WATER, SNOW, STONE, SAND, NONE }

  public TileDatabase()
  {
    AddTile(TileType.WATER, Colors.LightBlue);
    AddTile(TileType.SAND, Colors.Yellow);
    AddTile(TileType.GRASS, Colors.LawnGreen);
    AddTile(TileType.FOREST, Colors.ForestGreen);
    AddTile(TileType.STONE, Colors.LightGray);
    AddTile(TileType.SNOW, Colors.White);
    AddTile(TileType.NONE, Colors.HotPink);
  }

  private void AddTile(TileType tileType, Color color)
    => AddEntry(tileType, new((int)tileType, color));

}
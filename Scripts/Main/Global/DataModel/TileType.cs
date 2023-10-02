using Godot;

public class TileType : InMemoryEntity<uint>
{
  public enum TileTypes { GRASS, FOREST, WATER, SNOW, STONE }
  public TileTypes tileType;
  public Color color;

  public TileType() { }

  public TileType(TileTypes tileType, Color color)
  {
    this.tileType = tileType;
    this.color = color;
  }
}
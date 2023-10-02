using Godot;

using static TileType.TileTypes;

public class TileTypeDatabase : InMemoryDatabase<uint, TileType>
{
  public readonly TileType[] tileTypes = new TileType[]
    {
      new(GRASS, Colors.LimeGreen),
      new(FOREST, Colors.ForestGreen),
      new(WATER, Colors.LightSeaGreen),
      new(SNOW, Colors.White),
      new(STONE, Colors.LightGray)
    };

  public TileTypeDatabase()
  {
    for (uint i = 0; i < tileTypes.Length; i++)
      AddEntry(i, tileTypes[i]);
  }
}
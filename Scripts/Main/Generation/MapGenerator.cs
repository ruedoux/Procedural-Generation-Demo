using Godot;

namespace ProceduralGeneration;

public partial class MapGenerator
{
  private readonly FastNoiseLite fastNoiseLite;
  private readonly TileNoiseRange tileNoiseRange;

  public MapGenerator(
    FastNoiseLite fastNoiseLite,
    TileNoiseRange tileNoiseRange)
  {
    this.fastNoiseLite = fastNoiseLite;
    this.tileNoiseRange = tileNoiseRange;
  }

  public virtual Tile GetTileOnPosition(Vector3I vec)
  {
    return tileNoiseRange.GetTileByNoise(fastNoiseLite.GetNoise3Dv(vec));
  }

  public void FillTileMapWithNoise(TileMap tileMap)
  {
    Vector2 size = new(600, 400);

    for (int x = 0; x < size.X; x++)
      for (int y = 0; y < size.Y; y++)
      {
        Tile tile = GetTileOnPosition(new Vector3I(x, y, 0));
        tileMap.SetCell(0, new Vector2I(x, y), 0, new Vector2I(tile.tileId, 0));
      }
  }
}
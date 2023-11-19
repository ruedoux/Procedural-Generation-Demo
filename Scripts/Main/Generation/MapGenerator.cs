using System;
using Godot;

namespace ProceduralGeneration;

public partial class MapGenerator
{
  private readonly MapFilter mapFilter;
  private readonly FastNoiseLite fastNoiseLite;
  private readonly TileNoiseRange tileNoiseRange;

  public MapGenerator(
    FastNoiseLite fastNoiseLite,
    TileNoiseRange tileNoiseRange,
    MapFilter mapFilter = null)
  {
    this.fastNoiseLite = fastNoiseLite;
    this.tileNoiseRange = tileNoiseRange;
    this.mapFilter = mapFilter;
  }

  public virtual Tile GetTileOnPosition(Vector3I vec)
  {
    float noise = fastNoiseLite.GetNoise3Dv(vec);
    if (mapFilter != null)
      noise = mapFilter.Filter(vec, noise);

    return tileNoiseRange.GetTileByNoise(noise);
  }

  public void FillTileMapWithNoise(TileMap tileMap, int width, int height)
  {
    for (int x = 0; x < width; x++)
      for (int y = 0; y < height; y++)
      {
        Tile tile = GetTileOnPosition(new Vector3I(x, y, 0));
        tileMap.SetCell(0, new Vector2I(x, y), 0, new Vector2I(tile.tileId, 0));
      }
  }

  public void CreateImageInPath(string filePath, int width, int height)
  {
    Image image = Image.Create(width, height, false, Image.Format.Rgba8);
    for (int x = 0; x < width; x++)
      for (int y = 0; y < height; y++)
      {
        Tile tile = GetTileOnPosition(new Vector3I(x, y, 0));
        image.SetPixel(x, y, tile.color);
      }

    image.SavePng(filePath);
  }
}
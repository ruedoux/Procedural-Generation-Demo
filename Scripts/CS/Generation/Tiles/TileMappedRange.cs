using Godot;
using Godot.Collections;

public partial class TileMappedRange : RefCounted
{
  private readonly Array<Tile> range = new();

  public TileMappedRange(Array<TileRange> tileRanges, Tile defaultTile, int rangeSize)
  {
    for (int i = 0; i < rangeSize; i++)
    {
      range.Add(defaultTile);
    }

    foreach (TileRange tileRange in tileRanges)
    {
      for (int i = NoiseToIndex(tileRange.noiseFrom); i < NoiseToIndex(tileRange.noiseTo); i++)
      {
        range[i] = tileRange.tile;
      }
    }
  }

  public Array<Tile> GetRange()
  {
    return range;
  }

  public int NoiseToIndex(float noise)
  {
    int convertedNoise = (int)(noise * 100) + 100; // 0, ..., 200
    return (int)(convertedNoise / (((float)convertedNoise) / range.Count));
  }
}
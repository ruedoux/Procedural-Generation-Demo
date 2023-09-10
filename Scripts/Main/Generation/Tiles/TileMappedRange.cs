using System.Collections.Generic;

public partial class TileMappedRange
{
  private readonly Tile[] range;

  public TileMappedRange(
    List<TileNoiseEntry> tileRanges, Tile defaultTile, int rangeSize)
  {
    range = new Tile[rangeSize];
    for (int i = 0; i < rangeSize; i++)
    {
      range[i] = defaultTile;
    }

    foreach (TileNoiseEntry tileRange in tileRanges)
    {
      for (int i = NoiseToIndex(tileRange.noiseFrom); i < NoiseToIndex(tileRange.noiseTo); i++)
      {
        range[i] = tileRange.tile;
      }
    }
  }

  public Tile[] GetRange()
  {
    return range;
  }

  public int NoiseToIndex(float noise)
  {
    int convertedNoise = (int)(noise * 100) + 100; // 0, ..., 200
    return (int)(convertedNoise / (((float)convertedNoise) / range.Length));
  }
}
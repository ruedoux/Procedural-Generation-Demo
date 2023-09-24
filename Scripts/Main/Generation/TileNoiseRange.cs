using System.Collections.Generic;

public partial class TileNoiseRange
{
  public const uint RANGE_SIZE = 100;
  public readonly Tile[] range;

  public TileNoiseRange(
    List<TileNoise> tileRanges, Tile defaultTile)
  {
    range = new Tile[RANGE_SIZE];
    for (uint i = 0; i < range.Length; i++)
      range[i] = defaultTile;

    foreach (TileNoise tileRange in tileRanges)
      for (uint i = NoiseToIndex(tileRange.noiseFrom); i < NoiseToIndex(tileRange.noiseTo); i++)
        range[i] = tileRange.tile;
  }

  private uint NoiseToIndex(float noise)
  {
    return (uint)((noise + 1f) * 100 / 2); // 0 ... 100
  }
}
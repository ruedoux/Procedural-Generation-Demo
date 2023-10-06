using System;
using System.Collections.Generic;

public partial class TileNoiseRange
{
  private const uint RANGE_SIZE = 100;
  public readonly Tile[] range;

  public TileNoiseRange(TileNoise[] tileRanges, Tile defaultTile = null)
  {
    range = new Tile[RANGE_SIZE];
    for (uint i = 0; i < range.Length; i++)
      range[i] = defaultTile;

    foreach (TileNoise tileRange in tileRanges)
      for (uint i = NoiseToIndex(tileRange.noiseFrom); i < NoiseToIndex(tileRange.noiseTo); i++)
        range[i] = tileRange.tile;
  }

  public Tile GetTileByNoise(float noise)
  {
    return range[NoiseToIndex(noise)];
  }

  private uint NoiseToIndex(float noise)
  {
    noise = Math.Clamp(noise, -1.0f, 1.0f);
    return (uint)((noise + 1f) * 100 / 2); // 0 ... 100
  }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProceduralGeneration;

public partial class TileNoiseRange
{
  private const uint RANGE_SIZE = 100;
  public readonly Tile[] range = new Tile[RANGE_SIZE];

  public TileNoiseRange(TileNoise[] tileRanges, Tile defaultTile)
  {
    List<TileNoise> orderedRanges = tileRanges.OrderBy(o => o.noiseMarker).ToList();

    for (int i = 0; i < range.Length; i++)
      range[i] = defaultTile;

    uint index = 0;
    foreach (TileNoise tileNoise in orderedRanges)
      for (; index <= NoiseToIndex(tileNoise.noiseMarker); index++)
        range[index] = tileNoise.tile;
  }

  public Tile GetTileByNoise(float noise)
  {
    return range[NoiseToIndex(noise)];
  }

  private static uint NoiseToIndex(float noise)
    => Math.Clamp((uint)((noise + 1f) * 100 / 2), 0, RANGE_SIZE - 1);
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProceduralGeneration;

public partial class TileNoiseRange
{
  private const uint RANGE_SIZE = 100;
  public readonly Tile[] range = new Tile[RANGE_SIZE];

  public TileNoiseRange(TileNoise[] tileRanges, Tile defaultTile = null)
  {
    List<TileNoise> orderedRanges = tileRanges.OrderBy(o => o.noiseMarker).ToList();
    orderedRanges.Add(new(1.0f, defaultTile));

    uint index = 0;
    foreach (TileNoise tileNoise in orderedRanges)
      for (; index < NoiseToIndex(tileNoise.noiseMarker); index++)
        range[index] = tileNoise.tile;
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
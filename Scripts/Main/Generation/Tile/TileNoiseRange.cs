using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

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
    {
      for (; index < NoiseToIndex(tileNoise.noiseMarker); index++)
        range[index] = tileNoise.tile;
      if (tileNoise.noiseMarker == 1.0f)
        range[RANGE_SIZE - 1] = tileNoise.tile;
    }

  }

  public Tile GetTileByNoise(float noise)
    => range[NoiseToIndex(noise)];

  private static uint NoiseToIndex(float noise)
  {
    noise = Math.Clamp(noise, -1.0f, 1.0f);
    return (uint)((noise + 1f) * (RANGE_SIZE - 1) / 2);
  }
}
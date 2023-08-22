using System;
using System.Collections.Generic;

public partial class TileRange
{
  private const int RANGE_LIST_SIZE = 100;
  private const float MIN_NOISE_RANGE = -1;
  private const float MAX_NOISE_RANGE = 1;
  public List<Tile> rangeList = new();

  public TileRange(Dictionary<Tuple<float, float>, Tile> tileDict, Tile defaultTile)
  {
    rangeList.ForEach(tile => tile = defaultTile);

    foreach (var keyPair in tileDict)
    {
      Exceptions.ThrowIfLessThan(keyPair.Key.Item1, MIN_NOISE_RANGE);
      Exceptions.ThrowIfGreaterThan(keyPair.Key.Item2, MAX_NOISE_RANGE);
      for (int i = NoiseToIndex(keyPair.Key.Item1); i < NoiseToIndex(keyPair.Key.Item2); i++)
      {
        Exceptions.ThrowIfEqualOrGreaterThan(i, RANGE_LIST_SIZE);
        Exceptions.ThrowIfLessThan(i, 0);

        rangeList[i] = keyPair.Value;
      }
    }
  }

  public static int NoiseToIndex(float noise)
  {
    return ((int)(noise * 100) + 100) / 2;
  }
}
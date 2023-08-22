using System.Collections.Generic;
using Godot;

public partial class MapGenerator
{
  private readonly FastNoiseLite fastNoiseLite;
  private readonly Tile[] tileRange;

  protected MapGenerator(
    FastNoiseLite fastNoiseLite,
    Tile[] tileRange)
  {
    this.fastNoiseLite = fastNoiseLite;
    this.tileRange = tileRange;
  }

  public Tile GetTileOnPosition(Vector3I vec)
  {
    GetValueOnPosition(vec);
    return new Tile();
  }

  protected int NoiseToIndex(float noise)
  {

    return 0;
  }

  protected virtual float GetValueOnPosition(Vector3I vec)
  {
    return fastNoiseLite.GetNoise3D(vec.X, vec.Y, vec.Z);
  }
}
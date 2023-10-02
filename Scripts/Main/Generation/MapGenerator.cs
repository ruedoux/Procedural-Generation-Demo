using Godot;

public partial class MapGenerator
{
  private readonly FastNoiseLite fastNoiseLite;
  private readonly TileNoiseRange tileNoiseRange;

  protected MapGenerator(
    FastNoiseLite fastNoiseLite,
    TileNoiseRange tileNoiseRange)
  {
    this.fastNoiseLite = fastNoiseLite;
    this.tileNoiseRange = tileNoiseRange;
  }

  public virtual Tile GetTileOnPosition(Vector3I vec)
  {
    return tileNoiseRange.GetTileByNoise(fastNoiseLite.GetNoise3Dv(vec));
  }
}
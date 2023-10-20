namespace ProceduralGeneration;

public partial class TileNoise
{
  private const float MIN_NOISE_RANGE = -1.0f;
  private const float MAX_NOISE_RANGE = 1.0f;

  public float noiseMarker;
  public Tile tile;

  public TileNoise(float noiseMarker, Tile tile)
  {
    Exceptions.ThrowIfLessThan(noiseMarker, MIN_NOISE_RANGE);
    Exceptions.ThrowIfGreaterThan(noiseMarker, MAX_NOISE_RANGE);

    this.noiseMarker = noiseMarker;
    this.tile = tile;
  }
}
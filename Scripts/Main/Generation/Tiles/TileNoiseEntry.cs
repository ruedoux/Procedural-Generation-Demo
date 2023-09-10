
public partial class TileNoiseEntry
{
  private const float MIN_NOISE_RANGE = -1;
  private const float MAX_NOISE_RANGE = 1;

  public float noiseFrom;
  public float noiseTo;
  public Tile tile;

  public TileNoiseEntry(float noiseFrom, float noiseTo, Tile tile)
  {
    Exceptions.ThrowIfLessThan(noiseFrom, MIN_NOISE_RANGE);
    Exceptions.ThrowIfGreaterThan(noiseTo, MAX_NOISE_RANGE);
    Exceptions.ThrowIfGreaterThan(noiseFrom, noiseTo);

    this.noiseFrom = noiseFrom;
    this.noiseTo = noiseTo;
    this.tile = tile;
  }
}
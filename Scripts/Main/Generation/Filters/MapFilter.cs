using Godot;

namespace ProceduralGeneration;

public class MapFilter
{
  protected readonly Vector3I mapSize;
  protected readonly float noiseBoost;

  public MapFilter(Vector3I mapSize, float noiseBoost)
  {
    this.mapSize = mapSize;
    this.noiseBoost = noiseBoost;
  }

  public virtual float Filter(Vector3 position, float noise)
    => noise + noiseBoost;
}
using Godot;

namespace ProceduralGeneration;

public class MapFilter
{
  protected readonly Vector3I mapSize;
  protected readonly Vector3 mapMiddle;
  protected readonly float mapWidth;
  protected readonly float noiseBoost;

  public MapFilter(Vector3I mapSize, float noiseBoost)
  {
    this.mapSize = mapSize;
    this.noiseBoost = noiseBoost;

    mapMiddle = mapSize / 2;
    mapWidth = (mapSize.X + mapSize.Y) / 2;
  }

  public virtual float Filter(Vector3 position, float noise)
    => noise + noiseBoost;
}
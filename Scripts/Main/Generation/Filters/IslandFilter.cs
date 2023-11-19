using Godot;

namespace ProceduralGeneration;

public class IslandFilter : MapFilter
{
  private readonly int filterStrenght;
  public IslandFilter(Vector3I mapSize, float noiseBoost, int filterStrenght)
  : base(mapSize, noiseBoost)
  {
    this.filterStrenght = filterStrenght;
  }

  public override float Filter(Vector3 position, float noise)
  {
    noise = base.Filter(position, noise);
    Vector3 mapMiddle = mapSize / 2;
    float distanceToMiddle = mapMiddle.DistanceTo(position);
    return noise - (distanceToMiddle / mapMiddle.X * (3 * (float)filterStrenght / 100));
  }
}
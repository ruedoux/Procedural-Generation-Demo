using Godot;

namespace ProceduralGeneration;

public class IslandFilter : MapFilter
{
  public override float Filter(Vector3 position, float noise)
  {
    float distanceToMiddle = mapMiddle.DistanceTo(position);
    return base.Filter(position, noise) - DistanceFromMiddleReduction(distanceToMiddle);
  }

  private float DistanceFromMiddleReduction(float distanceToMiddle)
    => GetDistancePercentage(distanceToMiddle) * 3 * filterStrenght;

  private float GetDistancePercentage(float distanceToMiddle)
    => distanceToMiddle / mapWidth;
}
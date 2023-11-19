using System;
using Godot;

namespace ProceduralGeneration;

public class IslandFilter : MapFilter
{
  private readonly float filterStrenght;

  public IslandFilter(Vector3I mapSize, float noiseBoost, float filterStrenght)
  : base(mapSize, noiseBoost)
  {
    this.filterStrenght = Math.Clamp(filterStrenght, 0f, 1f);
  }

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
using System;
using Godot;

namespace ProceduralGeneration;

public class MapFilter
{
  protected Vector3I mapSize;
  protected Vector3 mapMiddle;
  protected float mapWidth;
  protected float noiseBoost;
  protected float filterStrenght;

  public void SetValues(Vector3I mapSize, float noiseBoost, float filterStrenght)
  {
    this.mapSize = mapSize;
    this.noiseBoost = noiseBoost;
    this.filterStrenght = Math.Clamp(filterStrenght, 0f, 1f);

    mapMiddle = mapSize / 2;
    mapWidth = (mapSize.X + mapSize.Y) / 2;
  }

  public virtual float Filter(Vector3 position, float noise)
    => noise + noiseBoost;
}
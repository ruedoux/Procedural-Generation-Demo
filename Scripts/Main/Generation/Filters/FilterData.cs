using System;
using System.Collections.Generic;
using Godot;

namespace ProceduralGeneration;

public class FilterData
{
  public enum FilterType { None, Island }

  private static readonly Dictionary<FilterType, Type> filterTypeMap = new()
  {
    {FilterType.None, typeof(MapFilter)},
    {FilterType.Island, typeof(IslandFilter)},
  };

  public static MapFilter GetMapFilter(
    FilterType selectedFilter, Vector3I mapSize, float filterBoost, float filterStrenght)
  {
    MapFilter mapFilter = Activator.CreateInstance(filterTypeMap[selectedFilter]) as MapFilter;
    mapFilter.SetValues(mapSize, filterBoost, filterStrenght);
    return mapFilter;
  }
}
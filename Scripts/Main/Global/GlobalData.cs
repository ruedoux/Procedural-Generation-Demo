using System;
using System.Collections.Generic;
using Godot;

public static class GlobalData
{
  public enum TileTypes { GRASS, FOREST, WATER, SNOW, STONE }

  public static readonly Dictionary<TileTypes, Color> DefaultTileColors = new() {
    {TileTypes.GRASS,  new Color(0.451f, 0.988f, 0.22f)},
    {TileTypes.FOREST, new Color(0.306f, 0.678f, 0.149f)},
    {TileTypes.WATER,  new Color(0.671f, 0.984f, 1f)},
    {TileTypes.SNOW,   new Color(1f, 1f, 1f)},
    {TileTypes.STONE,  new Color(0.78f, 0.78f, 0.78f)},
   };
}
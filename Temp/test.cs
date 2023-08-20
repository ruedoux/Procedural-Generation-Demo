using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;


public partial class test : Node2D
{
  public override void _Ready()
  {
    List<Color> colors = new();
    Vector2I tileSize = new(1, 1);

    for (int i = 0; i < 5; i++)
    {
      colors.Add(new Color(GD.Randf(), GD.Randf(), GD.Randf()));
    }

    ProceduralTileMapCreator.GenerateTileMap("test.tscn", tileSize, colors.ToArray());
  }
}
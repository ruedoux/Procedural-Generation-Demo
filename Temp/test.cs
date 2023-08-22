using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;


public partial class test : Node2D
{
  public override void _Ready()
  {
    Logger.LogInfo(1, 2, 3, 3.0, new Tile());
  }
}
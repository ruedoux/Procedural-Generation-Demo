using System;
using System.Collections.Generic;
using Godot;

namespace ProceduralGeneration;

public partial class MapRoot : MapRootUI
{
  private MapCamera mapCamera;
  private TileMap tileMap;

  private readonly Vector2I tileSize = new(2, 2);


  public override void _Ready()
  {
    InitializeUI();

    generateButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedGenerateButton)));
    saveImageButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedSaveImageButton)));
    saveSettingsButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedSaveSettingsButton)));
    loadSettingsButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedLoadSettingsButton)));

    saveImageDialog.Connect(
      FileDialog.SignalName.FileSelected, new Callable(this, nameof(SelectedSaveImageFile)));
    saveSettingsDialog.Connect(
      FileDialog.SignalName.FileSelected, new Callable(this, nameof(SelectedSaveSettingsFile)));
    loadSettingsDialog.Connect(
      FileDialog.SignalName.FileSelected, new Callable(this, nameof(SelectedLoadSettingsFile)));

    mapCamera = new(GetNode<Control>("MainUI/C/H/ButtonPanel/Null"));
    AddChild(mapCamera);
  }


  public void PressedGenerateButton()
  {
    ReplaceTileMap();
    GenerationSettings generationSettings = GetGenerationSettings();
    GetMapGenerator().FillTileMapWithNoise(
      tileMap, generationSettings.mapSize.X, generationSettings.mapSize.Y);

    mapCamera.maxCameraPosition = new Vector2(
      generationSettings.mapSize.X * tileSize.X,
      generationSettings.mapSize.Y * tileSize.Y);

    Logger.Log("Generation finished");
  }


  public void PressedSaveImageButton()
    => saveImageDialog.Show();

  public void PressedSaveSettingsButton()
    => saveSettingsDialog.Show();

  public void PressedLoadSettingsButton()
    => loadSettingsDialog.Show();


  public void SelectedSaveImageFile(string filePath)
  {
    GenerationSettings generationSettings = GetGenerationSettings();
    filePath += ".png";

    MapGenerator mapGenerator = GetMapGenerator();
    mapGenerator.CreateImageInPath(
      filePath, generationSettings.mapSize.X, generationSettings.mapSize.Y);

    Logger.Log("Saved image to: " + filePath);
  }

  public void SelectedSaveSettingsFile(string filePath)
  {
    filePath += ".settings";
    GetGenerationSettings().SaveInPath(filePath);
    Logger.Log("Saved settings to: " + filePath);
  }

  public void SelectedLoadSettingsFile(string filePath)
  {
    try
    {
      LoadSettings(new GenerationSettings().LoadFromPath(filePath));
      Logger.Log("Loaded settings from: " + filePath);
    }
    catch (Exception ex)
    {
      Logger.LogError(ex);
    }
  }


  private void ReplaceTileMap()
  {
    List<Color> colors = new();
    foreach (TileNoise tileNoise in GetGenerationSettings().tileNoises)
      colors.Add(tileNoise.tile.color);

    tileMap?.QueueFree();
    tileMap = ProceduralTileMapCreator.GenerateTileMap(tileSize, colors.ToArray());
    AddChild(tileMap);
  }


  private MapGenerator GetMapGenerator()
  {
    GenerationSettings generationSettings = GetGenerationSettings();
    return new MapGenerator(
      generationSettings.CreateFastNoiseLite(),
       new(generationSettings.tileNoises,
       new(-1, Colors.Red)),
       GetMapFilter());
  }


  private MapFilter GetMapFilter()
  {
    GenerationSettings generationSettings = GetGenerationSettings();
    return FilterData.GetMapFilter(
      generationSettings.filterType,
      GetMapSize(),
      generationSettings.filterBoost,
      generationSettings.filterStrength);
  }
}
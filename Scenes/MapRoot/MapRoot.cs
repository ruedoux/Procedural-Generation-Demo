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
    GetMapGenerator().FillTileMapWithNoise(
      tileMap, SanitizeIntField(mapWidthLineEdit), SanitizeIntField(mapHeightLineEdit));

    mapCamera.maxCameraPosition = new Vector2(
      SanitizeIntField(mapWidthLineEdit) * tileSize.X, SanitizeIntField(mapHeightLineEdit) * tileSize.Y);

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
    filePath += ".png";
    int width = SanitizeIntField(mapWidthLineEdit);
    int height = SanitizeIntField(mapHeightLineEdit);

    MapGenerator mapGenerator = GetMapGenerator();
    mapGenerator.CreateImageInPath(filePath, width, height);

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
    FilterData.FilterType mapFilterType = SanitizeEnum<FilterData.FilterType>(filterType);
    float strenght = SanitizeFloatField(filterStrenght);
    float boost = SanitizeFloatField(filterBoost);
    return FilterData.GetMapFilter(mapFilterType, GetMapSize(), boost, strenght);
  }
}
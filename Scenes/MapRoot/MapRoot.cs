using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

using static Godot.FastNoiseLite;

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
    generateImageButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedGenerateImageButton)));
    generateImageDialog.Connect(
      FileDialog.SignalName.FileSelected, new Callable(this, nameof(SelectedSaveFile)));

    mapCamera = new(GetNode<Control>("MainUI/C/H/B"));
    AddChild(mapCamera);

    //PressedGenerateButton();
  }

  public void PressedGenerateButton()
  {
    TileNoise[] tileNoises = GetTileNoises();
    ReplaceTileMap(tileNoises);

    MapGenerator mapGenerator = GetMapGenerator(tileNoises);
    mapGenerator.FillTileMapWithNoise(
      tileMap, SanitizeIntField(mapWidth), SanitizeIntField(mapHeight));

    Logger.Log("Generation finished");
  }

  public void PressedGenerateImageButton()
    => generateImageDialog.Show();

  public void SelectedSaveFile(string filePath)
  {
    filePath += ".png";
    int width = SanitizeIntField(mapWidth);
    int height = SanitizeIntField(mapHeight);

    MapGenerator mapGenerator = GetMapGenerator(GetTileNoises());
    mapGenerator.CreateImageInPath(filePath, width, height);

    Logger.Log("Saved image to: " + filePath);
  }

  private TileNoise[] GetTileNoises()
  {
    List<TileNoise> tileNoises = new();

    var TileContainerTiles = TileContainer.GetChildren();
    for (int i = 0; i < TileContainerTiles.Count; i++)
    {
      var TileUI = (HBoxContainer)TileContainerTiles[i];
      float value = SanitizeFloatField(TileUI.GetNode<LineEdit>("Value/LineEdit"));
      Color color = TileUI.GetNode<ColorPickerButton>("PickColor").GetPicker().Color;
      tileNoises.Add(new(value, new(i, color)));
    }

    return tileNoises.ToArray();
  }

  private void ReplaceTileMap(TileNoise[] tileNoises)
  {
    List<Color> colors = new();
    foreach (TileNoise tileNoise in tileNoises)
      colors.Add(tileNoise.tile.color);

    tileMap?.QueueFree();
    tileMap = ProceduralTileMapCreator.GenerateTileMap(tileSize, colors.ToArray());
    AddChild(tileMap);
  }

  private MapGenerator GetMapGenerator(TileNoise[] tileNoises)
  {
    FastNoiseLite fastNoiseLite = new()
    {
      NoiseType = SanitizeEnum<NoiseTypeEnum>(noiseType),
      CellularDistanceFunction = SanitizeEnum<CellularDistanceFunctionEnum>(cellularDistance),
      CellularReturnType = SanitizeEnum<CellularReturnTypeEnum>(cellularReturn),
      DomainWarpType = SanitizeEnum<DomainWarpTypeEnum>(domainWarp),
      DomainWarpFractalType = SanitizeEnum<DomainWarpFractalTypeEnum>(domainFractal),
      FractalType = SanitizeEnum<FractalTypeEnum>(fractal),
      Seed = SanitizeIntField(noiseSeed),
      FractalOctaves = SanitizeIntField(fractalOctaves),
      FractalLacunarity = SanitizeFloatField(fractalLacunarity),
      Frequency = SanitizeFloatField(noiseFrequency),
      CellularJitter = SanitizeFloatField(cellularJitter),
      FractalWeightedStrength = SanitizeFloatField(fractalStrenght),
      FractalPingPongStrength = SanitizeFloatField(fractalPingPong),
      FractalGain = SanitizeFloatField(fractalGain),
      DomainWarpAmplitude = SanitizeFloatField(domainAmplitude),
      DomainWarpFractalGain = SanitizeFloatField(domainGain),
      DomainWarpFractalLacunarity = SanitizeFloatField(domainLacunarity),
      DomainWarpFractalOctaves = SanitizeIntField(domainOctaves),
      DomainWarpFrequency = SanitizeFloatField(domainFrequency),
    };

    return new MapGenerator(
      fastNoiseLite, new(tileNoises.ToArray(), new(-1, Colors.Red)), GetMapFilter());
  }

  private MapFilter GetMapFilter()
  {
    FilterData.FilterType mapFilterType = SanitizeEnum<FilterData.FilterType>(filterType);
    float strenght = SanitizeFloatField(filterStrenght);
    float boost = SanitizeFloatField(filterBoost);
    return FilterData.GetMapFilter(mapFilterType, GetMapSize(), boost, strenght);
  }

  private Vector3I GetMapSize()
    => new(SanitizeIntField(mapWidth), SanitizeIntField(mapHeight), 0);

  private static T SanitizeEnum<T>(OptionButton enumOptionButton) where T : Enum
  {
    T result = InputSanitizer.SanitizeEnum<T>(
      enumOptionButton.GetItemText(enumOptionButton.GetSelectedId()));
    return result;
  }

  private static float SanitizeFloatField(LineEdit lineEdit)
  {
    float result = InputSanitizer.SanitizeFloat(lineEdit.Text);
    lineEdit.Text = result.ToString();
    return result;
  }

  private static int SanitizeIntField(LineEdit lineEdit)
  {
    int result = InputSanitizer.SanitizeInt(lineEdit.Text);
    lineEdit.Text = result.ToString();
    return result;
  }
}
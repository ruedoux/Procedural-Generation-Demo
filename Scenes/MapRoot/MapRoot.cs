using System;
using System.Linq;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRoot : MapRootUI
{
  private MapCamera mapCamera;

  private readonly TileDatabase tileDatabase = new();
  private readonly TileMap tileMap;
  private readonly Vector2I tileSize = new(2, 2);


  public MapRoot()
  {
    Color[] colors = tileDatabase.entries
      .OrderBy(keyPair => (int)keyPair.Key)
      .Select(keyPair => keyPair.Value.color).ToArray();

    tileMap = ProceduralTileMapCreator.GenerateTileMap(tileSize, colors);
  }

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
    AddChild(tileMap);

    PressedGenerateButton();
  }

  public void PressedGenerateButton()
  {
    MapGenerator mapGenerator = GetMapGenerator();
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

    MapGenerator mapGenerator = GetMapGenerator();
    mapGenerator.CreateImageInPath(filePath, width, height);

    Logger.Log("Saved image to: " + filePath);
  }

  private MapGenerator GetMapGenerator()
  {
    TileNoise[] tileNoises = new TileNoise[] {
      new(0f, tileDatabase.GetEntry(TileDatabase.TileType.WATER)),
      new(0.1f, tileDatabase.GetEntry(TileDatabase.TileType.SAND)),
      new(0.6f, tileDatabase.GetEntry(TileDatabase.TileType.GRASS)),
      new(0.9f, tileDatabase.GetEntry(TileDatabase.TileType.FOREST)),
      new(0.95f, tileDatabase.GetEntry(TileDatabase.TileType.STONE)),
      new(1.0f, tileDatabase.GetEntry(TileDatabase.TileType.SNOW)),
    };

    TileNoiseRange tileNoiseRange = new(
      tileNoises, tileDatabase.GetEntry(TileDatabase.TileType.NONE));

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

    return new MapGenerator(fastNoiseLite, tileNoiseRange);
  }

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
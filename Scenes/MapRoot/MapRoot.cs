using System;
using System.Linq;
using Godot;

namespace ProceduralGeneration;

public partial class MapRoot : Node2D
{
  private MapCamera mapCamera = new();

  private LineEdit mapWidth;
  private LineEdit mapHeight;

  private LineEdit noiseSeed;
  private OptionButton noiseType;
  private OptionButton noiseDistance;
  private OptionButton noiseReturn;
  private OptionButton noiseDomainWarp;
  private OptionButton noiseDomainFractal;
  private OptionButton noiseFractal;

  private LineEdit noiseOctaves;
  private LineEdit noiseLacunarity;
  private LineEdit noiseFrequency;

  private Button generateButton;


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
    mapWidth = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Size/V/Width/LineEdit");
    mapHeight = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Size/V/Height/LineEdit");

    noiseSeed = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Seed/LineEdit");
    noiseType = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/Type/OptionButton");
    noiseDistance = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/Distance/OptionButton");
    noiseReturn = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/Return/OptionButton");
    noiseDomainWarp = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/DomainWarp/OptionButton");
    noiseDomainFractal = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/DomainFractal/OptionButton");
    noiseFractal = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/Fractal/OptionButton");

    noiseOctaves = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Octaves/LineEdit");
    noiseLacunarity = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Lacunarity/LineEdit");
    noiseFrequency = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Frequency/LineEdit");

    generateButton = GetNode<Button>("MainUI/C/H/B/P/S/V/Generate/Button");

    FillOptionWithEnum<FastNoiseLite.NoiseTypeEnum>(noiseType);
    FillOptionWithEnum<FastNoiseLite.CellularDistanceFunctionEnum>(noiseDistance);
    FillOptionWithEnum<FastNoiseLite.CellularReturnTypeEnum>(noiseReturn);
    FillOptionWithEnum<FastNoiseLite.DomainWarpTypeEnum>(noiseDomainWarp);
    FillOptionWithEnum<FastNoiseLite.DomainWarpFractalTypeEnum>(noiseDomainFractal);
    FillOptionWithEnum<FastNoiseLite.FractalTypeEnum>(noiseFractal);

    generateButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedGenerateButton)));

    AddChild(mapCamera);
    AddChild(tileMap);
    PressedGenerateButton();
  }

  public void PressedGenerateButton()
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
      NoiseType = SanitizeEnum<FastNoiseLite.NoiseTypeEnum>(noiseType),
      CellularDistanceFunction = SanitizeEnum<FastNoiseLite.CellularDistanceFunctionEnum>(noiseDistance),
      CellularReturnType = SanitizeEnum<FastNoiseLite.CellularReturnTypeEnum>(noiseReturn),
      DomainWarpType = SanitizeEnum<FastNoiseLite.DomainWarpTypeEnum>(noiseDomainWarp),
      DomainWarpFractalType = SanitizeEnum<FastNoiseLite.DomainWarpFractalTypeEnum>(noiseDomainFractal),
      FractalType = SanitizeEnum<FastNoiseLite.FractalTypeEnum>(noiseFractal),
      Seed = SanitizeIntField(noiseSeed),
      FractalOctaves = SanitizeIntField(noiseOctaves),
      FractalLacunarity = SanitizeFloatField(noiseLacunarity),
      Frequency = SanitizeFloatField(noiseFrequency)
    };

    MapGenerator mapGenerator = new(fastNoiseLite, tileNoiseRange);
    mapGenerator.FillTileMapWithNoise(
      tileMap, SanitizeIntField(mapWidth), SanitizeIntField(mapHeight));
  }

  private static void FillOptionWithEnum<T>(OptionButton enumOptionButton) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
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
using System;
using System.Linq;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRoot : Node2D
{
  private MapCamera mapCamera;

  private LineEdit mapWidth;
  private LineEdit mapHeight;

  private LineEdit noiseSeed;
  private OptionButton noiseType;
  private OptionButton noiseDistance;
  private OptionButton noiseReturn;
  private OptionButton noiseDomainWarp;
  private OptionButton noiseDomainFractal;
  private OptionButton noiseFractal;

  private LineEdit fractalGain;
  private LineEdit fractalOctaves;
  private LineEdit fractalLacunarity;
  private LineEdit noiseFrequency;
  private LineEdit cellularJitter;
  private LineEdit fractalStrenght;
  private LineEdit fractalPingPong;

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
    noiseType = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/NoiseType/OptionButton");
    noiseDistance = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/CellularDistance/OptionButton");
    noiseReturn = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/CellularReturn/OptionButton");
    noiseDomainWarp = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/DomainWarp/OptionButton");
    noiseDomainFractal = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/DomainFractal/OptionButton");
    noiseFractal = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/Fractal/OptionButton");

    fractalGain = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalGain/LineEdit");
    fractalOctaves = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalOctaves/LineEdit");
    fractalLacunarity = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalLacunarity/LineEdit");
    fractalStrenght = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalStrenght/LineEdit");
    fractalPingPong = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalPingPong/LineEdit"); ;
    noiseFrequency = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Frequency/LineEdit");
    cellularJitter = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/CellularJitter/LineEdit");

    generateButton = GetNode<Button>("MainUI/C/H/B/P/S/V/Generate/Button");

    FillOptionWithEnum(noiseType, NoiseTypeEnum.SimplexSmooth);
    FillOptionWithEnum(noiseDistance, CellularDistanceFunctionEnum.Euclidean);
    FillOptionWithEnum(noiseReturn, CellularReturnTypeEnum.Distance);
    FillOptionWithEnum(noiseDomainWarp, DomainWarpTypeEnum.Simplex);
    FillOptionWithEnum(noiseDomainFractal, DomainWarpFractalTypeEnum.Progressive);
    FillOptionWithEnum(noiseFractal, FractalTypeEnum.Fbm);

    generateButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedGenerateButton)));

    mapCamera = new(GetNode<Control>("MainUI/C/H/B"));
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
      NoiseType = SanitizeEnum<NoiseTypeEnum>(noiseType),
      CellularDistanceFunction = SanitizeEnum<CellularDistanceFunctionEnum>(noiseDistance),
      CellularReturnType = SanitizeEnum<CellularReturnTypeEnum>(noiseReturn),
      DomainWarpType = SanitizeEnum<DomainWarpTypeEnum>(noiseDomainWarp),
      DomainWarpFractalType = SanitizeEnum<DomainWarpFractalTypeEnum>(noiseDomainFractal),
      FractalType = SanitizeEnum<FractalTypeEnum>(noiseFractal),
      Seed = SanitizeIntField(noiseSeed),
      FractalOctaves = SanitizeIntField(fractalOctaves),
      FractalLacunarity = SanitizeFloatField(fractalLacunarity),
      Frequency = SanitizeFloatField(noiseFrequency),
      CellularJitter = SanitizeFloatField(cellularJitter),
      FractalWeightedStrength = SanitizeFloatField(fractalStrenght),
      FractalPingPongStrength = SanitizeFloatField(fractalPingPong),
      FractalGain = SanitizeFloatField(fractalGain),
    };

    MapGenerator mapGenerator = new(fastNoiseLite, tileNoiseRange);
    mapGenerator.FillTileMapWithNoise(
      tileMap, SanitizeIntField(mapWidth), SanitizeIntField(mapHeight));
    Logger.Log("Generation finished");
  }

  private static void FillOptionWithEnum<T>(
    OptionButton enumOptionButton, T defaultValue) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
    enumOptionButton.Select(Convert.ToInt32(defaultValue));
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
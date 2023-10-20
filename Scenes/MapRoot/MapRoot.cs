using System.Linq;
using Godot;

namespace ProceduralGeneration;

public partial class MapRoot : Node2D
{
  LineEdit noiseSeed;
  LineEdit noiseOctaves;
  LineEdit noiseLacunarity;
  LineEdit noiseFrequency;
  OptionButton noiseType;
  Button generateButton;


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
    noiseSeed = GetNode<LineEdit>("MainUI/C/H/B/P/V/Noise/V/Seed/LineEdit");
    noiseOctaves = GetNode<LineEdit>("MainUI/C/H/B/P/V/Noise/V/Octaves/LineEdit");
    noiseLacunarity = GetNode<LineEdit>("MainUI/C/H/B/P/V/Noise/V/Lacunarity/LineEdit");
    noiseFrequency = GetNode<LineEdit>("MainUI/C/H/B/P/V/Noise/V/Frequency/LineEdit");
    noiseType = GetNode<OptionButton>("MainUI/C/H/B/P/V/Noise/V/Type/OptionButton");
    generateButton = GetNode<Button>("MainUI/C/H/B/P/V/Generate/Button");

    generateButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedGenerateButton)));

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
      new(0.98f, tileDatabase.GetEntry(TileDatabase.TileType.STONE)),
      new(1.0f, tileDatabase.GetEntry(TileDatabase.TileType.SNOW)),
    };

    TileNoiseRange tileNoiseRange = new(
      tileNoises, tileDatabase.GetEntry(TileDatabase.TileType.NONE));

    FastNoiseLite fastNoiseLite = new()
    {
      NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
      Seed = SanitizeIntField(noiseSeed),
      FractalOctaves = SanitizeIntField(noiseOctaves),
      FractalLacunarity = SanitizeFloatField(noiseLacunarity),
      Frequency = SanitizeFloatField(noiseFrequency)
    };
    MapGenerator mapGenerator = new(fastNoiseLite, tileNoiseRange);
    mapGenerator.FillTileMapWithNoise(tileMap);
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
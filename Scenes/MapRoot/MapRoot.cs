using System.Linq;
using Godot;

public partial class MapRoot : Node2D
{
  private readonly TileDatabase tileDatabase = new();
  private readonly TileNoiseRange tileNoiseRange;
  private readonly MapGenerator mapGenerator;
  private readonly TileMap tileMap;

  private readonly Vector2I tileSize = new(2, 2);

  public MapRoot()
  {
    Color[] colors = tileDatabase.entries
      .OrderBy(keyPair => (int)keyPair.Key)
      .Select(keyPair => keyPair.Value.color).ToArray();

    tileMap = ProceduralTileMapCreator.GenerateTileMap(tileSize, colors);

    TileNoise[] tileNoises = new TileNoise[] {
      new(0f, tileDatabase.GetEntry(TileDatabase.TileType.WATER)),
      new(0.1f, tileDatabase.GetEntry(TileDatabase.TileType.SAND)),
      new(0.6f, tileDatabase.GetEntry(TileDatabase.TileType.GRASS)),
      new(0.9f, tileDatabase.GetEntry(TileDatabase.TileType.FOREST)),
      new(0.98f, tileDatabase.GetEntry(TileDatabase.TileType.STONE)),
      new(1.0f, tileDatabase.GetEntry(TileDatabase.TileType.SNOW)),
    };

    tileNoiseRange = new(tileNoises, tileDatabase.GetEntry(TileDatabase.TileType.NONE));

    FastNoiseLite fastNoiseLite = new()
    {
      NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
      Seed = 1,
      FractalOctaves = 1,
      FractalLacunarity = 0,
      Frequency = 0.03f
    };

    mapGenerator = new(fastNoiseLite, tileNoiseRange);
    FillTileMapWithNoise();
  }

  public override void _Ready()
  {
    AddChild(tileMap);
  }

  private void FillTileMapWithNoise()
  {
    Vector2 size = new(600, 400);

    for (int x = 0; x < size.X; x++)
      for (int y = 0; y < size.Y; y++)
      {
        Tile tile = mapGenerator.GetTileOnPosition(new Vector3I(x, y, 0));
        tileMap.SetCell(0, new Vector2I(x, y), 0, new Vector2I(tile.tileId, 0));
      }
  }
}
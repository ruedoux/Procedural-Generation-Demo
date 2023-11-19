using SGT;
using ProceduralGeneration;
using Godot;

namespace UnitTests;

public class TileNoiseRangeTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void Constructor_shouldCreateValidObject_whenValuesValid()
  {
    // Given
    Tile defaultTile = new();
    TileNoise firstHalfTileNoise = new(0f, new Tile(1));
    TileNoise secondQuarterTileNoise = new(0.5f, new Tile(2));
    TileNoise[] tileNoises = new TileNoise[] { firstHalfTileNoise, secondQuarterTileNoise };

    // When
    TileNoiseRange tileNoiseRange = new(tileNoises, defaultTile);

    // Then
    for (uint i = 0; i < 49; i++)
      Assertions.AssertEqual(firstHalfTileNoise.tile, tileNoiseRange.range[i], $"For index: {i}");

    for (uint i = 50; i < 74; i++)
      Assertions.AssertEqual(secondQuarterTileNoise.tile, tileNoiseRange.range[i], $"For index: {i}");

    for (uint i = 75; i < 100; i++)
      Assertions.AssertEqual(defaultTile, tileNoiseRange.range[i], $"For index: {i}");
  }

  [SimpleTestMethod]
  public void GetTileByNoise_shouldReturnCorrectTile_whenInvoked()
  {
    // Given
    Tile defaultTile = new();
    TileNoise firstHalfTileNoise = new(0f, new Tile(1));
    TileNoise secondQuarterTileNoise = new(0.5f, new Tile(2));
    TileNoise[] tileNoises = new TileNoise[] { firstHalfTileNoise, secondQuarterTileNoise };

    // When
    TileNoiseRange tileNoiseRange = new(tileNoises, defaultTile);

    // Then
    Assertions.AssertEqual(firstHalfTileNoise.tile, tileNoiseRange.GetTileByNoise(-1.0f));
    Assertions.AssertEqual(secondQuarterTileNoise.tile, tileNoiseRange.GetTileByNoise(0f));
    Assertions.AssertEqual(defaultTile, tileNoiseRange.GetTileByNoise(1.0f));
  }
}
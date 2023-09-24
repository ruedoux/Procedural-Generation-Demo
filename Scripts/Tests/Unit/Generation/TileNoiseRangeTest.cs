using System.Collections.Generic;
using Godot;
using SGT;

namespace UnitTests;


public class TileNoiseRangeTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void Constructor_shouldCreateValidObject_whenValuesValid()
  {
    // Given
    Tile defaultTile = new();
    TileNoise firstHalfTileNoise = new(-1, 0, new Tile(1));
    TileNoise secondQuarterTileNoise = new(0, 0.5f, new Tile(2));
    List<TileNoise> tileNoises = new()
    {
      firstHalfTileNoise, secondQuarterTileNoise
    };

    // When
    TileNoiseRange tileNoiseRange = new(tileNoises, defaultTile);

    // Then
    for (uint i = 0; i < 50; i++)
    {
      Assertions.AssertEqual(
        firstHalfTileNoise.tile, tileNoiseRange.range[i], $"For index: {i}");
    }

    for (uint i = 50; i < 75; i++)
    {
      Assertions.AssertEqual(
        secondQuarterTileNoise.tile, tileNoiseRange.range[i], $"For index: {i}");
    }

    for (uint i = 75; i < 100; i++)
    {
      Assertions.AssertEqual(
        defaultTile, tileNoiseRange.range[i], $"For index: {i}");
    }
  }
}
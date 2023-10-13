using SGT;

namespace UnitTests;


public class TileNoiseTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void Constructor_shouldCreateValidObject_whenValuesValid()
  {
    // Given
    Tile tile = new(1);
    float noiseMarker = -1;


    // When
    TileNoise tileNoise = new(noiseMarker, tile);

    // Then
    Assertions.AssertEqual(tileNoise.noiseMarker, noiseMarker);
    Assertions.AssertEqual(tileNoise.tile, tile);
  }

  [SimpleTestMethod]
  public void Constructor_shouldThrowException_whenValuesInvalid()
  {
    // Given
    Tile tile = new(1);

    // When
    // Then
    Assertions.AssertThrows<WrongValueException>(() =>
      new TileNoise(-1.1f, tile));
    Assertions.AssertThrows<WrongValueException>(() =>
      new TileNoise(1.1f, tile));
  }
}
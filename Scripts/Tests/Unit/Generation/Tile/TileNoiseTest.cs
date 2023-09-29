using SGT;

namespace UnitTests;


public class TileNoiseTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void Constructor_shouldCreateValidObject_whenValuesValid()
  {
    // Given
    Tile tile = new(1, 2);
    float noiseFrom = -1;
    float noiseTo = 1;

    // When
    TileNoise tileNoise = new(noiseFrom, noiseTo, tile);

    // Then
    Assertions.AssertEqual(tileNoise.noiseFrom, noiseFrom);
    Assertions.AssertEqual(tileNoise.noiseTo, noiseTo);
    Assertions.AssertEqual(tileNoise.tile, tile);
  }

  [SimpleTestMethod]
  public void Constructor_shouldThrowException_whenValuesInvalid()
  {
    // Given
    Tile tile = new(1, 2);

    // When
    // Then
    Assertions.AssertThrows<WrongValueException>(() =>
      new TileNoise(-1.1f, 1f, tile));
    Assertions.AssertThrows<WrongValueException>(() =>
      new TileNoise(-1f, 1.1f, tile));
    Assertions.AssertThrows<WrongValueException>(() =>
      new TileNoise(0f, -1f, tile));
  }
}
namespace UnitTests;
using System;
using SGT;

public class TileTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void Serialization_shouldCreateValidObject_whenSerialized()
  {
    // Given
    Random random = new();

    int tileId = random.Next();
    int layer = random.Next();

    // When
    Tile tile = new(tileId, layer);
    string jsonString = tile.ToJsonString();
    Tile tileFromJson = new Tile().FromJsonString<Tile>(jsonString);

    // Then
    Assertions.AssertEqual(tile, tileFromJson);
  }

  [SimpleTestMethod]
  public void EqualAndHashCode_shouldCompare_whenCalled()
  {
    // Given
    Tile tileEqual1 = new(1, 2);
    Tile tileEqual2 = new(1, 2);
    Tile tileNotEqual = new(2, 1);

    // When
    // Then
    Assertions.AssertEqual(tileEqual1, tileEqual1);
    Assertions.AssertEqual(tileEqual1, tileEqual2);
    Assertions.AssertNotEqual(tileEqual1, tileNotEqual);

    Assertions.AssertEqual(tileEqual1.GetHashCode(), tileEqual1.GetHashCode());
    Assertions.AssertEqual(tileEqual1.GetHashCode(), tileEqual2.GetHashCode());
    Assertions.AssertNotEqual(tileEqual1.GetHashCode(), tileNotEqual.GetHashCode());
  }
}
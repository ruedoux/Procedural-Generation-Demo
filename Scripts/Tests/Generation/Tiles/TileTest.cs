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
    Assertions.AssertEqual(tile.id, tileFromJson.id);
    Assertions.AssertEqual(tile.layer, tileFromJson.layer);
  }
}
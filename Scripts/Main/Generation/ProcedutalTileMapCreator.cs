using System;
using System.Collections.Generic;
using Godot;

public static partial class ProceduralTileMapCreator
{
  public static void GenerateTileMap(string path, Vector2I tileSize, Color[] colors)
  {
    Exceptions.ThrowIfEqual(colors.Length, 0);
    Exceptions.ThrowIfNotEqual(tileSize.X, tileSize.Y);

    List<Image> tiles = new();
    foreach (Color color in colors)
      tiles.Add(CreateTileImage(tileSize, color));


    var source = CreateTileSourceFromTiles(tiles.ToArray());
    var tileSet = CreateTileSet(new List<TileSetAtlasSource> { source }.ToArray(), tileSize);
    var tileMap = CreateTileMap(tileSet);

    PackedScene packedScene = new();
    packedScene.Pack(tileMap);

    ResourceSaver.Save(packedScene, path);
    Logger.Log("Generated TileMap: " + path);
  }

  private static TileMap CreateTileMap(TileSet tileSet)
  {
    TileMap tileMap = new()
    {
      TileSet = tileSet,
      Name = "GeneratedTileMap"
    };

    tileMap.RemoveLayer(0);
    for (int i = 0; i < tileSet.GetSourceCount(); i++)
    {
      tileMap.AddLayer(i);
      tileMap.SetLayerName(i, i.ToString());
      tileMap.SetLayerZIndex(i, i);
    }

    return tileMap;
  }

  private static TileSet CreateTileSet(TileSetAtlasSource[] sources, Vector2I tileSize)
  {
    Exceptions.ThrowIfEqual(sources.Length, 0);

    TileSet tileSet = new() { TileSize = tileSize };
    foreach (TileSetAtlasSource source in sources)
      tileSet.AddSource(source);

    return tileSet;
  }

  private static TileSetAtlasSource CreateTileSourceFromTiles(Image[] tiles)
  {
    Vector2I tileSize = tiles[0].GetSize();

    Exceptions.ThrowIfEqual(tiles.Length, 0);
    Exceptions.ThrowIfEqual(tileSize, new Vector2I());

    TileSetAtlasSource tileSetAtlasSource = new()
    {
      TextureRegionSize = tileSize,
      Texture = ImageTexture.CreateFromImage(CreateTileTextureFromTiles(tiles))
    };

    for (int i = 0; i < tiles.Length; i++)
      tileSetAtlasSource.CreateTile(new Vector2I(i, 0), GetTileDimensions(tileSize));


    return tileSetAtlasSource;
  }

  private static Image CreateTileImage(Vector2I tileSize, Color color)
  {
    Image image = Image.Create(tileSize.X, tileSize.Y, true, Image.Format.Rgba8);
    image.Fill(color);
    return image;
  }

  private static Image CreateTileTextureFromTiles(Image[] tiles)
  {
    Exceptions.ThrowIfEqual(tiles.Length, 0);

    Vector2I tileSize = tiles[0].GetSize();
    foreach (Image tile in tiles)
      Exceptions.ThrowIfNotEqual(tileSize, tile.GetSize());


    Image image = Image.Create(
      tileSize.X * tiles.Length, tileSize.Y, true, Image.Format.Rgba8);

    for (int i = 0; i < tiles.Length; i++)
    {
      new Rect2I(new Vector2I(), tileSize);
      image.BlitRect(
        tiles[i],
        new Rect2I(new Vector2I(), tileSize),
        new Vector2I(tileSize.X * i, 0));
    }

    return image;
  }

  private static Vector2I GetTileDimensions(Vector2I tileSize)
  {
    int smallerDimension = tileSize.X;
    if (smallerDimension > tileSize.Y)
      smallerDimension = tileSize.Y;

    return tileSize / smallerDimension;
  }
}
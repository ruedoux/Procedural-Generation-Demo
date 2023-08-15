using Godot;

public static partial class TileSetCreator
{
  public static Image CreateTileImage(int width, int height, Color color)
  {
    Image image = Image.Create(width, height, true, Image.Format.Rgba8);
    image.Fill(color);
    return image;
  }

  public static TileSetAtlasSource CreateTileSourceFromTiles(Image[] tiles)
  {
    Exceptions.ThrowIfEqual(tiles.Length, 0);

    TileSetAtlasSource tileSetAtlasSource = new()
    {
      Texture = ImageTexture.CreateFromImage(CreateTileTextureFromTiles(tiles))
    };

    Vector2I tileSize = tiles[0].GetSize();
    for (int i = 0; i < tiles.Length; i++)
    {
      tileSetAtlasSource.CreateTile(
        new Vector2I(tileSize.X * i, 0), tileSize);
    }

    return tileSetAtlasSource;
  }

  public static Image CreateTileTextureFromTiles(Image[] tiles)
  {
    Exceptions.ThrowIfEqual(tiles.Length, 0);

    Vector2I tileSize = tiles[0].GetSize();
    foreach (Image tile in tiles)
    {
      Exceptions.ThrowIfNotEqual(tileSize, tile.GetSize());
    }

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

  public static TileSet CreateTileSetFromSources(TileSetAtlasSource[] sources)
  {
    Exceptions.ThrowIfEqual(sources.Length, 0);

    TileSet tileSet = new();
    foreach (TileSetAtlasSource source in sources)
    {
      tileSet.AddSource(source);
    }

    return tileSet;
  }
}

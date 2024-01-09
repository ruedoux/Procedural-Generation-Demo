using System.Collections.Generic;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRoot : MapRootUI
{
  private MapCamera mapCamera;
  private TileMap tileMap;

  private readonly Vector2I tileSize = new(2, 2);


  public override void _Ready()
  {
    InitializeUI();

    generateButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedGenerateButton)));
    saveImageButton.Connect(
      Button.SignalName.Pressed, new Callable(this, nameof(PressedSaveImageButton)));
    saveImageDialog.Connect(
      FileDialog.SignalName.FileSelected, new Callable(this, nameof(SelectedSaveFile)));

    mapCamera = new(GetNode<Control>("MainUI/C/H/ButtonPanel/Null"));
    AddChild(mapCamera);
  }


  public void PressedGenerateButton()
  {
    ReplaceTileMap();
    GetMapGenerator().FillTileMapWithNoise(
      tileMap, SanitizeIntField(mapWidth), SanitizeIntField(mapHeight));

    mapCamera.maxCameraPosition = new Vector2(
      SanitizeIntField(mapWidth) * tileSize.X, SanitizeIntField(mapHeight) * tileSize.Y);

    Logger.Log("Generation finished");
  }


  public void PressedSaveImageButton()
    => saveImageDialog.Show();


  public void SelectedSaveFile(string filePath)
  {
    filePath += ".png";
    int width = SanitizeIntField(mapWidth);
    int height = SanitizeIntField(mapHeight);

    MapGenerator mapGenerator = GetMapGenerator();
    mapGenerator.CreateImageInPath(filePath, width, height);

    Logger.Log("Saved image to: " + filePath);
  }


  private void ReplaceTileMap()
  {
    List<Color> colors = new();
    foreach (TileNoise tileNoise in GetGenerationSettings().tileNoises)
      colors.Add(tileNoise.tile.color);

    tileMap?.QueueFree();
    tileMap = ProceduralTileMapCreator.GenerateTileMap(tileSize, colors.ToArray());
    AddChild(tileMap);
  }


  private MapGenerator GetMapGenerator()
  {
    GenerationSettings generationSettings = GetGenerationSettings();
    return new MapGenerator(
      generationSettings.CreateFastNoiseLite(),
       new(generationSettings.tileNoises,
       new(-1, Colors.Red)),
       GetMapFilter());
  }


  private MapFilter GetMapFilter()
  {
    FilterData.FilterType mapFilterType = SanitizeEnum<FilterData.FilterType>(filterType);
    float strenght = SanitizeFloatField(filterStrenght);
    float boost = SanitizeFloatField(filterBoost);
    return FilterData.GetMapFilter(mapFilterType, GetMapSize(), boost, strenght);
  }
}
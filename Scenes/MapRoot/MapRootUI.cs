using System;
using System.Collections.Generic;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRootUI : Node2D
{
  protected Control TileContainer;

  protected LineEdit mapWidth;
  protected LineEdit mapHeight;

  protected Control optionMenu;
  protected Control filterMenu;
  protected LineEdit noiseSeed;
  protected OptionButton noiseType;
  protected OptionButton cellularDistance;
  protected OptionButton cellularReturn;
  protected OptionButton domainWarp;
  protected OptionButton domainFractal;
  protected OptionButton fractal;
  protected OptionButton filterType;

  protected LineEdit fractalGain;
  protected LineEdit fractalOctaves;
  protected LineEdit fractalLacunarity;
  protected LineEdit noiseFrequency;
  protected LineEdit cellularJitter;
  protected LineEdit fractalStrenght;
  protected LineEdit fractalPingPong;
  protected LineEdit domainAmplitude;
  protected LineEdit domainGain;
  protected LineEdit domainLacunarity;
  protected LineEdit domainOctaves;
  protected LineEdit domainFrequency;
  protected LineEdit filterStrenght;
  protected LineEdit filterBoost;

  protected Button generateButton;
  protected Button generateImageButton;
  protected FileDialog generateImageDialog;

  protected void InitializeUI()
  {
    TileContainer = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/V/S/V/TilesContainer/Container/Tiles/V/TileContainer");

    mapWidth = GetNode<LineEdit>("MainUI/C/H/OptionPanel/B/P/V/S/V/SizeContainer/Container/Size/V/Width/LineEdit");
    mapHeight = GetNode<LineEdit>("MainUI/C/H/OptionPanel/B/P/V/S/V/SizeContainer/Container/Size/V/Height/LineEdit");

    optionMenu = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/V/S/V/NoiseContainer/Container/Noise/V");
    filterMenu = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/V/S/V/FilterContainer/Container/Filter/V");
    noiseSeed = optionMenu.GetNode<LineEdit>("Seed/LineEdit");
    noiseType = optionMenu.GetNode<OptionButton>("NoiseType/OptionButton");
    cellularDistance = optionMenu.GetNode<OptionButton>("CellularDistance/OptionButton");
    cellularReturn = optionMenu.GetNode<OptionButton>("CellularReturn/OptionButton");
    domainWarp = optionMenu.GetNode<OptionButton>("DomainWarp/OptionButton");
    domainFractal = optionMenu.GetNode<OptionButton>("DomainFractal/OptionButton");
    fractal = optionMenu.GetNode<OptionButton>("Fractal/OptionButton");
    filterType = filterMenu.GetNode<OptionButton>("Type/OptionButton");

    fractalGain = optionMenu.GetNode<LineEdit>("FractalGain/LineEdit");
    fractalOctaves = optionMenu.GetNode<LineEdit>("FractalOctaves/LineEdit");
    fractalLacunarity = optionMenu.GetNode<LineEdit>("FractalLacunarity/LineEdit");
    fractalStrenght = optionMenu.GetNode<LineEdit>("FractalStrenght/LineEdit");
    fractalPingPong = optionMenu.GetNode<LineEdit>("FractalPingPong/LineEdit"); ;
    noiseFrequency = optionMenu.GetNode<LineEdit>("Frequency/LineEdit");
    cellularJitter = optionMenu.GetNode<LineEdit>("CellularJitter/LineEdit");
    domainAmplitude = optionMenu.GetNode<LineEdit>("DomainAmplitude/LineEdit");
    domainGain = optionMenu.GetNode<LineEdit>("DomainGain/LineEdit");
    domainLacunarity = optionMenu.GetNode<LineEdit>("DomainLacunarity/LineEdit");
    domainOctaves = optionMenu.GetNode<LineEdit>("DomainOctaves/LineEdit");
    domainFrequency = optionMenu.GetNode<LineEdit>("DomainFrequency/LineEdit");
    filterStrenght = filterMenu.GetNode<LineEdit>("Strenght/LineEdit");
    filterBoost = filterMenu.GetNode<LineEdit>("Boost/LineEdit");

    generateButton = GetNode<Button>("MainUI/C/H/ButtonPanel/H/P/H/Generate/Button");
    generateImageButton = GetNode<Button>("MainUI/C/H/ButtonPanel/H/P/H/GenerateImage/Button");
    generateImageDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/H/P/H/GenerateImage/FileDialog");

    FillOptionWithEnum(noiseType, NoiseTypeEnum.SimplexSmooth);
    FillOptionWithEnum(cellularDistance, CellularDistanceFunctionEnum.Euclidean);
    FillOptionWithEnum(cellularReturn, CellularReturnTypeEnum.Distance);
    FillOptionWithEnum(domainWarp, DomainWarpTypeEnum.Simplex);
    FillOptionWithEnum(domainFractal, DomainWarpFractalTypeEnum.Progressive);
    FillOptionWithEnum(fractal, FractalTypeEnum.Fbm);
    FillOptionWithEnum(filterType, FilterData.FilterType.None);
  }

  protected TileNoise[] GetTileNoises()
  {
    List<TileNoise> tileNoises = new();

    var TileContainerTiles = TileContainer.GetChildren();
    for (int i = 0; i < TileContainerTiles.Count; i++)
    {
      var TileUI = (HBoxContainer)TileContainerTiles[i];
      float value = SanitizeFloatField(TileUI.GetNode<LineEdit>("Value/LineEdit"));
      Color color = TileUI.GetNode<ColorPickerButton>("PickColor").GetPicker().Color;
      tileNoises.Add(new(value, new(i, color)));
    }

    return tileNoises.ToArray();
  }


  protected GenerationSettings GetGenerationSettings()
  {
    return new()
    {
      tileNoises = GetTileNoises(),
      mapSize = GetMapSize(),
      noiseType = SanitizeEnum<NoiseTypeEnum>(noiseType),
      cellularDistanceFunction = SanitizeEnum<CellularDistanceFunctionEnum>(cellularDistance),
      cellularReturnType = SanitizeEnum<CellularReturnTypeEnum>(cellularReturn),
      domainWarpType = SanitizeEnum<DomainWarpTypeEnum>(domainWarp),
      domainWarpFractalType = SanitizeEnum<DomainWarpFractalTypeEnum>(domainFractal),
      fractalType = SanitizeEnum<FractalTypeEnum>(fractal),
      seed = SanitizeIntField(noiseSeed),
      fractalOctaves = SanitizeIntField(fractalOctaves),
      fractalLacunarity = SanitizeFloatField(fractalLacunarity),
      frequency = SanitizeFloatField(noiseFrequency),
      cellularJitter = SanitizeFloatField(cellularJitter),
      fractalWeightedStrength = SanitizeFloatField(fractalStrenght),
      fractalPingPongStrength = SanitizeFloatField(fractalPingPong),
      fractalGain = SanitizeFloatField(fractalGain),
      domainWarpAmplitude = SanitizeFloatField(domainAmplitude),
      domainWarpFractalGain = SanitizeFloatField(domainGain),
      domainWarpFractalLacunarity = SanitizeFloatField(domainLacunarity),
      domainWarpFractalOctaves = SanitizeIntField(domainOctaves),
      domainWarpFrequency = SanitizeFloatField(domainFrequency)
    };
  }


  protected static void FillOptionWithEnum<T>(
    OptionButton enumOptionButton, T defaultValue) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
    enumOptionButton.Select(Convert.ToInt32(defaultValue));
  }

  protected Vector3I GetMapSize()
    => new(SanitizeIntField(mapWidth), SanitizeIntField(mapHeight), 0);


  protected static T SanitizeEnum<T>(OptionButton enumOptionButton) where T : Enum
  {
    T result = InputSanitizer.SanitizeEnum<T>(
      enumOptionButton.GetItemText(enumOptionButton.GetSelectedId()));
    return result;
  }

  protected static float SanitizeFloatField(LineEdit lineEdit)
  {
    float result = InputSanitizer.SanitizeFloat(lineEdit.Text);
    lineEdit.Text = result.ToString();
    return result;
  }

  protected static int SanitizeIntField(LineEdit lineEdit)
  {
    int result = InputSanitizer.SanitizeInt(lineEdit.Text);
    lineEdit.Text = result.ToString();
    return result;
  }
}
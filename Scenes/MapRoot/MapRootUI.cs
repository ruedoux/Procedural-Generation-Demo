using System;
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
    TileContainer = GetNode<Control>("MainUI/C/H/B/P/V/S/V/TilesContainer/Container/Tiles/V/TileContainer");

    mapWidth = GetNode<LineEdit>("MainUI/C/H/B/P/V/S/V/SizeContainer/Container/Size/V/Width/LineEdit");
    mapHeight = GetNode<LineEdit>("MainUI/C/H/B/P/V/S/V/SizeContainer/Container/Size/V/Height/LineEdit");

    optionMenu = GetNode<Control>("MainUI/C/H/B/P/V/S/V/NoiseContainer/Container/Noise/V");
    filterMenu = GetNode<Control>("MainUI/C/H/B/P/V/S/V/FilterContainer/Container/Filter/V");
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

    generateButton = GetNode<Button>("MainUI/C/H/B/P/V/Generate/Button");
    generateImageButton = GetNode<Button>("MainUI/C/H/B/P/V/GenerateImage/Button");
    generateImageDialog = GetNode<FileDialog>("MainUI/C/H/B/P/V/GenerateImage/FileDialog");

    FillOptionWithEnum(noiseType, NoiseTypeEnum.SimplexSmooth);
    FillOptionWithEnum(cellularDistance, CellularDistanceFunctionEnum.Euclidean);
    FillOptionWithEnum(cellularReturn, CellularReturnTypeEnum.Distance);
    FillOptionWithEnum(domainWarp, DomainWarpTypeEnum.Simplex);
    FillOptionWithEnum(domainFractal, DomainWarpFractalTypeEnum.Progressive);
    FillOptionWithEnum(fractal, FractalTypeEnum.Fbm);
    FillOptionWithEnum(filterType, FilterData.FilterType.None);
  }

  private static void FillOptionWithEnum<T>(
    OptionButton enumOptionButton, T defaultValue) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
    enumOptionButton.Select(Convert.ToInt32(defaultValue));
  }
}
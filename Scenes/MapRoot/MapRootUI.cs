using System;
using System.Linq;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRootUI : Node2D
{
  protected LineEdit mapWidth;
  protected LineEdit mapHeight;

  protected LineEdit noiseSeed;
  protected OptionButton noiseType;
  protected OptionButton noiseDistance;
  protected OptionButton noiseReturn;
  protected OptionButton noiseDomainWarp;
  protected OptionButton noiseDomainFractal;
  protected OptionButton noiseFractal;

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

  protected Button generateButton;

  protected void InitializeUI()
  {
    mapWidth = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Size/V/Width/LineEdit");
    mapHeight = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Size/V/Height/LineEdit");

    noiseSeed = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Seed/LineEdit");
    noiseType = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/NoiseType/OptionButton");
    noiseDistance = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/CellularDistance/OptionButton");
    noiseReturn = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/CellularReturn/OptionButton");
    noiseDomainWarp = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/DomainWarp/OptionButton");
    noiseDomainFractal = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/DomainFractal/OptionButton");
    noiseFractal = GetNode<OptionButton>("MainUI/C/H/B/P/S/V/Noise/V/Fractal/OptionButton");

    fractalGain = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalGain/LineEdit");
    fractalOctaves = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalOctaves/LineEdit");
    fractalLacunarity = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalLacunarity/LineEdit");
    fractalStrenght = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalStrenght/LineEdit");
    fractalPingPong = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/FractalPingPong/LineEdit"); ;
    noiseFrequency = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/Frequency/LineEdit");
    cellularJitter = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/CellularJitter/LineEdit");
    domainAmplitude = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/DomainAmplitude/LineEdit");
    domainGain = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/DomainGain/LineEdit");
    domainLacunarity = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/DomainLacunarity/LineEdit");
    domainOctaves = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/DomainOctaves/LineEdit");
    domainFrequency = GetNode<LineEdit>("MainUI/C/H/B/P/S/V/Noise/V/DomainFrequency/LineEdit");

    generateButton = GetNode<Button>("MainUI/C/H/B/P/S/V/Generate/Button");

    FillOptionWithEnum(noiseType, NoiseTypeEnum.SimplexSmooth);
    FillOptionWithEnum(noiseDistance, CellularDistanceFunctionEnum.Euclidean);
    FillOptionWithEnum(noiseReturn, CellularReturnTypeEnum.Distance);
    FillOptionWithEnum(noiseDomainWarp, DomainWarpTypeEnum.Simplex);
    FillOptionWithEnum(noiseDomainFractal, DomainWarpFractalTypeEnum.Progressive);
    FillOptionWithEnum(noiseFractal, FractalTypeEnum.Fbm);
  }

  private static void FillOptionWithEnum<T>(
    OptionButton enumOptionButton, T defaultValue) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
    enumOptionButton.Select(Convert.ToInt32(defaultValue));
  }
}
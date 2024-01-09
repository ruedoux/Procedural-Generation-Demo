using System;
using System.Collections.Generic;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRootUI : Node2D
{
  protected Control tileContainer;

  protected LineEdit mapWidthLineEdit;
  protected LineEdit mapHeightLineEdit;

  protected Control optionMenu;
  protected Control filterMenu;
  protected LineEdit seedLineEdit;
  protected OptionButton noiseTypeOptionButton;
  protected OptionButton cellularDistanceFunctionOptionButton;
  protected OptionButton cellularReturnTypeOptionButton;
  protected OptionButton domainWarpTypeOptionButton;
  protected OptionButton domainWarpFractalTypeOptionButton;
  protected OptionButton fractalTypeOptionButton;
  protected OptionButton filterType;

  protected LineEdit fractalGainLineEdit;
  protected LineEdit fractalOctavesLineEdit;
  protected LineEdit fractalLacunarityLineEdit;
  protected LineEdit frequencyLineEdit;
  protected LineEdit cellularJitterLineEdit;
  protected LineEdit fractalWeightedStrengthLineEdit;
  protected LineEdit fractalPingPongStrengthLineEdit;
  protected LineEdit domainWarpAmplitudeLineEdit;
  protected LineEdit domainWarpFractalGainLineEdit;
  protected LineEdit domainWarpFractalLacunarityLineEdit;
  protected LineEdit domainWarpFractalOctavesLineEdit;
  protected LineEdit domainWarpFrequencyLineEdit;
  protected LineEdit filterStrenght;
  protected LineEdit filterBoost;

  protected Button generateButton;
  protected Button saveImageButton;
  protected Button saveSettingsButton;
  protected Button loadSettingsButton;

  protected FileDialog saveImageDialog;
  protected FileDialog saveSettingsDialog;
  protected FileDialog loadSettingsDialog;

  protected void InitializeUI()
  {
    tileContainer = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/S/V/TilesContainer/Container/Tiles/V/TileContainer");

    mapWidthLineEdit = GetNode<LineEdit>("MainUI/C/H/OptionPanel/B/P/S/V/SizeContainer/Container/Size/V/Width/LineEdit");
    mapHeightLineEdit = GetNode<LineEdit>("MainUI/C/H/OptionPanel/B/P/S/V/SizeContainer/Container/Size/V/Height/LineEdit");

    optionMenu = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/S/V/NoiseContainer/Container/Noise/V");
    filterMenu = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/S/V/FilterContainer/Container/Filter/V");
    seedLineEdit = optionMenu.GetNode<LineEdit>("Seed/LineEdit");
    noiseTypeOptionButton = optionMenu.GetNode<OptionButton>("NoiseType/OptionButton");
    cellularDistanceFunctionOptionButton = optionMenu.GetNode<OptionButton>("CellularDistance/OptionButton");
    cellularReturnTypeOptionButton = optionMenu.GetNode<OptionButton>("CellularReturn/OptionButton");
    domainWarpTypeOptionButton = optionMenu.GetNode<OptionButton>("DomainWarp/OptionButton");
    domainWarpFractalTypeOptionButton = optionMenu.GetNode<OptionButton>("DomainFractal/OptionButton");
    fractalTypeOptionButton = optionMenu.GetNode<OptionButton>("Fractal/OptionButton");
    filterType = filterMenu.GetNode<OptionButton>("Type/OptionButton");

    fractalGainLineEdit = optionMenu.GetNode<LineEdit>("FractalGain/LineEdit");
    fractalOctavesLineEdit = optionMenu.GetNode<LineEdit>("FractalOctaves/LineEdit");
    fractalLacunarityLineEdit = optionMenu.GetNode<LineEdit>("FractalLacunarity/LineEdit");
    fractalWeightedStrengthLineEdit = optionMenu.GetNode<LineEdit>("FractalStrenght/LineEdit");
    fractalPingPongStrengthLineEdit = optionMenu.GetNode<LineEdit>("FractalPingPong/LineEdit"); ;
    frequencyLineEdit = optionMenu.GetNode<LineEdit>("Frequency/LineEdit");
    cellularJitterLineEdit = optionMenu.GetNode<LineEdit>("CellularJitter/LineEdit");
    domainWarpAmplitudeLineEdit = optionMenu.GetNode<LineEdit>("DomainAmplitude/LineEdit");
    domainWarpFractalGainLineEdit = optionMenu.GetNode<LineEdit>("DomainGain/LineEdit");
    domainWarpFractalLacunarityLineEdit = optionMenu.GetNode<LineEdit>("DomainLacunarity/LineEdit");
    domainWarpFractalOctavesLineEdit = optionMenu.GetNode<LineEdit>("DomainOctaves/LineEdit");
    domainWarpFrequencyLineEdit = optionMenu.GetNode<LineEdit>("DomainFrequency/LineEdit");
    filterStrenght = filterMenu.GetNode<LineEdit>("Strenght/LineEdit");
    filterBoost = filterMenu.GetNode<LineEdit>("Boost/LineEdit");

    generateButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/Generate/Button");
    saveImageButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/SaveImage/Button");
    saveSettingsButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/SaveSettings/Button");
    loadSettingsButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/LoadSettings/Button");

    saveImageDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/P/H/SaveImage/FileDialog");
    saveSettingsDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/P/H/SaveSettings/FileDialog");
    loadSettingsDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/P/H/LoadSettings/FileDialog");

    FillOptionWithEnum(noiseTypeOptionButton, NoiseTypeEnum.SimplexSmooth);
    FillOptionWithEnum(cellularDistanceFunctionOptionButton, CellularDistanceFunctionEnum.Euclidean);
    FillOptionWithEnum(cellularReturnTypeOptionButton, CellularReturnTypeEnum.Distance);
    FillOptionWithEnum(domainWarpTypeOptionButton, DomainWarpTypeEnum.Simplex);
    FillOptionWithEnum(domainWarpFractalTypeOptionButton, DomainWarpFractalTypeEnum.Progressive);
    FillOptionWithEnum(fractalTypeOptionButton, FractalTypeEnum.Fbm);
    FillOptionWithEnum(filterType, FilterData.FilterType.None);
  }

  protected TileNoise[] GetTileNoises()
  {
    List<TileNoise> tileNoises = new();

    var tileContainerTiles = tileContainer.GetChildren();
    for (int i = 0; i < tileContainerTiles.Count; i++)
    {
      var TileUI = (HBoxContainer)tileContainerTiles[i];
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
      noiseType = SanitizeEnum<NoiseTypeEnum>(noiseTypeOptionButton),
      cellularDistanceFunction = SanitizeEnum<CellularDistanceFunctionEnum>(cellularDistanceFunctionOptionButton),
      cellularReturnType = SanitizeEnum<CellularReturnTypeEnum>(cellularReturnTypeOptionButton),
      domainWarpType = SanitizeEnum<DomainWarpTypeEnum>(domainWarpTypeOptionButton),
      domainWarpFractalType = SanitizeEnum<DomainWarpFractalTypeEnum>(domainWarpFractalTypeOptionButton),
      fractalType = SanitizeEnum<FractalTypeEnum>(fractalTypeOptionButton),
      seed = SanitizeIntField(seedLineEdit),
      fractalOctaves = SanitizeIntField(fractalOctavesLineEdit),
      fractalLacunarity = SanitizeFloatField(fractalLacunarityLineEdit),
      frequency = SanitizeFloatField(frequencyLineEdit),
      cellularJitter = SanitizeFloatField(cellularJitterLineEdit),
      fractalWeightedStrength = SanitizeFloatField(fractalWeightedStrengthLineEdit),
      fractalPingPongStrength = SanitizeFloatField(fractalPingPongStrengthLineEdit),
      fractalGain = SanitizeFloatField(fractalGainLineEdit),
      domainWarpAmplitude = SanitizeFloatField(domainWarpAmplitudeLineEdit),
      domainWarpFractalGain = SanitizeFloatField(domainWarpFractalGainLineEdit),
      domainWarpFractalLacunarity = SanitizeFloatField(domainWarpFractalLacunarityLineEdit),
      domainWarpFractalOctaves = SanitizeIntField(domainWarpFractalOctavesLineEdit),
      domainWarpFrequency = SanitizeFloatField(domainWarpFrequencyLineEdit)
    };
  }

  protected void LoadSettings(GenerationSettings generationSettings)
  {
    SetLineEditText(mapWidthLineEdit, generationSettings.mapSize.X);
    SetLineEditText(mapHeightLineEdit, generationSettings.mapSize.Y);
    SetLineEditText(seedLineEdit, generationSettings.seed);
    SetLineEditText(fractalOctavesLineEdit, generationSettings.fractalOctaves);
    SetLineEditText(fractalLacunarityLineEdit, generationSettings.fractalLacunarity);
    SetLineEditText(frequencyLineEdit, generationSettings.frequency);
    SetLineEditText(cellularJitterLineEdit, generationSettings.cellularJitter);
    SetLineEditText(fractalWeightedStrengthLineEdit, generationSettings.fractalWeightedStrength);
    SetLineEditText(fractalPingPongStrengthLineEdit, generationSettings.fractalPingPongStrength);
    SetLineEditText(fractalGainLineEdit, generationSettings.fractalGain);
    SetLineEditText(domainWarpAmplitudeLineEdit, generationSettings.domainWarpAmplitude);
    SetLineEditText(domainWarpFractalGainLineEdit, generationSettings.domainWarpFractalGain);
    SetLineEditText(domainWarpFractalLacunarityLineEdit, generationSettings.domainWarpFractalLacunarity);
    SetLineEditText(domainWarpFractalOctavesLineEdit, generationSettings.domainWarpFractalOctaves);
    SetLineEditText(domainWarpFrequencyLineEdit, generationSettings.domainWarpFrequency);

    SetOptionButtonEnum(noiseTypeOptionButton, generationSettings.noiseType);
    SetOptionButtonEnum(cellularDistanceFunctionOptionButton, generationSettings.cellularDistanceFunction);
    SetOptionButtonEnum(cellularReturnTypeOptionButton, generationSettings.cellularReturnType);
    SetOptionButtonEnum(domainWarpTypeOptionButton, generationSettings.domainWarpType);
    SetOptionButtonEnum(domainWarpFractalTypeOptionButton, generationSettings.domainWarpFractalType);
    SetOptionButtonEnum(fractalTypeOptionButton, generationSettings.fractalType);
  }


  protected static void FillOptionWithEnum<T>(
    OptionButton enumOptionButton, T defaultValue) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
    enumOptionButton.Select(Convert.ToInt32(defaultValue));
  }

  protected Vector3I GetMapSize()
    => new(SanitizeIntField(mapWidthLineEdit), SanitizeIntField(mapHeightLineEdit), 0);


  protected static T SanitizeEnum<T>(OptionButton enumOptionButton) where T : Enum
  {
    T result = InputSanitizer.SanitizeEnum<T>(
      enumOptionButton.GetItemText(enumOptionButton.GetSelectedId()));
    return result;
  }

  protected static float SanitizeFloatField(LineEdit lineEdit)
  {
    float result = InputSanitizer.SanitizeFloat(lineEdit.Text);
    SetLineEditText(lineEdit, result);
    return result;
  }

  protected static int SanitizeIntField(LineEdit lineEdit)
  {
    int result = InputSanitizer.SanitizeInt(lineEdit.Text);
    SetLineEditText(lineEdit, result);
    return result;
  }

  private static void SetLineEditText<T>(LineEdit lineEdit, T value)
    => lineEdit.Text = value.ToString();

  private static void SetOptionButtonEnum<T>(
    OptionButton enumOptionButton, T value) where T : Enum
      => enumOptionButton.Select(Convert.ToInt32(value));

}
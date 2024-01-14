using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Godot;

using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public partial class MapRootUI : Node2D
{
  protected PackedScene tileScene = GD.Load<PackedScene>("res://Scenes/MapRoot/TileUI/TileUI.tscn");

  protected Control tileContainer;

  private LineEdit mapWidthLineEdit;
  private LineEdit mapHeightLineEdit;


  private LineEdit seedLineEdit;
  private OptionButton noiseTypeOptionButton;
  private OptionButton cellularDistanceFunctionOptionButton;
  private OptionButton cellularReturnTypeOptionButton;
  private OptionButton domainWarpTypeOptionButton;
  private OptionButton domainWarpFractalTypeOptionButton;
  private OptionButton fractalTypeOptionButton;
  private OptionButton filterTypeOptionButton;

  private LineEdit fractalGainLineEdit;
  private LineEdit fractalOctavesLineEdit;
  private LineEdit fractalLacunarityLineEdit;
  private LineEdit frequencyLineEdit;
  private LineEdit cellularJitterLineEdit;
  private LineEdit fractalWeightedStrengthLineEdit;
  private LineEdit fractalPingPongStrengthLineEdit;
  private LineEdit domainWarpAmplitudeLineEdit;
  private LineEdit domainWarpFractalGainLineEdit;
  private LineEdit domainWarpFractalLacunarityLineEdit;
  private LineEdit domainWarpFractalOctavesLineEdit;
  private LineEdit domainWarpFrequencyLineEdit;
  private LineEdit filterStrenghtLineEdit;
  private LineEdit filterBoostLineEdit;

  protected Button generateButton;
  protected Button saveImageButton;
  protected Button saveSettingsButton;
  protected Button loadSettingsButton;
  protected Button addTileButton;
  protected Button sortTilesButton;

  protected FileDialog saveImageDialog;
  protected FileDialog saveSettingsDialog;
  protected FileDialog loadSettingsDialog;

  protected void InitializeUI()
  {
    tileContainer = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/TilesContainer/Container/Tiles/V/TileContainer");
    mapWidthLineEdit = GetNode<LineEdit>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/SizeContainer/Container/Size/V/Width/LineEdit");
    mapHeightLineEdit = GetNode<LineEdit>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/SizeContainer/Container/Size/V/Height/LineEdit");

    Control optionMenu = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/NoiseContainer/Container/Noise/V");

    Control filterMenu = GetNode<Control>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/FilterContainer/Container/Filter/V");
    filterTypeOptionButton = filterMenu.GetNode<OptionButton>("Type/OptionButton");
    filterStrenghtLineEdit = filterMenu.GetNode<LineEdit>("Strenght/LineEdit");
    filterBoostLineEdit = filterMenu.GetNode<LineEdit>("Boost/LineEdit");

    Control optionGeneral = optionMenu.GetNode<Control>("General");
    seedLineEdit = optionGeneral.GetNode<LineEdit>("Seed/LineEdit");
    noiseTypeOptionButton = optionGeneral.GetNode<OptionButton>("NoiseType/OptionButton");
    frequencyLineEdit = optionGeneral.GetNode<LineEdit>("Frequency/LineEdit");

    Control optionFractal = optionMenu.GetNode<Control>("Fractal");
    fractalGainLineEdit = optionFractal.GetNode<LineEdit>("Gain/LineEdit");
    fractalOctavesLineEdit = optionFractal.GetNode<LineEdit>("Octaves/LineEdit");
    fractalLacunarityLineEdit = optionFractal.GetNode<LineEdit>("Lacunarity/LineEdit");
    fractalWeightedStrengthLineEdit = optionFractal.GetNode<LineEdit>("Strenght/LineEdit");
    fractalPingPongStrengthLineEdit = optionFractal.GetNode<LineEdit>("PingPong/LineEdit"); ;
    fractalTypeOptionButton = optionFractal.GetNode<OptionButton>("Type/OptionButton");

    Control optionCellular = optionMenu.GetNode<Control>("Cellular");
    cellularDistanceFunctionOptionButton = optionCellular.GetNode<OptionButton>("DistanceFunction/OptionButton");
    cellularReturnTypeOptionButton = optionCellular.GetNode<OptionButton>("ReturnType/OptionButton");
    cellularJitterLineEdit = optionCellular.GetNode<LineEdit>("Jitter/LineEdit");

    Control optionDomainWarp = optionMenu.GetNode<Control>("DomainWarp");
    domainWarpTypeOptionButton = optionDomainWarp.GetNode<OptionButton>("Type/OptionButton");
    domainWarpFrequencyLineEdit = optionDomainWarp.GetNode<LineEdit>("Frequency/LineEdit");
    domainWarpAmplitudeLineEdit = optionDomainWarp.GetNode<LineEdit>("Amplitude/LineEdit");

    Control optionDomainWarpFractal = optionMenu.GetNode<Control>("DomainWarpFractal");
    domainWarpFractalGainLineEdit = optionDomainWarpFractal.GetNode<LineEdit>("Gain/LineEdit");
    domainWarpFractalLacunarityLineEdit = optionDomainWarpFractal.GetNode<LineEdit>("Lacunarity/LineEdit");
    domainWarpFractalOctavesLineEdit = optionDomainWarpFractal.GetNode<LineEdit>("Octaves/LineEdit");
    domainWarpFractalTypeOptionButton = optionDomainWarpFractal.GetNode<OptionButton>("Type/OptionButton");

    generateButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/Generate/Button");
    saveImageButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/SaveImage/Button");
    saveSettingsButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/SaveSettings/Button");
    loadSettingsButton = GetNode<Button>("MainUI/C/H/ButtonPanel/P/H/LoadSettings/Button");
    addTileButton = GetNode<Button>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/TilesContainer/Container/Tiles/V/AddTile");
    sortTilesButton = GetNode<Button>("MainUI/C/H/OptionPanel/B/P/SettingsUI/V/TilesContainer/Container/Tiles/V/SortTiles");

    saveImageDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/P/H/SaveImage/FileDialog");
    saveSettingsDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/P/H/SaveSettings/FileDialog");
    loadSettingsDialog = GetNode<FileDialog>("MainUI/C/H/ButtonPanel/P/H/LoadSettings/FileDialog");

    FillOptionWithEnum(noiseTypeOptionButton, NoiseTypeEnum.SimplexSmooth);
    FillOptionWithEnum(cellularDistanceFunctionOptionButton, CellularDistanceFunctionEnum.Euclidean);
    FillOptionWithEnum(cellularReturnTypeOptionButton, CellularReturnTypeEnum.Distance);
    FillOptionWithEnum(domainWarpTypeOptionButton, DomainWarpTypeEnum.Simplex);
    FillOptionWithEnum(domainWarpFractalTypeOptionButton, DomainWarpFractalTypeEnum.Progressive);
    FillOptionWithEnum(fractalTypeOptionButton, FractalTypeEnum.Fbm);
    FillOptionWithEnum(filterTypeOptionButton, FilterData.FilterType.None);
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
      filterType = SanitizeEnum<FilterData.FilterType>(filterTypeOptionButton),
      filterStrength = SanitizeFloatField(filterStrenghtLineEdit),
      filterBoost = SanitizeFloatField(filterBoostLineEdit),
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
    SetLineEditText(filterStrenghtLineEdit, generationSettings.filterStrength);
    SetLineEditText(filterBoostLineEdit, generationSettings.filterBoost);

    SetOptionButtonEnum(noiseTypeOptionButton, generationSettings.noiseType);
    SetOptionButtonEnum(cellularDistanceFunctionOptionButton, generationSettings.cellularDistanceFunction);
    SetOptionButtonEnum(cellularReturnTypeOptionButton, generationSettings.cellularReturnType);
    SetOptionButtonEnum(domainWarpTypeOptionButton, generationSettings.domainWarpType);
    SetOptionButtonEnum(domainWarpFractalTypeOptionButton, generationSettings.domainWarpFractalType);
    SetOptionButtonEnum(fractalTypeOptionButton, generationSettings.fractalType);
    SetOptionButtonEnum(filterTypeOptionButton, generationSettings.filterType);

    foreach (Node tile in tileContainer.GetChildren())
      tileContainer.RemoveChild(tile);
    foreach (TileNoise tileNoise in generationSettings.tileNoises)
      AddTileInstance(tileNoise.noiseMarker, tileNoise.tile.color);
  }

  protected Vector3I GetMapSize()
    => new(SanitizeIntField(mapWidthLineEdit), SanitizeIntField(mapHeightLineEdit), 0);

  private void AddTileInstance(float value, Color color)
  {
    var tileInstance = tileScene.Instantiate();
    tileInstance.GetNode<LineEdit>("Value/LineEdit").Text = value.ToString();
    tileInstance.GetNode<ColorPickerButton>("PickColor").Color = color;
    tileContainer.AddChild(tileInstance);
  }

  public void SortTileContainer()
  {
    List<Node> tiles = new(tileContainer.GetChildren());
    foreach (Node tile in tiles)
      tileContainer.RemoveChild(tile);

    tiles = tiles.OrderBy(
      obj => InputSanitizer.SanitizeFloat(
        obj.GetNode<LineEdit>("Value/LineEdit").Text)).ToList();

    foreach (Node tile in tiles)
      tileContainer.AddChild(tile);
  }

  private static void FillOptionWithEnum<T>(
      OptionButton enumOptionButton, T defaultValue) where T : Enum
  {
    foreach (var noise in Enum.GetValues(typeof(T)))
      enumOptionButton.AddItem(noise.ToString());
    enumOptionButton.Select(Convert.ToInt32(defaultValue));
  }

  private static T SanitizeEnum<T>(OptionButton enumOptionButton) where T : Enum
  {
    T result = InputSanitizer.SanitizeEnum<T>(
      enumOptionButton.GetItemText(enumOptionButton.GetSelectedId()));
    return result;
  }

  private static float SanitizeFloatField(LineEdit lineEdit)
  {
    float result = InputSanitizer.SanitizeFloat(lineEdit.Text);
    SetLineEditText(lineEdit, result);
    return result;
  }

  private static int SanitizeIntField(LineEdit lineEdit)
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
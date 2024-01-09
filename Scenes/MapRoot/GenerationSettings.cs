
using System.IO;
using Godot;
using Newtonsoft.Json;
using static Godot.FastNoiseLite;

namespace ProceduralGeneration;

public class GenerationSettings : JsonSerializable
{
  [JsonProperty] public Vector3I mapSize;
  [JsonProperty] public TileNoise[] tileNoises;

  [JsonProperty] public NoiseTypeEnum noiseType;
  [JsonProperty] public CellularDistanceFunctionEnum cellularDistanceFunction;
  [JsonProperty] public CellularReturnTypeEnum cellularReturnType;
  [JsonProperty] public DomainWarpTypeEnum domainWarpType;
  [JsonProperty] public DomainWarpFractalTypeEnum domainWarpFractalType;
  [JsonProperty] public FractalTypeEnum fractalType;

  [JsonProperty] public int seed;
  [JsonProperty] public int fractalOctaves;
  [JsonProperty] public float fractalLacunarity;
  [JsonProperty] public float frequency;

  [JsonProperty] public float cellularJitter;

  [JsonProperty] public float fractalWeightedStrength;
  [JsonProperty] public float fractalPingPongStrength;
  [JsonProperty] public float fractalGain;

  [JsonProperty] public float domainWarpAmplitude;
  [JsonProperty] public float domainWarpFractalGain;
  [JsonProperty] public float domainWarpFractalLacunarity;
  [JsonProperty] public int domainWarpFractalOctaves;
  [JsonProperty] public float domainWarpFrequency;

  public FastNoiseLite CreateFastNoiseLite()
  {
    return new()
    {
      NoiseType = noiseType,
      CellularDistanceFunction = cellularDistanceFunction,
      CellularReturnType = cellularReturnType,
      DomainWarpType = domainWarpType,
      DomainWarpFractalType = domainWarpFractalType,
      FractalType = fractalType,
      Seed = seed,
      FractalOctaves = fractalOctaves,
      FractalLacunarity = fractalLacunarity,
      Frequency = frequency,
      CellularJitter = cellularJitter,
      FractalWeightedStrength = fractalWeightedStrength,
      FractalPingPongStrength = fractalPingPongStrength,
      FractalGain = fractalGain,
      DomainWarpAmplitude = domainWarpAmplitude,
      DomainWarpFractalGain = domainWarpFractalGain,
      DomainWarpFractalLacunarity = domainWarpFractalLacunarity,
      DomainWarpFractalOctaves = domainWarpFractalOctaves,
      DomainWarpFrequency = domainWarpFrequency,
    };
  }

  public void SaveInPath(string path)
  {
    using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
    file.StoreString(ToJsonString());
  }

  public GenerationSettings LoadFromPath(string path)
  {
    if (!Godot.FileAccess.FileExists(path))
      throw new FileNotFoundException("Unable to find file: " + path);

    using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);

    string content = file.GetAsText();
    return FromJsonString<GenerationSettings>(content);
  }
}
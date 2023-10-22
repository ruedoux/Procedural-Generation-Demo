using System;
using System.Globalization;

namespace ProceduralGeneration;

public static class InputSanitizer
{
  public static float SanitizeFloat(string input, float defaultValue = 0.0f)
  {
    input = input.Replace(',', '.');

    if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
      return result;

    return defaultValue;
  }

  public static int SanitizeInt(string input, int defaultValue = 0)
  {
    if (int.TryParse(input, out int result))
      return result;

    return defaultValue;
  }

  public static T SanitizeEnum<T>(string enumText) where T : Enum
  {
    if (Enum.TryParse(typeof(T), enumText, out object enumValue))
      return (T)enumValue;

    return Enum.GetValues(typeof(T)).Length > 0 ? (T)Enum.GetValues(typeof(T)).GetValue(0) : default(T);
  }
}
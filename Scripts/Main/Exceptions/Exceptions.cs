using System;

namespace ProceduralGeneration;

public static partial class Exceptions
{
  public static void ThrowIfNotEqual(object isNow, object shouldBe)
  {
    if (!Equals(isNow, shouldBe))
    {
      throw new WrongValueException(
        $"Value is not equal, is: '{isNow}', but should be: '{shouldBe}'.");
    }
  }

  public static void ThrowIfEqual(object isNow, object shouldNotBe)
  {
    if (Equals(isNow, shouldNotBe))
    {
      throw new WrongValueException(
        $"Value is equal to: '{isNow}'.");
    }
  }

  public static void ThrowIfGreaterThan<T>(T value, T maxValue)
        where T : IComparable<T>
  {
    if (value.CompareTo(maxValue) > 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is greater than '{maxValue}'.");
    }
  }

  public static void ThrowIfEqualOrGreaterThan<T>(T value, T maxValue)
        where T : IComparable<T>
  {
    if (value.CompareTo(maxValue) <= 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is greater than '{maxValue}'.");
    }
  }

  public static void ThrowIfLessThan<T>(T value, T minValue)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) < 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is less than '{minValue}'.");
    }
  }

  public static void ThrowIfEqualOrLessThan<T>(T value, T minValue)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) <= 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is less than '{minValue}'.");
    }
  }

  public static void ThrowIfInRange<T>(T value, T minValue, T maxValue)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is in range: '{minValue}' - '{maxValue}'.");
    }
  }

  public static void ThrowIfNotInRange<T>(T value, T minValue, T maxValue)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is not in range: '{minValue}' - '{maxValue}'.");
    }
  }
}
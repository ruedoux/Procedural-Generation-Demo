using System;

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

  public static void ThrowIfLessThan<T>(T value, T minValue)
      where T : IComparable<T>
  {
    if (value.CompareTo(minValue) < 0)
    {
      throw new WrongValueException(
          $"Value '{value}' is less than '{minValue}'.");
    }
  }
}
using System;

namespace SGT;

public static class Assertions
{
  public static void AssertEqual(object isNow, object shouldBe)
  {
    Exceptions.ThrowIfNotEqual(isNow, shouldBe);
  }

  public static void AssertNotEqual(object isNow, object shouldNotBe)
  {
    Exceptions.ThrowIfEqual(isNow, shouldNotBe);
  }
}
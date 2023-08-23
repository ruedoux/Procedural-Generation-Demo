using System;


public static class ITAssertions
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
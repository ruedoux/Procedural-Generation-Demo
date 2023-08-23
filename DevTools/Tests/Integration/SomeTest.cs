using Godot;
using System;

[ITClass]
public class SomeTest
{
  [ITMethod]
  public void Test()
  {
    ITAssertions.AssertEqual(1, 2);
  }
}

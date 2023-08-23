using Godot;
using System;

public class SomeTest : ITClass
{
  [ITMethod]
  public void Test()
  {
    Exceptions.ThrowIfEqual(1, 1);
    //ITAssertions.AssertEqual(1, 2);
  }
}

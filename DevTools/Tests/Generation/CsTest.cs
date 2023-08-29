using Godot;
using Godot.Collections;

public partial class CsTest : RefCounted
{
  public Array<int> arr;

  public CsTest(Godot.Collections.Array<int> arr)
  {
    this.arr = arr;
  }
}
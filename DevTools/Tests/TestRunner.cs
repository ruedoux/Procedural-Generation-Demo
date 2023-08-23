using System;
using System.IO;
using System.Reflection;
using Godot;

public partial class TestRunner : Control
{
  public override void _Ready()
  {

    try
    {
      Exceptions.ThrowIfEqual(1, 1);
    }
    catch (Exception ex)
    {
      Logger.Log(ex.Message);
    }

    RunAllTests();
  }

  private void RunAllTests()
  {
    new SomeTest();
  }
}
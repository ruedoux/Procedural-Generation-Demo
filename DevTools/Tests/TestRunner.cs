using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Godot;

public partial class TestRunner : Control
{
  public override void _Ready()
  {
    RunAllTests();
  }

  private void RunAllTests()
  {
    var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(ITClass)));

    foreach (var type in types)
    {
      var instance = Activator.CreateInstance(type);
      ITRunner.RunTests(instance);
    }
  }
}
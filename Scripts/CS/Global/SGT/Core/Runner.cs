// Since there is currently no working and usable unit test library for unit tests
// This is made to handle doing integration tests with basic testing and feedback
// Production name is "Simple Godot Tests" (SGT - since we need a cool name for this)
using System;
using System.Reflection;
using System.Linq;

namespace SGT;

public class Runner
{
  // Searches for all classes with SGTClass attribute and runs them
  public static void RunAllTests()
  {
    var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(TestClass)));

    foreach (var type in types)
    {
      var instance = Activator.CreateInstance(type);
      RunTests(instance);
    }
  }

  public static void RunTests(object objectToRun)
  {
    var methods = objectToRun.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    Logger.Log($">---------------- STARTING: {objectToRun.GetType().Name}  ----------------<");

    foreach (var method in methods)
    {
      if (Attribute.IsDefined(method, typeof(TestMethod)))
      {
        try
        {
          method.Invoke(objectToRun, null);
          LogSuccess($"{method.Name}");
        }
        catch (Exception ex)
        {
          LogFail($"{method.Name} \n{ex.InnerException}");
        }
      }
    }

    Logger.Log($">---------------- FINISHED: {objectToRun.GetType().Name}  ----------------<");
    Logger.Log("");
  }

  private static void LogSuccess(String msg)
  {
    Logger.Log("[TEST OK] ", msg);
  }

  private static void LogFail(String msg)
  {
    Logger.Log("[TEST FAIL] ", msg);
  }
}

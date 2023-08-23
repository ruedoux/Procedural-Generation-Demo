// Since there is currently no working and usable unit test library for unit tests
// This is made to handle doing integration tests with basic testing and feedback
using System;
using System.Reflection;

public class ITRunner
{
  public static void RunTests(object objectToRun)
  {
    var methods = objectToRun.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    Logger.Log($">---------------- STARTING: {objectToRun.GetType().Name}  ----------------<");

    foreach (var method in methods)
    {
      if (Attribute.IsDefined(method, typeof(ITMethod)))
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

[AttributeUsage(AttributeTargets.Class)]
public class ITClass : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class ITMethod : Attribute { }
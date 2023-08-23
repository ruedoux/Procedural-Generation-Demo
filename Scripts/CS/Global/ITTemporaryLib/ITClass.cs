// Since there is currently no working and usable unit test library for unit tests
// This is made to handle doing integration tests with basic testing and feedback
using System;

[AttributeUsage(AttributeTargets.Class)]
public class ITClass : Attribute
{
  public ITClass() { RunTests(); }

  public void RunTests()
  {
    var methods = GetType().GetMethods();

    Logger.Log($">---------------- STARTING: {GetType().Name}  ----------------<");

    foreach (var method in methods)
    {
      if (IsDefined(method, typeof(ITMethod)))
      {
        try
        {
          method.Invoke(this, null);
          LogSuccess($"{method.Name}");
        }
        catch (Exception ex)
        {
          LogFail($"{method.Name} \n{ex.InnerException}");
        }
      }
    }

    Logger.Log($">---------------- FINISHED: {GetType().Name}  ----------------<");
    Logger.Log("");
  }

  private void LogSuccess(String msg)
  {
    Logger.Log("[TEST OK] ", msg);
  }

  private void LogFail(String msg)
  {
    Logger.Log("[TEST FAIL] ", msg);
  }
}

[AttributeUsage(AttributeTargets.Method)]
public class ITMethod : Attribute { }
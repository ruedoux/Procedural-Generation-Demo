using Godot;
using System;

public partial class Logger
{
  private const String ERROR_MARKER = "[ERROR] ";
  private const String WARNING_MARKER = "[WARN] ";
  private const String INFO_MARKER = "[INFO] ";

  public static bool supressError = false;
  public static bool supressWarning = false;

  public static void Log(params object[] msgs)
  {
    LogToConsole("", msgs);
  }

  public static void LogError(params object[] msgs)
  {
    LogToConsole(ERROR_MARKER, msgs);
    if (!supressError)
    {
      GD.PushError(msgs);
    }
  }

  public static void LogInfo(params object[] msgs)
  {
    LogToConsole(INFO_MARKER, msgs);
  }

  public static void LogWarning(params object[] msgs)
  {
    LogToConsole(WARNING_MARKER, msgs);
    if (!supressWarning)
    {
      GD.PushWarning(msgs);
    }
  }

  private static void LogToConsole(String marker, params object[] msgs)
  {
    foreach (object msg in msgs)
    {
      marker += msg.ToString();
    }

    GD.Print(marker);
  }
}

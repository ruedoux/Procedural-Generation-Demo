using Godot;
using System;

public static partial class Logger
{
  private const String ERROR_MARKER = "[ERROR] ";
  private const String WARNING_MARKER = "[WARN] ";
  private const String INFO_MARKER = "[INFO] ";

  public static bool supressError = false;
  public static bool supressWarning = false;

  public static void Log(params String[] msgs)
  {
    LogToConsole("", msgs);
  }

  public static void LogError(params String[] msgs)
  {
    LogToConsole(ERROR_MARKER, msgs);
    if (!supressError)
    {
      GD.PushError(msgs);
    }
  }

  public static void LogInfo(params String[] msgs)
  {
    LogToConsole(INFO_MARKER, msgs);
  }

  public static void LogWarning(params String[] msgs)
  {
    LogToConsole(WARNING_MARKER, msgs);
    if (!supressWarning)
    {
      GD.PushWarning(msgs);
    }
  }

  private static void LogToConsole(String marker, params String[] msgs)
  {
    foreach (String msg in msgs)
    {
      marker += msg;
    }

    GD.Print(marker);
  }
}

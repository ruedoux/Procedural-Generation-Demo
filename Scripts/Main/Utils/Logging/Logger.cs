using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;

namespace ProceduralGeneration;

public static class Logger
{
  public static ObserverManager<string> warningObservers = new();
  public static ObserverManager<string> errorObservers = new(GD.PushError);
  public static ObserverManager<string> allObservers = new(GD.PrintRich);

  public static bool supressError = false;
  public static bool supressWarning = false;
  public static bool logThread = false;

  public static void Log(params object[] msgs)
    => ForwardMessage(Message.GetInfo(GetSourceClassName(), msgs));

  public static void LogError(params object[] msgs)
    => ForwardMessage(Message.GetError(GetSourceClassName(), msgs));

  public static void LogWarning(params object[] msgs)
    => ForwardMessage(Message.GetWarning(GetSourceClassName(), msgs));

  public static void LogException(Exception ex)
  {
    List<string> parsedExceptions = new() { ex.InnerException.Message };
    string[] lines = ex.InnerException.ToString()
      .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (string line in lines) parsedExceptions.Add(line);
    LogError(string.Join('\n', parsedExceptions.ToArray()));
  }

  private static void ForwardMessage(Message message)
  {
    allObservers.NotifyObservers(message.GetAsString(logThread, true));
    if (!supressWarning && message.type == Message.TYPE.WARN)
      warningObservers.NotifyObservers(message.GetText());
    if (!supressError && message.type == Message.TYPE.ERROR)
      errorObservers.NotifyObservers(message.GetText());
  }

  private static string GetSourceClassName()
    => new StackTrace().GetFrame(2).GetMethod().DeclaringType.Name;
}
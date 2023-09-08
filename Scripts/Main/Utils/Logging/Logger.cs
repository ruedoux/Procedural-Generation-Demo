using Godot;
using System;
using System.Collections.Generic;

public static class Logger
{
  private const String ERROR_MARKER = "[ERROR] ";
  private const String WARNING_MARKER = "[WARNING] ";
  private const String INFO_MARKER = "[INFO] ";

  public static ObserverManager<string> normalObservers = new();
  public static ObserverManager<string> warningObservers = new();
  public static ObserverManager<string> errorObservers = new();
  public static ObserverManager<string> allObservers = new();
  public static ObserverManager<Message> messageObservers = new();

  public static bool supressError = false;
  public static bool supressWarning = false;
  public static uint indentationTabs = 0;


  public static void Log(params object[] msgs)
  {
    LogMessage(Message.GetNormal("", msgs));
  }

  public static void LogError(params object[] msgs)
  {
    LogMessage(Message.GetError(ERROR_MARKER, msgs));
  }

  public static void LogInfo(params object[] msgs)
  {
    LogMessage(Message.GetInfo(INFO_MARKER, msgs));
  }

  public static void LogWarning(params object[] msgs)
  {
    LogMessage(Message.GetWarning(WARNING_MARKER, msgs));
  }

  public static void LogMessage(Message message)
  {
    message.SetIndentation(indentationTabs);

    allObservers.NotifyObservers(message.GetWhole());
    messageObservers.NotifyObservers(message);

    if (message.GetMessageType() == Message.TYPE.NORMAL)
    {
      normalObservers.NotifyObservers(message.GetWhole());
    }
    else if ((message.GetMessageType() == Message.TYPE.WARNING) && !supressWarning)
    {
      warningObservers.NotifyObservers(message.GetWhole());
    }
    else if ((message.GetMessageType() == Message.TYPE.ERROR) && !supressError)
    {
      errorObservers.NotifyObservers(message.GetWhole());
    }
  }

  public static void LogException(Exception ex)
  {
    List<string> parsedExceptions = new()
    {
      ex.InnerException.Message
    };

    string[] lines = ex.InnerException.ToString()
      .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (string line in lines)
    {
      parsedExceptions.Add(line);
    }

    Log(Message.GetError("", string.Join('\n', parsedExceptions.ToArray())));
  }

  public static void StartBlock(Message message)
  {
    LogMessage(message);
    indentationTabs += 1;
  }

  public static void EndBlock(Message message)
  {
    indentationTabs -= 1;
    LogMessage(message);
  }
}
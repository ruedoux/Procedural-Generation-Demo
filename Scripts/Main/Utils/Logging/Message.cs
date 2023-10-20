using System;
using System.Collections.Generic;
using System.Threading;

namespace ProceduralGeneration;

public class Message
{
  public enum TYPE { INFO, WARN, ERROR }
  private static readonly Dictionary<TYPE, string> TypeColorDict = new()
  { {TYPE.INFO, "Deepskyblue"}, {TYPE.WARN, "Orange"}, {TYPE.ERROR, "Red"}};

  public readonly TYPE type;
  public readonly string typeStr;
  public readonly string time;
  public readonly string className;
  public readonly string threadId;
  public readonly string text;


  private Message() { }

  private Message(
    TYPE type, string className, params object[] msgs)
  {
    time = DateTime.Now.ToString("HH:mm:ss.fff");
    threadId = Thread.CurrentThread.ManagedThreadId.ToString("X").PadLeft(8)[..8];
    typeStr = type.ToString().PadLeft(5)[..5];

    this.type = type;
    this.className = className.PadLeft(16)[..16];
    text = string.Join("", msgs);
  }

  public string GetAsString(bool withThreadId, bool withBBCode)
    => $"{GetContext(withBBCode, withThreadId)} : {text}";

  public string GetText() => text;

  private string GetContext(bool withBBCode, bool withThreadId)
  {
    string finalTypeStr = typeStr;
    if (withBBCode)
      finalTypeStr = AddColorToString(finalTypeStr, TypeColorDict[type]);

    string context = $"{time} {finalTypeStr} [{className}]";
    if (withThreadId)
      context += $" [{threadId}]";

    return context;
  }

  private static string AddColorToString(string msg, string color)
    => $"[color={color}]{msg}[/color]";

  public static Message GetInfo(string className, params object[] msgs)
  => new(TYPE.INFO, className, msgs);
  public static Message GetWarning(string className, params object[] msgs)
    => new(TYPE.WARN, className, msgs);
  public static Message GetError(string className, params object[] msgs)
    => new(TYPE.ERROR, className, msgs);
}
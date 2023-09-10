using System;

public partial class WrongValueException : Exception
{
  public WrongValueException(string message)
       : base(message)
  {
  }
}

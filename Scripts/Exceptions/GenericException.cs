using System;

public partial class GenericException : Exception
{
  public GenericException(string message)
       : base(message)
  {
  }
}

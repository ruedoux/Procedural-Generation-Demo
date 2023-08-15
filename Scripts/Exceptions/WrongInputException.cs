public partial class WrongInputException : GenericException
{
  public WrongInputException(string parameterName, object inputValue)
       : base($"Invalid input value '{inputValue}' for parameter '{parameterName}'.")
  {
  }
}

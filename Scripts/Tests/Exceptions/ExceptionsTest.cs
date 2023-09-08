namespace IntegrationTests;
using SGT;

public class ExceptionsTest : SimpleTestClass
{
  [SimpleTestMethod]
  public void ThrowIfNotEqual_shouldThrowException_whenNotEqual()
  {
    // Given
    int equal = 1;
    int notEqual = 2;

    // When
    Exceptions.ThrowIfNotEqual(equal, equal);

    // Then
    Assertions.AssertThrows<WrongValueException>(() =>
      Exceptions.ThrowIfNotEqual(equal, notEqual));
  }

  [SimpleTestMethod]
  public void ThrowIfEqual_shouldThrowException_whenEqual()
  {
    // Given
    int equal = 1;
    int notEqual = 2;

    // When
    Exceptions.ThrowIfEqual(equal, notEqual);

    // Then
    Assertions.AssertThrows<WrongValueException>(() =>
      Exceptions.ThrowIfEqual(equal, equal));
  }

  [SimpleTestMethod]
  public void ThrowIfGreaterThan_shouldThrowException_whenGreater()
  {
    // Given
    int less = 0;
    int value = 1;
    int greater = 2;

    // When
    Exceptions.ThrowIfGreaterThan(value, less);
    Exceptions.ThrowIfGreaterThan(value, value);

    // Then
    Assertions.AssertThrows<WrongValueException>(() =>
      Exceptions.ThrowIfGreaterThan(value, greater));

  }

  [SimpleTestMethod]
  public void ThrowIfEqualOrGreaterThan_shouldThrowException_whenGreaterOrEqual()
  {
    // Given
    int less = 0;
    int value = 1;
    int greater = 2;

    // When
    Exceptions.ThrowIfEqualOrGreaterThan(value, less);

    // Then
    Assertions.AssertThrows<WrongValueException>(() =>
      Exceptions.ThrowIfEqualOrGreaterThan(value, greater));
    Assertions.AssertThrows<WrongValueException>(() =>
          Exceptions.ThrowIfEqualOrGreaterThan(value, value));
  }
}
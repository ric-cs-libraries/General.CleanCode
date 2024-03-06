using Xunit;

using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Domain.ErrorHandling.UnitTests;

public class UnavailableResultValueExceptionTests
{

    [Fact]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage()
    {
        //--- Arrange ---
        var ex = new UnavailableResultValueException();


        //--- Act ---
        var result = ex.Message;

        //--- Assert ---
        var expectedMessage = UnavailableResultValueException.MESSAGE;
        Assert.Equal(expectedMessage, result);
    }
}
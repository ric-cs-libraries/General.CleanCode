using Xunit;

using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Domain.ErrorHandling.UnitTests;

public class UnavailableResultValueExceptionUnitTests
{

    [Fact]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage()
    {
        //--- Arrange ---
        var ex = new UnavailableResultValueException();


        //--- Act ---
        var result = ex.Message;

        //--- Assert ---
        var expected = $"Due to the operation failure, the Result value is not available.";
        Assert.Equal(expected, result);
    }
}
using Xunit;

using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Domain.ErrorHandling.UnitTests;

public class ErrorCodeIsRequiredExceptionUnitTests
{

    [Fact]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage()
    {
        //--- Arrange ---
        var ex = new ErrorCodeIsRequiredException();


        //--- Act ---
        var result = ex.Message;

        //--- Assert ---
        var expected = ErrorCodeIsRequiredException.MESSAGE;
        Assert.Equal(expected, result);
    }
}
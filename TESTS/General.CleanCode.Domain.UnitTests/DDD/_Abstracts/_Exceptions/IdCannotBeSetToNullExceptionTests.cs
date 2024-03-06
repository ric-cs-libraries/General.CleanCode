using Xunit;

using General.CleanCode.Domain.DDD;


namespace General.CleanCode.Domain.DDD.UnitTests;

public class IdCannotBeSetToNullExceptionTests
{

    [Fact]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage()
    {
        //--- Arrange ---
        var ex = new IdCannotBeSetToNullException();


        //--- Act ---
        var result = ex.Message;

        //--- Assert ---
        var expected = IdCannotBeSetToNullException.MESSAGE;
        Assert.Equal(expected, result);
    }
}
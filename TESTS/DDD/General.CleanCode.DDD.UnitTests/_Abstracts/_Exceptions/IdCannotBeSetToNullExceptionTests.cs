using Xunit;

using General.CleanCode.DDD;


namespace General.CleanCode.DDD.UnitTests;

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
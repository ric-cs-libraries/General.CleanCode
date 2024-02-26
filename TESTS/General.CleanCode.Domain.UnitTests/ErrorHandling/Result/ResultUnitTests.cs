using Xunit;

using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Domain.ErrorHandling.UnitTests;

public class ResultUnitTests
{
    private Error GetError()
    {
        return new Error(code: "20", debugMessage: "A");
    }

    [Fact]
    public void ResultOk__ShouldInitializeFieldsCorrectly()
    {
        Result result = Result.Ok();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public void ResultNotOk__ShouldInitializeFieldsCorrectly()
    {
        Error error = GetError();
        Result result = Result.NotOk(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void ResultOfTOk__ShouldInitializeFieldsCorrectly()
    {
        var value = 150;
        Result<int> result = Result<int>.Ok(value);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTOk__ShouldInitializeFieldsCorrectly_2()
    {
        MyClass value = new();
        Result<MyClass> result = Result<MyClass>.Ok(value);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTNotOk__ShouldInitializeFieldsCorrectly()
    {
        Error error = GetError();
        Result<MyClass> result = Result<MyClass>.NotOk(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void ResultOfTNotOk_WhenTryingToAccessTheValueProperty_ShouldThrowAnUnavailableResultValueException()
    {
        Error error = GetError();
        Result<MyClass> result = Result<MyClass>.NotOk(error);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperator__ShouldConvertValueIntoAResultOfTValue()
    {
        int value = 150;
        Result<int> result = value;

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperator__ShouldConvertValueIntoAResultOfTValue_2()
    {
        MyClass value = new();
        Result<MyClass> result = value;

        Assert.Equal(value, result.Value);
    }
}


class MyClass
{

}

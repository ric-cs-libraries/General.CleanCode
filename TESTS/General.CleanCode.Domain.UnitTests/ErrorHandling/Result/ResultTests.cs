﻿using Xunit;

using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Domain.ErrorHandling.UnitTests;

public class ResultTests
{
    private static readonly Error ERROR = new Error(code: "20", debugMessage: "A");

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
        Result result = Result.NotOk(ERROR);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(ERROR, result.Error);
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
        Result<MyClass> result = Result<MyClass>.NotOk(ERROR);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(ERROR, result.Error);
    }

    [Fact]
    public void ResultOfTNotOk_WhenTryingToAccessTheValueProperty_ShouldThrowAnUnavailableResultValueException()
    {
        Result<MyClass> result = Result<MyClass>.NotOk(ERROR);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForTValue__ShouldConvertValueIntoAResultOfTValue()
    {
        int value = 150;
        Result<int> result = value;

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForTValue__ShouldConvertValueIntoAResultOfTValue_2()
    {
        MyClass value = new();
        Result<MyClass> result = value;

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ResultOfTImplicitOperatorForError__ShouldConvertIntoAResultOfTValueWithError()
    {
        Error error = new("errorCode", "debugMessage", "someFieldName");
        Result<MyClass> result = error; //Implicit operator :  Error -> Result<T>

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);

        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);

        var expectedMessage = UnavailableResultValueException.MESSAGE;
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void ResultImplicitOperatorForError__ShouldConvertIntoAResultWithError()
    {
        Error error = new("errorCode", "debugMessage", "someFieldName");
        Result result = error; //Implicit operator :  Error -> Result

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
}

class MyClass
{

}

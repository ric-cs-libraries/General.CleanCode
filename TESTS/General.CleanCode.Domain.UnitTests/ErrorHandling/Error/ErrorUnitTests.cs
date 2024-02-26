using Xunit;

using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Domain.ErrorHandling.UnitTests;

public class ErrorUnitTests
{
    [Theory]
    [InlineData("some.error", "msg for dev")]
    [InlineData("some.error", "msg for dev", "field1")]
    public void Instanciation__ShouldCorrectlyInitializeTheInstance(string code, string debugMessage, string? relatedFieldName = null)
    {
        //--- Act ---
        var error = new Error(code, debugMessage, relatedFieldName);

        //--- Assert ---
        Assert.Equal(code, error.Code);
        Assert.Equal(debugMessage, error.DebugMessage);
        Assert.Equal(relatedFieldName, error.RelatedFieldName);
    }

    [Fact]
    public void Instanciation_WhenRelatedFieldNameIsNotPassed_ShouldCorrectlyInitializeTheInstanceWithANullRelatedFieldName()
    {
        //--- Act ---
        var error = new Error("some.code", "msg");

        //--- Assert ---
        Assert.Null(error.RelatedFieldName);
    }

    [Fact]
    public void Instanciation_WhenErrorCodeIsEmptyStringOrWhiteSpacesOnly_ShouldThrowAnErrorCodeIsRequiredException()
    {
        //--- Act & Assert ---
        Assert.Throws<ErrorCodeIsRequiredException>(() => new Error(" ", "msg", "field1"));
        Assert.Throws<ErrorCodeIsRequiredException>(() => new Error("", "msg", "field1"));
    }

    [Fact]
    public void ImplicitOperator__ShouldReturnErrorCode()
    {
        //--- Act ---
        Error error = new("some.code", "msg");
        string errorCode = error;

        //--- Assert ---
        Assert.Equal(errorCode, error.Code);
    }

}

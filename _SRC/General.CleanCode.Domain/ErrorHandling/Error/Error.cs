

using General.CleanCode.Domain.DDD;


namespace General.CleanCode.Domain.ErrorHandling;

public record Error : ValueObject
{
    public string Code { get; }
    public string DebugMessage { get; }
    public string? RelatedFieldName { get; }

    public Error(string code, string debugMessage, string? relatedFieldName = null)
    {
        Code = code.Trim();
        if (Code == string.Empty)
        {
            throw new ErrorCodeIsRequiredException();
        }

        DebugMessage = debugMessage;
        RelatedFieldName = relatedFieldName;
    }

    public static implicit operator string(Error error) => error.Code;
}

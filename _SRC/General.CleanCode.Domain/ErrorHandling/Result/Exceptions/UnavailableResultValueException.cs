
namespace General.CleanCode.Domain.ErrorHandling;

public class UnavailableResultValueException : Exception
{
    public override string Message { get; }

    public UnavailableResultValueException() : base("")
    {
        Message = $"Due to the operation failure, the Result value is not available.";
    }
}
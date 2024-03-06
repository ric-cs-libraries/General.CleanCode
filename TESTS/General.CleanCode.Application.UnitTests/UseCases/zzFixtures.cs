
using General.CleanCode.Domain.ErrorHandling;

namespace General.CleanCode.Application.UnitTests.UseCases;


internal record MyUseCaseRequest(long SomePositiveId);

internal record MyUseCaseResponse(int SomeIntValue, string SomeStringValue);


internal static class Errors
{
    public static readonly Error InvalidIdError = new Error("Invalid.Id", "Dans la request, l'id doit être >0 !");
}



public interface ISomeService
{
    Task<FindDataDtoResponse> FindDataAsync(long Id);
    FindDataDtoResponse FindData(long Id);

    public class UnfoundIdException : Exception
    {
        public const string MESSAGE_FORMAT = "Unfound data for Id={0}";

        public override string Message { get; }

        public UnfoundIdException(long Id)
        {
            Message = string.Format(MESSAGE_FORMAT, Id);
        }
    }

    public class AnotherException : Exception
    {
        public const string MESSAGE = "The service has encountered an unexpected issue.";

        public override string Message { get; }

        public AnotherException()
        {
            Message = MESSAGE;
        }
    }
}
public class SomeService : ISomeService
{
    private Dictionary<long, int> intsDico = new()
    {
        { 10, 1000 },
        { 20, 2500 }
    };
    public async Task<FindDataDtoResponse> FindDataAsync(long id)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(3));

        return FindData(id);
    }

    public FindDataDtoResponse FindData(long id)
    {

        if (intsDico.TryGetValue(id, out int intValue))
        {
            return new(intValue, $"Id '{id}' is ok");
        }
        else
        {
            throw new ISomeService.UnfoundIdException(id);
        }
    }
}

public record FindDataDtoResponse(int IntValue, string StringValue);

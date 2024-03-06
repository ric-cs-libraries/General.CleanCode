using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Application.CleanArchi.UseCases;

public abstract class QueryUseCaseAsync<TUseCaseRequest, TUseCaseResponse> : UseCaseAsync<TUseCaseRequest, Result<TUseCaseResponse>>
{
    protected override Result<TUseCaseResponse> GetOnOperationCanceledResponse(Error operationCanceledError)
    {
        return Result<TUseCaseResponse>.NotOk(operationCanceledError);
    }
}
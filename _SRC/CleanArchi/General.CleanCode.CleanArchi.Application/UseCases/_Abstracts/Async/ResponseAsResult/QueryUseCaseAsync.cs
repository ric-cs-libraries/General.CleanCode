namespace General.CleanCode.CleanArchi.Application.UseCases;

public abstract class QueryUseCaseAsync<TUseCaseRequest, TUseCaseResponse> : UseCaseAsync<TUseCaseRequest, Result<TUseCaseResponse>>
{
    protected override Result<TUseCaseResponse> GetOnOperationCanceledResponse(Error operationCanceledError)
    {
        return Result<TUseCaseResponse>.NotOk(operationCanceledError);
    }
}
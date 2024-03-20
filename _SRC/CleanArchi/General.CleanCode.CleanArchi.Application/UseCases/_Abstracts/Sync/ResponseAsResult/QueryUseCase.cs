namespace General.CleanCode.CleanArchi.Application.UseCases;

public abstract class QueryUseCase<TUseCaseRequest, TUseCaseResponse> : UseCase<TUseCaseRequest, Result<TUseCaseResponse>>
{
    protected override Result<TUseCaseResponse> GetOnOperationCanceledResponse(Error operationCanceledError)
    {
        return Result<TUseCaseResponse>.NotOk(operationCanceledError);
    }
}
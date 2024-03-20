namespace General.CleanCode.CleanArchi.Application.UseCases;

public abstract class CommandUseCaseAsync<TUseCaseRequest> : UseCaseAsync<TUseCaseRequest, Result>
{
    protected override Result GetOnOperationCanceledResponse(Error operationCanceledError)
    {
        return Result.NotOk(operationCanceledError);
    }
}

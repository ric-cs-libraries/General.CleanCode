using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Application.CleanArchi.UseCases;

public abstract class CommandUseCaseAsync<TUseCaseRequest> : UseCaseAsync<TUseCaseRequest, Result>
{
    protected override Result GetOnOperationCanceledResponse(Error operationCanceledError)
    {
        return Result.NotOk(operationCanceledError);
    }
}

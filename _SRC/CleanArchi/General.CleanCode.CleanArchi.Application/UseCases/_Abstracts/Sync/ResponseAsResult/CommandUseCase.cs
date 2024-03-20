namespace General.CleanCode.CleanArchi.Application.UseCases;

public abstract class CommandUseCase<TUseCaseRequest> : UseCase<TUseCaseRequest, Result>
{
    protected override Result GetOnOperationCanceledResponse(Error operationCanceledError)
    {
        return Result.NotOk(operationCanceledError);
    }
}

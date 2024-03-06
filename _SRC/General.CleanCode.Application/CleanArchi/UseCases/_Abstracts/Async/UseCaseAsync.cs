using General.CleanCode.Domain.ErrorHandling;


namespace General.CleanCode.Application.CleanArchi.UseCases;

//--- POUR LES UseCases destinés à être utilisés UNIQUEMENT de façon ASYNCHRONE ---
public abstract class UseCaseAsync<TUseCaseRequest, TUseCaseResponse> :  IUseCaseAsync<TUseCaseRequest, TUseCaseResponse>
{
    public async Task<TUseCaseResponse> RunAsync(TUseCaseRequest request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            TUseCaseResponse response = await HandleAsync(request, cancellationToken);
            return response;
        }
        catch (OperationCanceledException ex)
        {
            Error error = ToOperationCanceledError(ex);
            return GetOnOperationCanceledResponse(error);
        }
    }
    
    protected abstract Task<TUseCaseResponse> HandleAsync(TUseCaseRequest request, CancellationToken cancellationToken);
    protected abstract TUseCaseResponse GetOnOperationCanceledResponse(Error operationCanceledError);

    private Error ToOperationCanceledError(OperationCanceledException ex) =>
        (ex is TaskCanceledException) ? AsyncErrors.UseCaseHandlingTaskCancellation : AsyncErrors.UseCaseHandlingTaskOperationCancellation;

}
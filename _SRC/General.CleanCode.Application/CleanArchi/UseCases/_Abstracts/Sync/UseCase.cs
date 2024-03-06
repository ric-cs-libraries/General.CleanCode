namespace General.CleanCode.Application.CleanArchi.UseCases;


//--- POUR LES UseCases destinés à être utilisés de façon SYNCHRONE(Run) ou de façon ASYNCHRONE(RunAsync) ---
public abstract class UseCase<TUseCaseRequest, TUseCaseResponse> : UseCaseAsync<TUseCaseRequest, TUseCaseResponse>, IUseCase<TUseCaseRequest, TUseCaseResponse>
{
    public TUseCaseResponse Run(TUseCaseRequest request)
    {
        TUseCaseResponse response = Handle(request);
        return response;
    }
    protected abstract TUseCaseResponse Handle(TUseCaseRequest request);

    protected override Task<TUseCaseResponse> HandleAsync(TUseCaseRequest request, CancellationToken cancellationToken)
    {
        Task<TUseCaseResponse> task = Task.Run(() =>
        {
            return Run(request);

        }, cancellationToken);

        return task;
    }
}
namespace General.CleanCode.Application.CleanArchi.UseCases;

public interface IUseCaseAsync<TUseCaseRequest, TUseCaseResponse>
{
    Task<TUseCaseResponse> RunAsync(TUseCaseRequest request, CancellationToken cancellationToken);
}

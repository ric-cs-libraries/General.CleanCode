namespace General.CleanCode.CleanArchi.Application.UseCases;

public interface IUseCaseAsync<TUseCaseRequest, TUseCaseResponse>
{
    Task<TUseCaseResponse> RunAsync(TUseCaseRequest request, CancellationToken cancellationToken);
}

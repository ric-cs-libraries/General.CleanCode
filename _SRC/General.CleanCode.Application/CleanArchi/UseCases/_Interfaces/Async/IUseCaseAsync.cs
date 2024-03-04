namespace General.CleanCode.Application.CleanArchi;

public interface IUseCaseAsync<TUseCaseRequest, TUseCaseResponse>
{
    Task<TUseCaseResponse> HandleAsync(TUseCaseRequest request, CancellationToken cancellationToken);
}

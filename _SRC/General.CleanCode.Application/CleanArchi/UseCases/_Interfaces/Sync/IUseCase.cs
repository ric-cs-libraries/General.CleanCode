namespace General.CleanCode.Application.CleanArchi.UseCases;

public interface IUseCase<TUseCaseRequest, TUseCaseResponse> : IUseCaseAsync<TUseCaseRequest, TUseCaseResponse>
{
    TUseCaseResponse Run(TUseCaseRequest request);
}
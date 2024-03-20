namespace General.CleanCode.CleanArchi.Application.UseCases;

public interface IUseCase<TUseCaseRequest, TUseCaseResponse> : IUseCaseAsync<TUseCaseRequest, TUseCaseResponse>
{
    TUseCaseResponse Run(TUseCaseRequest request);
}
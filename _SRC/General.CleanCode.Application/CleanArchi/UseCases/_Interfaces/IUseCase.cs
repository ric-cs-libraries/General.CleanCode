namespace General.CleanCode.Application.CleanArchi;

public interface IUseCase<TUseCaseRequest, TUseCaseResponse>
{
    TUseCaseResponse Handle(TUseCaseRequest request);
}
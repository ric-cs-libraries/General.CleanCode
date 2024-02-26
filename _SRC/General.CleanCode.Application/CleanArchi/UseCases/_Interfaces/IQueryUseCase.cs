using General.CleanCode.Domain.ErrorHandling;

namespace General.CleanCode.Application.CleanArchi;

public interface IQueryUseCase<TUseCaseRequest, TUseCaseResponse> : IUseCase<TUseCaseRequest, Result<TUseCaseResponse>>
{
}

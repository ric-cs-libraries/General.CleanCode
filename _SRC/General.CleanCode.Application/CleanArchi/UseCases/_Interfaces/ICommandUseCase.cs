using General.CleanCode.Domain.ErrorHandling;

namespace General.CleanCode.Application.CleanArchi;

public interface ICommandUseCase<TUseCaseRequest> : IUseCase<TUseCaseRequest, Result>
{
}

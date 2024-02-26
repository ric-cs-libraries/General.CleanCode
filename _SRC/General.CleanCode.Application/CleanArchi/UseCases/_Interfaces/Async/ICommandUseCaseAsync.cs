using General.CleanCode.Domain.ErrorHandling;

namespace General.CleanCode.Application.CleanArchi;

public interface ICommandUseCaseAsync<TUseCaseRequest> : IUseCaseAsync<TUseCaseRequest, Result>
{
}

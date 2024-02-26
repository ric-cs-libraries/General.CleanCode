using General.CleanCode.Domain.ErrorHandling;

namespace General.CleanCode.Application.CleanArchi;

public interface IQueryUseCaseAsync<TUseCaseRequest, TUseCaseResponse> : IUseCaseAsync<TUseCaseRequest, Result<TUseCaseResponse>>
{
}

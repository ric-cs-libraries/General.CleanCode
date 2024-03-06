using General.CleanCode.Domain.ErrorHandling;

namespace General.CleanCode.Application.CleanArchi.UseCases;

public static class AsyncErrors
{
    public static readonly Error UseCaseHandlingTaskCancellation = new("Use.Case.Handling.Task.Cancellation", "Use case handling task cancellation.");
    public static readonly Error UseCaseHandlingTaskOperationCancellation = new("Use.Case.Handling.TaskOperation.Cancellation", "Use case handling task operation cancellation.");
}

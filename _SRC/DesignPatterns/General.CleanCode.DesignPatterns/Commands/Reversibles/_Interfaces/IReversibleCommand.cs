using General.Basics.ErrorHandling;

namespace General.Basics.DesignPatterns.Commands.Interfaces;

public interface IReversibleCommand : ICommand
{
    Result Reverse();
    Task<Result> ReverseAsync();

    ReversibleCommandState State { get; set; }

    bool IsPending();
    bool IsSuccessfullyExecuted();
    bool WasReversed();
}

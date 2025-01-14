using General.Basics.ErrorHandling;


namespace General.Basics.DesignPatterns.Commands.Interfaces;

public interface ICommand
{
    string Name { get; }

    Result Execute();
    Task<Result> ExecuteAsync();
}

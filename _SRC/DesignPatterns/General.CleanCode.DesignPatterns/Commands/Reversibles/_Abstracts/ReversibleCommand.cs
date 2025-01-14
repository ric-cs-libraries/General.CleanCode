
using General.Basics.DesignPatterns.Commands.Interfaces;
using General.Basics.ErrorHandling;
using General.Basics.ReflectionExtended.POO.Extensions;


namespace General.Basics.DesignPatterns.Commands.Abstracts;

public abstract class ReversibleCommand : Command, IReversibleCommand
{
    public ReversibleCommandState State { get; set; }


    protected ReversibleCommand() : base()
    {
#if DEBUG
        Type concreteReversibleCommandClassType = GetType();
        Type ReversibleCommandClassType = typeof(ReversibleCommand);
        concreteReversibleCommandClassType.CheckIfRedefinesOneOfTheseMethods_(
            new[] { nameof(Reverse), nameof(ReverseAsync) },
            ReversibleCommandClassType
        );
#endif
    }

    public virtual Result Reverse()
    {
        Result result = ReverseAsync().GetAwaiter().GetResult();
        return result;
    }

    public virtual async Task<Result> ReverseAsync()
    {
        return await Task.Run(() => Reverse());
    }


    public bool IsPending()
    {
        return State.IsPending();
    }

    public bool IsSuccessfullyExecuted()
    {
        return State.IsSuccessfullyExecuted();
    }

    public bool WasReversed()
    {
        return State.IsReversed();
    }
}

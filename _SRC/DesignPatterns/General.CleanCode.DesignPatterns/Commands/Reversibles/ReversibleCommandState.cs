namespace General.Basics.DesignPatterns.Commands;

public enum ReversibleCommandState
{
    Pending = 0, //Valeur par défaut

    Started,
    SuccessfullyExecuted,

    Reversing,
    Reversed,
}

public static class ReversibleCommandStateExension
{
    public static bool IsPending(this ReversibleCommandState commandState)
    {
        return (commandState == ReversibleCommandState.Pending);
    }

    public static bool IsStarted(this ReversibleCommandState commandState)
    {
        return (commandState == ReversibleCommandState.Started);
    }
    public static bool IsSuccessfullyExecuted(this ReversibleCommandState commandState)
    {
        return (commandState == ReversibleCommandState.SuccessfullyExecuted);
    }

    public static bool IsReversing(this ReversibleCommandState commandState)
    {
        return (commandState == ReversibleCommandState.Reversing);
    }

    public static bool IsReversed(this ReversibleCommandState commandState)
    {
        return (commandState == ReversibleCommandState.Reversed);
    }
}

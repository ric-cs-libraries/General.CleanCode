
using General.Basics.DesignPatterns.Commands.Interfaces;
using General.Basics.ErrorHandling;
using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.POO.Extensions;


namespace General.Basics.DesignPatterns.Commands.Abstracts;

public abstract class Command : ICommand
{
    public string Name => GetType().GetName_();

    protected Command()
    {
#if DEBUG
        Type concreteCommandClassType = GetType();
        Type CommandClassType = typeof(Command);
        concreteCommandClassType.CheckIfRedefinesOneOfTheseMethods_(
            new[] { nameof(Execute), nameof(ExecuteAsync) },
            CommandClassType
        );
#endif
    }

    public virtual Result Execute()
    {
        Result result = ExecuteAsync().GetAwaiter().GetResult();
        return result;
    }

    public virtual async Task<Result> ExecuteAsync()
    {
        return await Task.Run(() => Execute());
    }
}

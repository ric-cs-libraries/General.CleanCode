namespace General.CleanCode.DDD;

public abstract record EntityId<TId>(TId Value) : ValueObject;

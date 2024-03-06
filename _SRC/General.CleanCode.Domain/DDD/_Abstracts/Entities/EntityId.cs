namespace General.CleanCode.Domain.DDD;

public abstract record EntityId<TId>(TId Value) : ValueObject;

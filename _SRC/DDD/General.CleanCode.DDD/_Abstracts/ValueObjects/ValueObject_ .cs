namespace General.CleanCode.DDD.Abstracts;


public abstract record ValueObject<TValue>(TValue Value) : ValueObject
{
}

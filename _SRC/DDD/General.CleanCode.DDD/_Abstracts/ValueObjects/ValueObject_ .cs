namespace General.CleanCode.DDD;


public abstract record ValueObject<TValue>(TValue Value) : ValueObject
{
}

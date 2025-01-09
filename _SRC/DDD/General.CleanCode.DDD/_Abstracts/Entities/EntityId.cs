namespace General.CleanCode.DDD.Abstracts;

public abstract record EntityId<TId>(TId Value) : ValueObject
    where TId : struct //Type valeur non nullabe
;

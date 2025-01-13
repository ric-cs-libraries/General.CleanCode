namespace General.CleanCode.DDD.Interfaces;


public interface IDomainEvent
{
    DateTime CreationDateUtc { get; }
}

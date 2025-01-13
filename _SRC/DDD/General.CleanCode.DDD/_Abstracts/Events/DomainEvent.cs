using General.CleanCode.DDD.Interfaces;


namespace General.CleanCode.DDD.Abstracts.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTime CreationDateUtc { get; } = DateTime.UtcNow;
}

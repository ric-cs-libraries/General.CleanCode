using General.CleanCode.DDD.Interfaces;

namespace General.CleanCode.DDD.Abstracts;


public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id) : base(id)
    {

    }

    public void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
}

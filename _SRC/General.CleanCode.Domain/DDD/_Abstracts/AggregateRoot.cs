namespace General.CleanCode.Domain.DDD;


public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id) : base(id)
    {

    }

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
}

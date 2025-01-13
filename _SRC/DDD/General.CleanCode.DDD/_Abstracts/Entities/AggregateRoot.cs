using General.CleanCode.DDD.Interfaces;

namespace General.CleanCode.DDD.Abstracts;


public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();


    protected AggregateRoot(TId id) : base(id)
    {
    }


    public IReadOnlyList<IDomainEvent> PopEvents()
    {
        var domainEvents = DomainEvents;
        _domainEvents.Clear();
        return domainEvents;
    }
    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

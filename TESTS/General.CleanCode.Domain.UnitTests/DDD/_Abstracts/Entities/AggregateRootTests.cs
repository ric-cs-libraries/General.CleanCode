using Xunit;


using General.CleanCode.Domain.DDD;


namespace General.CleanCode.Domain.UnitTests.DDD;

public class AggregateRootTests
{
    [Fact]
    public void AggregateRootOfT__ShouldHaveEntityOfTAsDirectParentClass()
    {
        Assert.True(typeof(AggregateRoot<int>).BaseType == typeof(Entity<int>));
        Assert.True(typeof(AggregateRoot<MyIdClass>).BaseType == typeof(Entity<MyIdClass>));
    }

    [Fact]
    public void Instanciation__ShouldInitializeCorrectlyTheId()
    {
        MyIdClass id = new();
        MyAggregate a1 = new(id);

        Assert.Equal(id, a1.Id);
    }

    //#region Id non nullable
    //[Fact]
    //public void Instanciation_WhenTIdNullableAndIdNull_ShouldThrowAnIdCannotBeSetToNullException()
    //{
    //    var ex = Assert.Throws<IdCannotBeSetToNullException>(() => new MyAggregate2(null));

    //}
    //#endregion Id non nullable

    #region Domain events
    [Fact]
    public void Instanciation__ShouldInitializeCorrectlyTheDomainEventsList()
    {
        MyIdClass id = new();
        MyAggregate a1 = new(id);

        Assert.NotNull(a1.DomainEvents);
        Assert.Empty(a1.DomainEvents);
    }

    [Fact]
    public void _WhenDomainEventIsRaised_ShouldAddItToTheDomainEventsList()
    {
        string id = "";
        MyAggregateEventRaiser a1 = new(id);
        a1.DoThat();

        Assert.NotNull(a1.DomainEvents);
        Assert.NotEmpty(a1.DomainEvents);
        Assert.Equal(1, a1.DomainEvents.Count);

        a1.DoThat();
        Assert.Equal(1+1, a1.DomainEvents.Count);

        Assert.IsAssignableFrom<IDomainEvent>(a1.DomainEvents.First());
        Assert.IsAssignableFrom<IDomainEvent>(a1.DomainEvents.Last());
    }

    [Fact]
    public void ClearEvents__ShouldClearTheDomainEventsList()
    {
        string id = "";
        MyAggregateEventRaiser a1 = new(id);
        a1.DoThat();
        a1.DoThat();

        Assert.NotNull(a1.DomainEvents);
        Assert.NotEmpty(a1.DomainEvents);
        Assert.Equal(1 + 1, a1.DomainEvents.Count);

        a1.ClearEvents();
        Assert.Empty(a1.DomainEvents);
    }
    #endregion Domain events

}
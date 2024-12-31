using General.CleanCode.DDD.Interfaces.Entities;


namespace General.CleanCode.DDD.UnitTests;

internal record ValueObject1 : ValueObject
{
    public int IntMember { get; }
    public string StrMember { get; }

    public ValueObject1(int intMember, string strMember)
    {
        IntMember = intMember;
        StrMember = strMember;
    }
}

internal record ValueObject2A : ValueObject1
{
    public ValueObject2A(int intMember, string strMember) : base(intMember, strMember)
    {
    }
}

internal record ValueObject2B : ValueObject1
{
    public bool BoolMember { get; }

    public ValueObject2B(int intMember, string strMember, bool boolMember) : base(intMember, strMember)
    {
        BoolMember = boolMember;
    }
}



internal class ClassValueObject1 : ClassValueObject
{
    public int IntMember { get; }
    public string StrMember { get; }

    public ClassValueObject1(int intMember, string strMember)
    {
        IntMember = intMember;
        StrMember = strMember;
    }

    protected override IEnumerable<object> GetMembersToCompare()
    {
        yield return IntMember;
        yield return StrMember;
    }
}


internal class ClassValueObject2A : ClassValueObject1
{
    public ClassValueObject2A(int intMember, string strMember) : base(intMember, strMember)
    {
    }
}

internal class ClassValueObject2B : ClassValueObject1
{
    public bool BoolMember { get; }

    public ClassValueObject2B(int intMember, string strMember, bool boolMember) : base(intMember, strMember)
    {
        BoolMember = boolMember;
    }

    protected override IEnumerable<object> GetMembersToCompare()
    {
        yield return IntMember;
        yield return StrMember;
        yield return BoolMember;
    }
}

internal class ClassValueObject3A : ClassValueObject1
{
    public bool BoolMember { get; }

    public ClassValueObject3A(int intMember, string strMember, bool boolMember) : base(intMember, strMember)
    {
        BoolMember = boolMember;
    }

    protected override IEnumerable<object> GetMembersToCompare()
    {
        yield return IntMember;
        yield return StrMember;
        //yield return BoolMember; //<<<<<<ce membre NE SERA PAS comparé donc, la comparaison de membre de ne se fera que sur les membres ci-dessus : IntMember et StrMember
    }
}



//================================================================================



class Entity1 : Entity<long>
{
    public Entity1(long id) : base(id)
    {
    }
}

class Entity2 : Entity1
{
    public Entity2(long id) : base(id)
    {
    }
}

class Entity3 : Entity<long>
{
    public Entity3(long id) : base(id)
    {
    }
}
class MyClass4
{
}



class MyEntity : Entity<MyIdClass>
{
    public MyEntity(MyIdClass id) : base(id)
    {
    }
}

//class EntityIdNullable : Entity<long?>   //Depuis que j'ai mis <WarningsAsErrors>nullable</WarningsAsErrors> , il devient, à juste titre,
//                                         // interdit d'avoir un TId nullable, c-à-d que ce n'est plus juste un avertissement !
//{
//    public EntityIdNullable(long? id) : base(id)
//    {
//    }
//}

class MyIdClass
{
}

class MyAggregate : AggregateRoot<MyIdClass>
{
    public MyAggregate(MyIdClass id) : base(id)
    {
    }
}
//class MyAggregate2 : AggregateRoot<long?>    //Depuis que j'ai mis <WarningsAsErrors>nullable</WarningsAsErrors> , il devient, à juste titre,
//{                                            // interdit d'avoir un TId nullable, c-à-d que ce n'est plus juste un avertissement !
//    public MyAggregate2(long? id) : base(id)
//    {
//    }
//}

class MyDomainEvent : IDomainEvent
{

}
class MyAggregateEventRaiser : AggregateRoot<string>   //Bizarrement n'empêche pas d'instancier cette classe avec un id null !?
{
    public MyAggregateEventRaiser(string id) : base(id)
    {
    }

    public void DoThat()
    {
        RaiseEvent(new MyDomainEvent());
    }
}
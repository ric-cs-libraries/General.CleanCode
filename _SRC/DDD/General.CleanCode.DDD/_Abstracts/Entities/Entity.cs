namespace General.CleanCode.DDD.Abstracts;

public abstract class Entity<TId>
    where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        if (id is null)
        {
            throw new IdCannotBeSetToNullException();
        }

        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> e2)
            return false;

        if (ReferenceEquals(this, e2))
            return true;

        if (GetRealType() != e2.GetRealType())
            return false;

        //if (Id is null || e2.Id is null)
        //return false;

        return Id.Equals(e2.Id);
    }

    public static bool operator ==(Entity<TId> e1, Entity<TId> e2)
    {
        if (e1 is null && e2 is null)
            return true;

        if (e1 is null || e2 is null)
            return false;

        return e1.Equals(e2);
    }

    public static bool operator !=(Entity<TId> e1, Entity<TId> e2)
    {
        return !(e1 == e2);
    }

    public override int GetHashCode()
    {
        return (GetRealType().ToString() + Id).GetHashCode();
    }

    private Type GetRealType()
    {
        Type type = GetType();

        //if (type.ToString().Contains("Castle.Proxies."))   //Violation de SoC, mais le jeu en vaut la chandelle
        //return type.BaseType!;

        return type;
    }
}

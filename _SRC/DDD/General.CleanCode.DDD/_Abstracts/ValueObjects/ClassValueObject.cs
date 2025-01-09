namespace General.CleanCode.DDD.Abstracts;



//L'avantage avec ClassValueObject, c'est que contrairement à ValueObject (qui est ici un simple record),
//on va pouvoir choisir les membres à comparer (via GetMembersToCompare()) !!!
public abstract class ClassValueObject
{
    public virtual bool Equals(ClassValueObject? vo2) => (vo2 is not null) && ValuesAreEqual(vo2);
    public override bool Equals(object? obj) => Equals(obj as ClassValueObject);  //Pas obligatoire du tout, mais juste pour éviter l'avertissement.

    public static bool operator ==(ClassValueObject? vo1, ClassValueObject? vo2)
    {
        if (vo1 is null && vo2 is null)
        {
            return true;
        }

        if (vo1 is null || vo2 is null)
        {
            return false;
        }

        return vo1.Equals(vo2);
    }
    public static bool operator !=(ClassValueObject? vo1, ClassValueObject? vo2) => !(vo1 == vo2);


    protected abstract IEnumerable<object> GetMembersToCompare();

    private bool ValuesAreEqual(ClassValueObject valueObject) =>
        (GetType() == valueObject.GetType()) && GetMembersToCompare().SequenceEqual(valueObject.GetMembersToCompare());

    public override int GetHashCode() =>
        GetMembersToCompare().Aggregate(
            default(int),
            (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));
}

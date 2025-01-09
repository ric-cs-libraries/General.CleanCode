using General.CleanCode.DDD.Abstracts;

namespace General.CleanCode.DDD;

public record GuidEntityId : EntityId<Guid>
{
    public GuidEntityId() : this(Guid.NewGuid())
    {
    }
    public GuidEntityId(Guid value) : base(value)
    {
    }
}

namespace General.CleanCode.Domain.DDD;

public record GuidEntityId : EntityId<Guid>
{
    public GuidEntityId() : this(Guid.NewGuid())
    {
    }
    public GuidEntityId(Guid value) : base(value)
	{
	}
}

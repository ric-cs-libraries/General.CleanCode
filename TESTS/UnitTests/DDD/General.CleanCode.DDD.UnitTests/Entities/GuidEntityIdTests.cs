using Xunit;


namespace General.CleanCode.DDD.UnitTests;

public class GuidEntityIdTests
{
    [Fact]
    public void Instanciation_WhenGuidIsGiven_ShouldInitializeTheIdWithThisGuid()
    {
        //--- Act ---
        Guid guid = Guid.NewGuid();
        GuidEntityId geId = new(guid);

        //--- Assert ---
        Assert.NotEmpty(geId.Value.ToString());
        Assert.Equal(geId.Value, guid);
    }

    [Fact]
    public void Instanciation_WhenNoGuidIsGiven_ShouldGenerateANewOneAndInitializeTheIdWithThisGuid()
    {
        //--- Act ---
        GuidEntityId geId = new();

        //--- Assert ---
        Assert.NotEmpty(geId.Value.ToString());
    }

    [Fact]
    public void Equality_WhenSameGuidIsGiven_InstancesShouldBeEqual()
    {
        //--- Act ---
        Guid guid = Guid.NewGuid();
        GuidEntityId geId1 = new(guid);
        GuidEntityId geId2 = new(guid);

        //--- Assert ---
        Assert.Equal(geId1, geId2);
        Assert.True(geId1 == geId2);
        Assert.True(geId1.Equals(geId2));
        Assert.True(geId2.Equals(geId1));
        Assert.True(object.Equals(geId1, geId2));
        Assert.True(object.Equals(geId2, geId1));
        Assert.False(object.ReferenceEquals(geId2, geId1));
    }


    [Fact]
    public void InstanciationOfChildClass_WhenGuidIsGiven_ShouldInitializeTheIdWithThisGuid()
    {
        //--- Act ---
        Guid guid = Guid.NewGuid();
        CustomerId customerId = new(guid);

        //--- Assert ---
        Assert.NotEmpty(customerId.Value.ToString());
        Assert.Equal(customerId.Value, guid);
    }

    [Fact]
    public void InstanciationOfChildClass_WhenNoGuidIsGiven_ShouldGenerateANewOneAndInitializeTheIdWithThisGuid()
    {
        //--- Act ---
        CustomerId customerId = new();

        //--- Assert ---
        Assert.NotEmpty(customerId.Value.ToString());
    }

    [Fact]
    public void EqualityForChildClass_WhenSameGuidIsGiven_InstancesShouldBeEqual()
    {
        //--- Act ---
        Guid guid = Guid.NewGuid();
        CustomerId geId1 = new(guid);
        CustomerId geId2 = new(guid);

        //--- Assert ---
        Assert.Equal(geId1, geId2);
        Assert.True(geId1 == geId2);
        Assert.True(geId1.Equals(geId2));
        Assert.True(geId2.Equals(geId1));
        Assert.True(object.Equals(geId1, geId2));
        Assert.True(object.Equals(geId2, geId1));
        Assert.False(object.ReferenceEquals(geId2, geId1));
    }
}

record CustomerId : GuidEntityId
{
    public CustomerId() : base() { }
    public CustomerId(Guid Id) : base(Id) { }
}

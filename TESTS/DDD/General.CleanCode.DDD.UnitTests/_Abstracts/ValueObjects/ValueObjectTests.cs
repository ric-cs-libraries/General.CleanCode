using Xunit;


using General.CleanCode.DDD;


namespace General.CleanCode.DDD.UnitTests;

public class ValueObjectTests
{
    [Fact]
    public void Equality_WhenEqualAndExactSameType_ShouldBeTrue()
    {
        //--- Arrange ---
        var vo1 = new ValueObject1(10, "aA");
        var vo2 = new ValueObject1(10, "aA");

        var vo2B1 = new ValueObject2B(10, "aA", true);
        var vo2B2 = new ValueObject2B(10, "aA", true);


        //--- Act & Assert ---
        Assert.True(vo1 == vo2);
        Assert.True(vo1.Equals(vo2));

        Assert.True(vo2B1 == vo2B2);
        Assert.True(vo2B1.Equals(vo2B1));
    }

    [Fact]
    public void Equality_WhenEqualButNotExactSameType_ShouldBeFalse()
    {
        //--- Arrange ---
        var vo1 = new ValueObject1(10, "aA");
        var vo2A = new ValueObject2A(10, "aA");

        //--- Act & Assert ---
        Assert.False(vo1 == vo2A);
        Assert.False(vo1.Equals(vo2A));
    }

    [Fact]
    public void Equality_WhenComparedToACompletelyDifferentType_ShouldBeFalse()
    {
        //--- Arrange ---
        var vo1 = new ValueObject1(10, "aA");

        //--- Act & Assert ---
        Assert.False(vo1.Equals("z"));
        Assert.False(vo1.Equals(10));
        Assert.False(vo1.Equals(true));
        Assert.False(vo1.Equals(null));
        Assert.False(vo1.Equals(DateTime.Now));
        Assert.False(vo1.Equals(new { }));
        Assert.False(vo1.Equals(new { IntMember = 10, StrMember = "aA" }));
    }


    [Fact]
    public void Equality_WhenNotEqualAndExactSameType_ShouldBeFalse()
    {
        //--- Arrange ---
        var vo1 = new ValueObject1(10, "aA");
        var vo2 = new ValueObject1(10, "AA");

        var vo2B1 = new ValueObject2B(10, "aA", true);
        var vo2B2 = new ValueObject2B(10, "AA", true);
        var vo2B3 = new ValueObject2B(10, "aA", false);


        //--- Act & Assert ---
        Assert.False(vo1 == vo2);
        Assert.False(vo1.Equals(vo2));

        Assert.False(vo2B1 == vo2B2);
        Assert.False(vo2B1.Equals(vo2B2));
        Assert.False(vo2B1 == vo2B3);
        Assert.False(vo2B1.Equals(vo2B3));
    }


    [Fact]
    public void Equality_WhenBothAreNullAndExactSameTypeOrNot_ShouldBeTrue()
    {
        //--- Arrange ---
        ValueObject1? vo1 = null;
        ValueObject1? vo2 = null;
        ValueObject2A? vo2A = null;
        ValueObject2B? vo2B = null;


        //--- Act & Assert ---
        Assert.True(vo1 == vo2);
        Assert.True(vo1 == vo2A);
        Assert.True(vo1 == vo2B);
        Assert.True(vo2A == vo2B);
    }

    [Fact]
    public void Equality_WhenOnlyOneIsNull_ShouldBeFalse()
    {
        //--- Arrange ---
        ValueObject1? vo1 = new(10, "aA");
        ValueObject1? vo2 = null;


        //--- Act & Assert ---
        Assert.False(vo1 == vo2);
        Assert.False(vo1.Equals(vo2));
    }
}

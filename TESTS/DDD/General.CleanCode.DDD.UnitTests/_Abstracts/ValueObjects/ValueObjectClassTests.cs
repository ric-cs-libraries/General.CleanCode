using Xunit;

using General.CleanCode.DDD;


namespace General.CleanCode.DDD.UnitTests;

public class ClassValueObjectTests
{
    [Fact]
    public void Equality_WhenEqualAndExactSameType_ShouldBeTrue()
    {
        //--- Arrange ---
        var voClass1 = new ClassValueObject1(10, "aA");
        var voClass2 = new ClassValueObject1(10, "aA");

        var voClass2B1 = new ClassValueObject2B(10, "aA", true);
        var voClass2B2 = new ClassValueObject2B(10, "aA", true);


        //--- Act & Assert ---
        Assert.True(voClass1 == voClass2);
        Assert.True(voClass1.Equals(voClass2));

        Assert.True(voClass2B1 == voClass2B2);
        Assert.True(voClass2B1.Equals(voClass2B1));
    }

    [Fact]
    public void Equality_WhenEqualButNotExactSameType_ShouldBeFalse()
    {
        //--- Arrange ---
        var voClass1 = new ClassValueObject1(10, "aA");
        var voClass2A = new ClassValueObject2A(10, "aA");

        //--- Act & Assert ---
        Assert.False(voClass1 == voClass2A);
        Assert.False(voClass1.Equals(voClass2A));
    }

    [Fact]
    public void Equality_WhenComparedToACompletelyDifferentType_ShouldBeFalse()
    {
        //--- Arrange ---
        var voClass1 = new ClassValueObject1(10, "aA");

        //--- Act & Assert ---
        Assert.False(voClass1.Equals("z"));
        Assert.False(voClass1.Equals(10));
        Assert.False(voClass1.Equals(true));
        Assert.False(voClass1.Equals(null));
        Assert.False(voClass1.Equals(DateTime.Now));
        Assert.False(voClass1.Equals(new { }));
        Assert.False(voClass1.Equals(new { IntMember = 10, StrMember = "aA" }));
    }

    [Fact]
    public void Equality_WhenNotEqualAndExactSameType_ShouldBeFalse()
    {
        //--- Arrange ---
        var voClass1 = new ClassValueObject1(10, "aA");
        var voClass2 = new ClassValueObject1(10, "AA");

        var voClass2B1 = new ClassValueObject2B(10, "aA", true);
        var voClass2B2 = new ClassValueObject2B(10, "AA", true);
        var voClass2B3 = new ClassValueObject2B(10, "aA", false);


        //--- Act & Assert ---
        Assert.False(voClass1 == voClass2);
        Assert.False(voClass1.Equals(voClass2));

        Assert.False(voClass2B1 == voClass2B2);
        Assert.False(voClass2B1.Equals(voClass2B2));
        Assert.False(voClass2B1 == voClass2B3);
        Assert.False(voClass2B1.Equals(voClass2B3));
    }

    [Fact]
    public void Equality_WhenExactSameTypeAndCompareOnlySomeEqualMembers_ShouldBeTrue() //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< L'avantage avec ClassValueObject
    {                                                                                   // c'est que l'on peut choisir les membres à comparer !!
        //--- Arrange ---
        var voClass3A1 = new ClassValueObject3A(10, "aA", false);
        var voClass3A2 = new ClassValueObject3A(10, "aA", true);


        //--- Act & Assert ---
        Assert.True(voClass3A1 == voClass3A2);
        Assert.True(voClass3A1.Equals(voClass3A2));
    }


    [Fact]
    public void Equality_WhenBothAreNullAndExactSameTypeOrNot_ShouldBeTrue()
    {
        //--- Arrange ---
        ClassValueObject1? voClass1 = null;
        ClassValueObject1? voClass2 = null;
        ClassValueObject2A? voClass2A = null;
        ClassValueObject2B? voClass2B = null;


        //--- Act & Assert ---
        Assert.True(voClass1 == voClass2);
        Assert.True(voClass1 == voClass2A);
        Assert.True(voClass1 == voClass2B);
        Assert.True(voClass2A == voClass2B);
    }

    [Fact]
    public void Equality_WhenOnlyOneIsNull_ShouldBeFalse()
    {
        //--- Arrange ---
        ClassValueObject1? voClass1 = new(10, "aA");
        ClassValueObject1? voClass2 = null;


        //--- Act & Assert ---
        Assert.False(voClass1 == voClass2);
        Assert.False(voClass1.Equals(voClass2));
    }
}
using Xunit;


using General.CleanCode.Domain.DDD;


namespace General.CleanCode.Domain.UnitTests.DDD;

public class EntityTests
{
    [Fact]
    public void Instanciation__ShouldInitializeCorrectlyTheId()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Assert.Equal(id, e1.Id);

        MyIdClass idClassObj = new();
        MyEntity e = new(idClassObj);
        Assert.Equal(idClassObj, e.Id);
    }

    //#region Id non nullable
    //[Fact]
    //public void Instanciation_WhenTIdNullableAndIdNull_ShouldThrowAnIdCannotBeSetToNullException()
    //{
    //    var ex = Assert.Throws<IdCannotBeSetToNullException>(() => new EntityIdNullable(null));

    //}
    //#endregion Id non nullable

    #region Equals
    [Fact]
    public void Equals_WhenEntityComparedToItself_ShouldReturnTrue()
    {
        long id = 10L;
        Entity1 e1 = new(id);

        Assert.True(e1.Equals(e1));
    }

    [Fact]
    public void Equals_WhenEntitiesHaveTheExactSameTypeAndId_ShouldReturnTrue()
    {
        long id = 10L;
        Entity1 e10 = new(id);
        Entity1 e11 = new(id);

        Assert.True(e10.Equals(e11));
        Assert.True(e11.Equals(e10));
    }

    [Fact]
    public void Equals_WhenEntitiesHaveTheExactSameTypeButNotTheSameId_ShouldReturnFalse()
    {
        Entity1 e1 = new(10L);
        Entity1 e2 = new(11L);

        Assert.False(e1.Equals(e2));
        Assert.False(e2.Equals(e1));
    }

    [Fact]
    public void Equals_WhenEntitiesHaveNotTheExactSameTypeButHaveSameId_ShouldReturnFalse()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Entity2 e2 = new(id); //Entity2 dérive de Entity1

        Assert.False(e1.Equals(e2));
        Assert.False(e2.Equals(e1));
    }

    [Fact]
    public void Equals_WhenEntitiesHaveNotTheSameTypeButHaveSameId_ShouldReturnFalse()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Entity3 e3 = new(id);

        Assert.False(e1.Equals(e3));
        Assert.False(e3.Equals(e1));
    }

    [Fact]
    public void Equals_WhenEntityIsComparedToANonEntity_ShouldReturnFalse()
    {
        Entity1 e1 = new(10L);
        long x = 10L;
        MyClass4 y = new();

        Assert.False(e1.Equals(x));
        Assert.False(e1.Equals(y));
    }

    [Fact]
    public void Equals_WhenTheOtherEntityIsNull_ShouldReturnFalse()
    {
        Entity1? e1 = new(10L);
        Entity2? e2 = null; //Dérive de Entity1

        Assert.False(e1.Equals(e2));
    }
    #endregion Equals


    #region ==
    [Fact]
    public void EqualitySymbol_WhenBothEntitiesAreNull_ShouldEvalTrue()
    {
        Entity1? e1 = null;
        Entity3? e3 = null;
        MyClass4? e = null;

        Assert.True(e1! == e3!);
        //Assert.True(e == e1); //Interdit
    }

    [Fact]
    public void EqualitySymbol_WhenTheOtherEntityIsNull_ShouldEvalFalse()
    {
        Entity1? e1 = new(10L);
        Entity3? e3 = null;
        MyClass4? e = null;

        Assert.False(e1 == e3!);
        //Assert.False(e == e1); //Interdit
    }


    [Fact]
    public void EqualitySymbol_WhenEntityComparedToItself_ShouldEvalTrue()
    {
        long id = 10L;
        Entity1 e1 = new(id);

        Assert.True(e1 == e1);
    }

    [Fact]
    public void EqualitySymbol_WhenEntitiesHaveTheExactSameTypeAndId_ShouldEvalTrue()
    {
        long id = 10L;
        Entity1 e10 = new(id);
        Entity1 e11 = new(id);

        Assert.True(e10 == e11);
        Assert.True(e11 == e10);
    }

    [Fact]
    public void EqualitySymbol_WhenEntitiesHaveTheExactSameTypeButNotTheSameId_ShouldEvalFalse()
    {
        Entity1 e1 = new(10L);
        Entity1 e2 = new(11L);

        Assert.False(e1 == e2);
        Assert.False(e2 == e1);
    }

    [Fact]
    public void EqualitySymbol_WhenEntitiesHaveNotTheExactSameTypeButHaveSameId_ShouldEvalFalse()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Entity2 e2 = new(id); //Entity2 dérive de Entity1

        Assert.False(e1 == e2);
        Assert.False(e2 == e1);
    }

    [Fact]
    public void EqualitySymbol_WhenEntitiesHaveNotTheSameTypeButHaveSameId_ShouldEvalFalse()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Entity3 e3 = new(id);

        Assert.False(e1 == e3);
        Assert.False(e3 == e1);
    }

    [Fact]
    public void EqualitySymbol_WhenEntityIsComparedToANonEntity_ShouldEvalFalse()
    {
        Entity1 e1 = new(10L);
        long x = 10L;
        MyClass4 y = new();

        //Assert.False(e1 == x); //Interdit
        //Assert.False(e1 == y); //Interdit
    }
    #endregion ==


    #region !=
    [Fact]
    public void NonEqualitySymbol_WhenBothEntitiesAreNull_ShouldEvalFalse()
    {
        Entity1? e1 = null;
        Entity3? e3 = null;
        MyClass4? e = null;

        Assert.False(e1! != e3!);
        //Assert.False(e != e1); //Interdit
    }

    [Fact]
    public void NonEqualitySymbol_WhenTheOtherEntityIsNull_ShouldEvalTrue()
    {
        Entity1? e1 = new(10L);
        Entity3? e3 = null;
        MyClass4? e = null;

        Assert.True(e1 != e3!);
        //Assert.True(e != e1); //Interdit
    }


    [Fact]
    public void NonEqualitySymbol_WhenEntityComparedToItself_ShouldEvalFalse()
    {
        long id = 10L;
        Entity1 e1 = new(id);

        Assert.False(e1 != e1);
    }

    [Fact]
    public void NonEqualitySymbol_WhenEntitiesHaveTheExactSameTypeAndId_ShouldEvalFalse()
    {
        long id = 10L;
        Entity1 e10 = new(id);
        Entity1 e11 = new(id);

        Assert.False(e10 != e11);
        Assert.False(e11 != e10);
    }

    [Fact]
    public void NonEqualitySymbol_WhenEntitiesHaveTheExactSameTypeButNotTheSameId_ShouldEvalTrue()
    {
        Entity1 e1 = new(10L);
        Entity1 e2 = new(11L);

        Assert.True(e1 != e2);
        Assert.True(e2 != e1);
    }

    [Fact]
    public void NonEqualitySymbol_WhenEntitiesHaveNotTheExactSameTypeButHaveSameId_ShouldEvalTrue()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Entity2 e2 = new(id); //Entity2 dérive de Entity1

        Assert.True(e1 != e2);
        Assert.True(e2 != e1);
    }

    [Fact]
    public void NonEqualitySymbol_WhenEntitiesHaveNotTheSameTypeButHaveSameId_ShouldEvalTrue()
    {
        long id = 10L;
        Entity1 e1 = new(id);
        Entity3 e3 = new(id);

        Assert.True(e1 != e3);
        Assert.True(e3 != e1);
    }

    [Fact]
    public void NonEqualitySymbol_WhenEntityIsComparedToANonEntity_ShouldEvalTrue()
    {
        Entity1 e1 = new(10L);
        long x = 10L;
        MyClass4 y = new();

        //Assert.True(e1 != x); //Interdit
        //Assert.True(e1 != y); //Interdit
    }
    #endregion !=

    #region GetHashCode
    [Fact]
    public void GetHashCode_WhenEntitiesHaveNotTheExactSameTypeOrNotTheSameId_ShouldNotBeEqual()
    {
        long id = 10L;
        Entity1 e10 = new(id: 10L);
        Entity1 e11 = new(id: 11L);
        Entity2 e2 = new(id); //Entity2 dérive de Entity1
        Entity3 e3 = new(id);

        var e10HashCode = e10.GetHashCode();
        Assert.NotEqual(e10HashCode, e11.GetHashCode());
        Assert.NotEqual(e10HashCode, e2.GetHashCode());
        Assert.NotEqual(e10HashCode, e3.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WhenEntitiesHaveTheExactSameTypeAndTheSameId_ShouldBeEqual()
    {
        long id = 10L;
        Entity1 e10 = new(id);
        Entity1 e11 = new(id);

        Assert.Equal(e10.GetHashCode(), e11.GetHashCode());
    }
    #endregion GetHashCode
}

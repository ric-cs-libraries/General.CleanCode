
namespace General.CleanCode.Domain.DDD;


public class IdCannotBeSetToNullException : Exception
{
    public const string MESSAGE = "TId ne devrait pas �tre nullable, car l'Id ne doit pas �tre null ! Conseil : pour plus de souplesse, faire que TId soit un type objet non nullable.";

    public override string Message { get; } = "";

    public IdCannotBeSetToNullException() : base("")
    {
        Message = MESSAGE;
    }
}
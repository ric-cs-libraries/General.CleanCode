namespace General.CleanCode.DDD.Abstracts;

public class IdCannotBeSetToNullException : Exception
{
    public const string MESSAGE = "TId ne devrait pas être nullable, car l'Id ne doit pas être null ! Conseil : pour plus de souplesse, faire que TId soit un type objet non nullable.";

    public override string Message { get; } = "";

    public IdCannotBeSetToNullException() : base("")
    {
        Message = MESSAGE;
    }
}
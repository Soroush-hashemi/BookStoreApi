namespace BookStore.Domain.Exceptions;

public class NullPropertyException : Exception
{
    public NullPropertyException(string message) : base(message)
    {

    }

    public NullPropertyException()
    {

    }
}

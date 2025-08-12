namespace BookStore.Domain.Exceptions;

public class SlugExistExceptions : Exception
{
    public SlugExistExceptions(string message) : base(message)
    {

    }

    public SlugExistExceptions()
    {

    }
}

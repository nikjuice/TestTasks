namespace MultiSearch.Exceptions;

public class MultiSearchValidationException : Exception
{
    public MultiSearchValidationException()
    {
    }

    public MultiSearchValidationException(string message)
        : base(message)
    {
    }

    public MultiSearchValidationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
namespace MuralVirtual.Domain.Exceptions;

public class CustomResponseException : Exception
{
    public int? StatusCode { get; set; }

    public CustomResponseException()
    {
    }

    public CustomResponseException(string message)
        : base(message)
    {

    }

    public CustomResponseException(string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
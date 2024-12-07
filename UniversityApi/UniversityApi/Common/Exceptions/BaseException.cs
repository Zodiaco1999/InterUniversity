namespace UniversityApi.Common.Exceptions;

public class BaseException : Exception
{
    public int Code { get; }
    public string Description { get; } = string.Empty;
    public bool IsWarning { get; }
    public Exception? Excepcion { get; set; }

    public BaseException(string message, int code, bool isWarning = false) : base(message)
    {
        Code = code;
        IsWarning = isWarning;
    }

    public BaseException(string message, string description, int code, bool isWarning = false) : base(message)
    {
        Code = code;
        Description = description;
        IsWarning = isWarning;
    }

    public BaseException(string message, Exception innerException, int code, bool isWarning = false) : base(message, innerException)
    {
        Code = code;
        Excepcion = innerException;
        IsWarning = isWarning;
    }
}

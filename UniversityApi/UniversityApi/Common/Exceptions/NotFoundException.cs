using System.Net;

namespace UniversityApi.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) 
        : base(message, (int)HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string name, object key)
    : base($"No se encontro el registro asociado a \"{name}\" por el parametro ({key}).", (int)HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException, (int)HttpStatusCode.NotFound)
    {
    }
}

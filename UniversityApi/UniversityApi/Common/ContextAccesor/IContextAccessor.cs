namespace UniversityApi.Common.ContextAccesor;

public interface IContextAccessor
{
    string UserId { get; }
    string UserName { get; }
    string UserMail { get; }
    string ClientIP { get; }
    string Headers { get; }
    string SessionId { get; }
}

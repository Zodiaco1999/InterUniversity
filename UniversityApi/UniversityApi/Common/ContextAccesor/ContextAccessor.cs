using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace UniversityApi.Common.ContextAccesor;

public class ContextAccessor : IContextAccessor
{
    private IHttpContextAccessor _httpContextAccessor;

    public ContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId { get => _httpContextAccessor.HttpContext!.User.Identity!.Name!; }
    public string UserName { get => _httpContextAccessor.HttpContext!.User!.Identity!.Name!; }
    public string UserMail { get => throw new NotImplementedException(); }
    public string ClientIP { get => $"{_httpContextAccessor.HttpContext!.Connection.RemoteIpAddress}"; }
    public string Headers { get => JsonConvert.SerializeObject(_httpContextAccessor.HttpContext!.Request.Headers); }
    public string SessionId { get => _httpContextAccessor.HttpContext!.User.Claims.First(claim => claim.Type == "jti").Value; }
}

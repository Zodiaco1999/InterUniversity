using Microsoft.IdentityModel.Tokens;

namespace UniversityApi.Common.JWT;

public class JWTOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public DateTime Expiration => IssuedAt.Add(TimeSpan.FromMinutes(ValidForMinutes));
    public DateTime NotBefore => DateTime.Now;
    public DateTime IssuedAt => DateTime.Now;
    public int ValidForMinutes { get; set; } = 1;
    public SigningCredentials SigningCredentials { get; set; } = null!;
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UniversityApi.Common.JWT;

public interface IJWTFactory
{
    string GenerateEncodedToken(ClaimsIdentity identity);
    JwtSecurityToken DecodeToken(string token);
}
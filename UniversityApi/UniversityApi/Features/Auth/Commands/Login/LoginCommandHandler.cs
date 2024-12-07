using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApi.Common.Exceptions;
using UniversityApi.Common.Helpers;
using UniversityApi.Common.JWT;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly UniversidadContext _context;
    private readonly IJWTFactory _jwtFactory;

    public LoginCommandHandler(UniversidadContext context, IJWTFactory jwtFactory)
    {
        _context = context;
        _jwtFactory = jwtFactory;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NumeroIdentificacion == request.NumeroIdentificacion.Trim())
            ?? throw new NotFoundException("El usuario no existe");

        if (!HashHelper.CheckHash(request.Contrasena, user.Contrasena, user.Salt))
        {
            throw new ValidationException("La contraseña no es correcta");
        }

        var sesionId = Guid.NewGuid().ToString();

        return new LoginCommandResponse(
            new UsuarioLogin(
                user.UsuarioId,
                user.NumeroIdentificacion,
                $"{user.Nombres} {user.Apellidos}"),
            CreateJWT(user, sesionId), 
            sesionId);
    }

    private string CreateJWT(Usuario user, string sesionId)
    {
        var claimsIdentity = GetClaimsIdentity(user, sesionId);

        return _jwtFactory.GenerateEncodedToken(claimsIdentity);
    }

    private ClaimsIdentity GetClaimsIdentity(Usuario usuario, string sesionId)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaims(new Claim[]
        {
            new(JwtRegisteredClaimNames.NameId, usuario.UsuarioId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, usuario.UsuarioId.ToString()),
            new(JwtRegisteredClaimNames.Jti, sesionId)
        });

        return claimsIdentity;
    }
}

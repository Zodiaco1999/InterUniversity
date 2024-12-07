using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApi.Common.Exceptions;
using UniversityApi.Common.Helpers;
using UniversityApi.Common.JWT;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;
using UniversityApi.Features.Auth.Commands.Login;

namespace UniversityApi.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginCommandResponse>
{
    private readonly UniversidadContext _context;
    private readonly IMediator _mediator;

    public RegisterCommandHandler(UniversidadContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<LoginCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_context.Usuarios.Any(u => u.NumeroIdentificacion == request.NumeroIdentificacion.Trim()))
            throw new ValidationException("El numero de identificación ya esta registrado");

        var hash = HashHelper.Hash(request.Contrasena);
        var user = new Usuario
        {
            NumeroIdentificacion = request.NumeroIdentificacion.Trim(),
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            FechaNacimiento = request.FechaNacimiento,
            Contrasena = hash.Password,
            Salt = hash.Salt
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        return await _mediator.Send(new LoginCommand(user.NumeroIdentificacion, request.Contrasena));
    }
}
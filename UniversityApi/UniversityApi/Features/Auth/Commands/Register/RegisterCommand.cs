using MediatR;
using UniversityApi.Features.Auth.Commands.Login;

namespace UniversityApi.Features.Auth.Commands.Register;

public record struct RegisterCommand(
    string NumeroIdentificacion,
    string Nombres,
    string Apellidos,
    DateTime FechaNacimiento,
    string Contrasena) : IRequest<LoginCommandResponse>;

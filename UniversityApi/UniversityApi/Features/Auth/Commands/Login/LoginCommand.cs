using MediatR;

namespace UniversityApi.Features.Auth.Commands.Login;

public record struct LoginCommand(string NumeroIdentificacion, string Contrasena) : IRequest<LoginCommandResponse>;

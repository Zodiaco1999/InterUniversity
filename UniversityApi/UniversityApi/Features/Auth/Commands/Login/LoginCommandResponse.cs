namespace UniversityApi.Features.Auth.Commands.Login;

public record struct LoginCommandResponse(
    UsuarioLogin user,
    string AccessToken, 
    string RefreshToken);

public record struct UsuarioLogin(
    int UsuarioId,
    string NumeroIdentifacion,
    string Nombre);




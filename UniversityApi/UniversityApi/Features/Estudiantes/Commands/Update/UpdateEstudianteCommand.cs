using MediatR;

namespace UniversityApi.Features.Estudiantes.Commands.Update;

public record struct UpdateEstudianteCommand(
    int EstudianteId,
    string NumeroIdentificacion,
    string Nombres,
    string Apellidos,
    DateTime FechaNacimiento) : IRequest;

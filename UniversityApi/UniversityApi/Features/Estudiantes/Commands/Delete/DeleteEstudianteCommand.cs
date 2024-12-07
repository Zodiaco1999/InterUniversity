using MediatR;

namespace UniversityApi.Features.Estudiantes.Commands.Delete;

public record struct DeleteEstudianteCommand(int EstudianteId) : IRequest;

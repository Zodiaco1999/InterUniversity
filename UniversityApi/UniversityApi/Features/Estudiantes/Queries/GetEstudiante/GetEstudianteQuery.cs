using MediatR;

namespace UniversityApi.Features.Estudiantes.Queries.GetEstudiante;

public record struct GetEstudianteQuery(int EstudianteId) : IRequest<GetEstudianteQueryResponse>;

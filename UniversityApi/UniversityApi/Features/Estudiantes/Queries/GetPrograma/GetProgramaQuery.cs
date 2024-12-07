using MediatR;

namespace UniversityApi.Features.Estudiantes.Queries.GetPrograma;

public record struct GetProgramaQuery() : IRequest<GetProgramaQueryResponse>;

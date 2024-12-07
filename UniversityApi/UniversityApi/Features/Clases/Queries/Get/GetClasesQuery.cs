using MediatR;

namespace UniversityApi.Features.Clases.Queries.Get;

public record struct GetClasesQuery(int EstudianteId) : IRequest<IEnumerable<GetClasesQueryResponse>>;

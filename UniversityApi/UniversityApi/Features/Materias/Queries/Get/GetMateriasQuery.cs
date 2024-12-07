using MediatR;

namespace UniversityApi.Features.Materias.Queries.Get;

public record struct GetMateriasQuery() : IRequest<IEnumerable<GetMateriasQueryResponse>>;

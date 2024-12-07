using MediatR;
using UniversityApi.Common.Models;

namespace UniversityApi.Features.Estudiantes.Queries.Get;

public record struct GetEstudiantesQuery(GetEntityQuery Query) : IRequest<PagedResult<GetEstudiantesQueryResponse>>;

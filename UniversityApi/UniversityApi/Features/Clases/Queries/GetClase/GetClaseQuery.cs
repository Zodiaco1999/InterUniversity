using MediatR;

namespace UniversityApi.Features.Clases.Queries.GetClase;

public record struct GetClaseQuery(int ProfesorId, int MateriaId) : IRequest<GetClaseQueryResponse>;

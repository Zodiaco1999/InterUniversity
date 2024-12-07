namespace UniversityApi.Features.Clases.Queries.Get;

public record struct GetClasesQueryResponse(
    int MateriaId,
    int ProfesorId,
    string Titulo,
    string Profesor);

namespace UniversityApi.Features.Materias.Queries.Get;

public record struct GetMateriasQueryResponse(
    int MateriaId,
    int ProfesorId,
    string Titulo,
    byte Creditos,
    string Profesor);

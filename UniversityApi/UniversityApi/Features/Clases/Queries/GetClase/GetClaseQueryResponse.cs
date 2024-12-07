namespace UniversityApi.Features.Clases.Queries.GetClase;

public record struct GetClaseQueryResponse(
    int MateriaId,
    int ProfesorId,
    string Titulo,
    string Profesor,
    string[] Estudiantes);

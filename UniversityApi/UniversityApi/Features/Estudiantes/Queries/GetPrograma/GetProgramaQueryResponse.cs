namespace UniversityApi.Features.Estudiantes.Queries.GetPrograma;

public record struct GetProgramaQueryResponse(
    string Especializacion,
    string Periodo,
    byte Creditos);

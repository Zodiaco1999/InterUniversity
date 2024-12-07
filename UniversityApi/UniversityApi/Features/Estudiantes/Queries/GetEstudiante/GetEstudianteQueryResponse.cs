namespace UniversityApi.Features.Estudiantes.Queries.GetEstudiante;

public record struct GetEstudianteQueryResponse(
    int EstudianteId,
    string NumeroIdentificacion,
    string Nombres,
    string Apellidos,
    byte Creditos,
    DateTime FechaNacimiento,
    DateTime FechaInscrito);

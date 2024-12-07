namespace UniversityApi.Features.Estudiantes.Queries.Get;

public record struct GetEstudiantesQueryResponse(
    string NumeroIdentificacion,
    string Nombres,
    string Apellidos,
    DateTime FechaInscrito);

using MediatR;

namespace UniversityApi.Features.Clases.Commands.Create;

public record struct CreateClaseCommand(IEnumerable<ClaseDto> Clases) : IRequest;
public record struct ClaseDto(
    int MateriaId,
    int ProfesorId);

using AutoMapper;
using MediatR;
using UniversityApi.Common.ContextAccesor;
using UniversityApi.Common.Exceptions;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Clases.Commands.Create;
public class CreateClaseCommandHandler : IRequestHandler<CreateClaseCommand>
{
    private readonly UniversidadContext _context;
    private readonly IContextAccessor _contextAccessor;

    public CreateClaseCommandHandler(UniversidadContext context, IContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task Handle(CreateClaseCommand request, CancellationToken cancellationToken)
    {
        var idsMaterias = request.Clases.Select(c => c.MateriaId);
        var creditosMaterias = _context.Materias.Where(m => idsMaterias.Contains(m.MateriaId)).Sum(m => m.Creditos);

        var estudiante = await _context.Estudiantes.FindAsync(int.Parse(_contextAccessor.UserId)) ?? new Estudiante();

        if (creditosMaterias > estudiante.Creditos)
            throw new ValidationException("No cuenta con los creditos suficientes para registrar materias");

        var clases = request.Clases.Select(c => new Clase
        {
            ProfesorId = c.ProfesorId,
            MateriaId = c.MateriaId,
            EstudianteId = int.Parse(_contextAccessor.UserId)
        }).ToList();

        estudiante.Creditos -= (byte)creditosMaterias;

        _context.Clases.AddRange(clases);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
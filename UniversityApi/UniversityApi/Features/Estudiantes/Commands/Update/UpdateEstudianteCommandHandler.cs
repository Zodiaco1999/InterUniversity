using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Common.ContextAccesor;
using UniversityApi.Common.Exceptions;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Estudiantes.Commands.Update;
public class UpdateEstudianteCommandHandler : IRequestHandler<UpdateEstudianteCommand>
{
    private readonly UniversidadContext _context;
    private readonly IContextAccessor _contextAccessor;

    public UpdateEstudianteCommandHandler(UniversidadContext context, IContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task Handle(UpdateEstudianteCommand request, CancellationToken cancellationToken)
    {
        if (request.EstudianteId != int.Parse(_contextAccessor.UserId))
            throw new ValidationException("No se pudo realizar la operación");

        if (_context.Usuarios.Any(u => u.NumeroIdentificacion == request.NumeroIdentificacion && u.UsuarioId != request.EstudianteId))
            throw new ValidationException("El número de identificación no es valido debido a que ya esta registrado");

        var estudiante = await _context.Usuarios
            .Where(u => u.Estudiante!.EstudianteId == request.EstudianteId)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Estudiante), request.EstudianteId);

        estudiante.NumeroIdentificacion = request.NumeroIdentificacion;
        estudiante.Nombres = request.Nombres;
        estudiante.Apellidos = request.Apellidos;
        estudiante.FechaNacimiento = request.FechaNacimiento;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
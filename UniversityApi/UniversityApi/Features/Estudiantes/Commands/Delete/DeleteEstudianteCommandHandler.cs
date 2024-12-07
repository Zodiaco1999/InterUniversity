using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Common.ContextAccesor;
using UniversityApi.Common.Exceptions;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Estudiantes.Commands.Delete;
public class DeleteEstudianteCommandHandler : IRequestHandler<DeleteEstudianteCommand>
{
    private readonly UniversidadContext _context;
    private readonly IContextAccessor _contextAccessor;

    public DeleteEstudianteCommandHandler(UniversidadContext context, IContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task Handle(DeleteEstudianteCommand request, CancellationToken cancellationToken)
    {
        if (request.EstudianteId != int.Parse(_contextAccessor.UserId))
            throw new ValidationException("No se pudo realizar la operación");

        var estudiante = await _context.Usuarios
            .Where(u => u.Estudiante!.EstudianteId == request.EstudianteId)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Estudiante), request.EstudianteId);

        _context.Usuarios.Remove(estudiante);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
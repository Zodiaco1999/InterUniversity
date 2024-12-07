using MediatR;
using UniversityApi.Common.ContextAccesor;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Estudiantes.Commands.Create;
public class CreateEstudianteCommandHandler : IRequestHandler<CreateEstudianteCommand>
{
    private readonly UniversidadContext _context;
    private readonly IContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;

    public CreateEstudianteCommandHandler(UniversidadContext context, IContextAccessor contextAccessor, IConfiguration configuration)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _configuration = configuration;
    }

    public async Task Handle(CreateEstudianteCommand request, CancellationToken cancellationToken)
    {
        var idCredito = _configuration.GetValue<int>("Parametros:CreditoIdEstudianteNuevo");

        var credito = await _context.Creditos.FindAsync(idCredito) ?? new Credito();

        var estudiante = new Estudiante
        {
            EstudianteId = int.Parse(_contextAccessor.UserId),
            FechaInscrito = DateTime.Now,
            Creditos = credito.Creditos
        };

        _context.Add(estudiante);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
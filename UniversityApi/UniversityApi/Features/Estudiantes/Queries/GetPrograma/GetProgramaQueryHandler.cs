using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UniversityApi.Common.ContextAccesor;
using UniversityApi.Common.Exceptions;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Estudiantes.Queries.GetPrograma;

public class GetProgramaQueryHandler : IRequestHandler<GetProgramaQuery, GetProgramaQueryResponse>
{
    private readonly UniversidadContext _context;
    private readonly IContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;

    public GetProgramaQueryHandler(UniversidadContext context, IContextAccessor contextAccessor, IConfiguration configuration)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _configuration = configuration;
    }

    public async Task<GetProgramaQueryResponse> Handle(GetProgramaQuery request, CancellationToken cancellationToken)
    {
        if (_context.Estudiantes.Any(e => e.EstudianteId == int.Parse(_contextAccessor.UserId)))
            throw new ValidationException("El estudiante ya esta inscrito");

        var idCredito = _configuration.GetValue<int>("Parametros:CreditoIdEstudianteNuevo");

        var credito = await _context.Creditos.FindAsync(idCredito) ?? new Credito();

        return new GetProgramaQueryResponse(
            "Ingeniera de sitemas",
            $"Primer semestre de pregrado {DateTime.Now.Year}",
            credito.Creditos);
    }
}
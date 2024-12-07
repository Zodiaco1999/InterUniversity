using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;
using UniversityApi.Common.Exceptions;

namespace UniversityApi.Features.Estudiantes.Queries.GetEstudiante;

public class GetEstudianteQueryHandler : IRequestHandler<GetEstudianteQuery, GetEstudianteQueryResponse>
{
    private readonly UniversidadContext _context;
    private readonly IMapper _mapper;

    public GetEstudianteQueryHandler(UniversidadContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<GetEstudianteQueryResponse> Handle(GetEstudianteQuery request, CancellationToken cancellationToken)
    {
        var estudiante = await _context.Estudiantes
            .Include(u => u.EstudianteNavigation)
            .FirstOrDefaultAsync(u => u.EstudianteId == request.EstudianteId) ?? throw new NotFoundException(nameof(Estudiante), request.EstudianteId);

        return _mapper.Map<GetEstudianteQueryResponse>(estudiante);
    }
}

public class GetEstudianteQueryProfile : Profile
{
    public GetEstudianteQueryProfile() =>
        CreateMap<Estudiante, GetEstudianteQueryResponse>()
            .ForMember(dest =>
                dest.NumeroIdentificacion,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.NumeroIdentificacion))
            .ForMember(dest =>
                dest.Nombres,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Nombres))
            .ForMember(dest =>
                dest.Apellidos,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Apellidos))
            .ForMember(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.FechaNacimiento));

}
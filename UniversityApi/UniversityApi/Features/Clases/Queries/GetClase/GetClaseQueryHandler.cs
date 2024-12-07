using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Common.Exceptions;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Clases.Queries.GetClase;

public class GetClaseQueryHandler : IRequestHandler<GetClaseQuery, GetClaseQueryResponse>
{
    private readonly UniversidadContext _context;
    private readonly IMapper _mapper;

    public GetClaseQueryHandler(UniversidadContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<GetClaseQueryResponse> Handle(GetClaseQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.MateriaProfesors
            .Where(m => m.MateriaId == request.MateriaId && m.ProfesorId == request.ProfesorId)
            .ProjectTo<GetClaseQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        response.Estudiantes = _context.Clases
            .Where(m => m.MateriaId == request.MateriaId && m.ProfesorId == request.ProfesorId)
            .Select(m => $"{m.Estudiante.EstudianteNavigation.Nombres} {m.Estudiante.EstudianteNavigation.Apellidos}")
            .ToArray();

        return response;
    }
}

public class GetClaseQueryProfile : Profile
{
    public GetClaseQueryProfile() =>
        CreateMap<MateriaProfesor, GetClaseQueryResponse>()
             .ForMember(dest =>
                dest.Titulo,
                opt => opt.MapFrom(mf => mf.Materia.Titulo))
            .ForMember(dest =>
                dest.Profesor,
                opt => opt.MapFrom(mf => $"{mf.Profesor.ProfesorNavigation.Nombres} {mf.Profesor.ProfesorNavigation.Apellidos}"));
}
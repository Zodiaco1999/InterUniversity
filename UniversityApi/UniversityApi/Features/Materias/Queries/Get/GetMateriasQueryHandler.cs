using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Materias.Queries.Get;

public class GetMateriasQueryHandler : IRequestHandler<GetMateriasQuery, IEnumerable<GetMateriasQueryResponse>>
{
    private readonly UniversidadContext _context;
    private readonly IMapper _mapper;

    public GetMateriasQueryHandler(UniversidadContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<IEnumerable<GetMateriasQueryResponse>> Handle(GetMateriasQuery request, CancellationToken cancellationToken)
    {
        return await _context.MateriaProfesors
            .OrderBy(e => e.Materia.Titulo)
            .ProjectTo<GetMateriasQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

public class GetMateriasQueryProfile : Profile
{
    public GetMateriasQueryProfile() =>
        CreateMap<MateriaProfesor, GetMateriasQueryResponse>()
             .ForMember(dest =>
                dest.Titulo,
                opt => opt.MapFrom(mf => mf.Materia.Titulo))
             .ForMember(dest =>
                dest.Creditos,
                opt => opt.MapFrom(mf => mf.Materia.Creditos))
            .ForMember(dest =>
                dest.Profesor,
                opt => opt.MapFrom(mf => $"{mf.Profesor.ProfesorNavigation.Nombres} {mf.Profesor.ProfesorNavigation.Apellidos}"));
}
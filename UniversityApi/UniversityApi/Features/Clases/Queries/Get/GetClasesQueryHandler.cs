using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Clases.Queries.Get;

public class GetClasesQueryHandler : IRequestHandler<GetClasesQuery, IEnumerable<GetClasesQueryResponse>>
{
    private readonly UniversidadContext _context;
    private readonly IMapper _mapper;

    public GetClasesQueryHandler(UniversidadContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<IEnumerable<GetClasesQueryResponse>> Handle(GetClasesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Clases
            .Where(c => c.EstudianteId == request.EstudianteId)
            .ProjectTo<GetClasesQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

public class GetClasesQueryProfile : Profile
{
    public GetClasesQueryProfile() =>
        CreateMap<Clase, GetClasesQueryResponse>()
             .ForMember(dest =>
                dest.Titulo,
                opt => opt.MapFrom(mf => mf.MateriaProfesor.Materia.Titulo))
            .ForMember(dest =>
                dest.Profesor,
                opt => opt.MapFrom(mf => $"{mf.MateriaProfesor.Profesor.ProfesorNavigation.Nombres} {mf.MateriaProfesor.Profesor.ProfesorNavigation.Apellidos}"));
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using UniversityApi.Common.Extensions;
using UniversityApi.Common.Models;
using UniversityApi.DataAccess;
using UniversityApi.DataAccess.Context;

namespace UniversityApi.Features.Estudiantes.Queries.Get;

public class GetEstudiantesQueryHandler : IRequestHandler<GetEstudiantesQuery, PagedResult<GetEstudiantesQueryResponse>>
{
    private readonly UniversidadContext _context;
    private readonly IMapper _mapper;

    public GetEstudiantesQueryHandler(UniversidadContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<PagedResult<GetEstudiantesQueryResponse>> Handle(GetEstudiantesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Estudiantes
            .OrderBy(e => e.EstudianteNavigation.Nombres)
            .ProjectTo<GetEstudiantesQueryResponse>(_mapper.ConfigurationProvider)
            .GetPagedResultAsync(request.Query.PageSize, request.Query.CurrentPage);
    }
}

public class GetEstudiantesQueryProfile : Profile
{
    public GetEstudiantesQueryProfile() =>
        CreateMap<Estudiante, GetEstudiantesQueryResponse>()
            .ForMember(dest =>
                dest.NumeroIdentificacion,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.NumeroIdentificacion))
            .ForMember(dest =>
                dest.Nombres,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Nombres))
            .ForMember(dest =>
                dest.Apellidos,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Apellidos));
}
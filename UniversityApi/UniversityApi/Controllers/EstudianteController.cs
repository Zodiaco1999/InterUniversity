using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Common.Models;
using UniversityApi.Features.Estudiantes.Commands.Create;
using UniversityApi.Features.Estudiantes.Commands.Delete;
using UniversityApi.Features.Estudiantes.Commands.Update;
using UniversityApi.Features.Estudiantes.Queries.Get;
using UniversityApi.Features.Estudiantes.Queries.GetEstudiante;
using UniversityApi.Features.Estudiantes.Queries.GetPrograma;

namespace UniversityApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EstudianteController : ControllerBase
{
    private readonly IMediator _mediator;

    public EstudianteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task CreateEstudiante(CreateEstudianteCommand command)
        => _mediator.Send(command);

    [HttpPut]
    public Task UpdateEstudiante(UpdateEstudianteCommand command)
        => _mediator.Send(command);

    [HttpGet]
    public Task<PagedResult<GetEstudiantesQueryResponse>> Get([FromQuery] GetEntityQuery query)
        => _mediator.Send(new GetEstudiantesQuery(query));

    [HttpGet("{id}")]
    public Task<GetEstudianteQueryResponse> GetEstudiante(int id)
       => _mediator.Send(new GetEstudianteQuery(id));

    [HttpGet("[action]")]
    public Task<GetProgramaQueryResponse> GetPrograma()
        => _mediator.Send(new GetProgramaQuery());

    [HttpDelete("{id}")]
    public Task DeleteEstudiante(int id)
      => _mediator.Send(new DeleteEstudianteCommand(id));
}

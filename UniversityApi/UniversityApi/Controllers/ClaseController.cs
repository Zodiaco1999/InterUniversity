using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Features.Clases.Commands.Create;
using UniversityApi.Features.Clases.Queries.Get;
using UniversityApi.Features.Clases.Queries.GetClase;

namespace UniversityApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task Post(IEnumerable<ClaseDto> clases)
        => _mediator.Send(new CreateClaseCommand(clases));

    [HttpGet("{id}")]
    public Task<IEnumerable<GetClasesQueryResponse>> Get(int id)
       => _mediator.Send(new GetClasesQuery(id));

    [HttpGet("{profesorId}/{materiaId}")]
    public Task<GetClaseQueryResponse> GetClase(int profesorId, int materiaId)
       => _mediator.Send(new GetClaseQuery(profesorId, materiaId));

}

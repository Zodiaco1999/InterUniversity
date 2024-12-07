using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Features.Materias.Queries.Get;

namespace UniversityApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MateriaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MateriaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<IEnumerable<GetMateriasQueryResponse>> Get()
        => _mediator.Send(new GetMateriasQuery());
}

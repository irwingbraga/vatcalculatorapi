namespace VatCalculator.Api.Controllers.Version1;

using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VatCalculator.Api.Routes.Version1;
using VatCalculator.Application.Features.CalculateVat;
using VatCalculator.Contracts.Calculation;

[Produces("application/json")]
[Consumes("application/json")]
[Route("api/v1/calculations"), ApiController]
public class CalculationsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CalculationsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route(ApiRoutes.VatCalculation.Vat)]
    [ProducesResponseType(typeof(CalculationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
    {
        var command = _mapper.Map<CalculateVatCommand>(request);

        var result = await _mediator.Send(command);

        return ReturnResult(result);
    }
}
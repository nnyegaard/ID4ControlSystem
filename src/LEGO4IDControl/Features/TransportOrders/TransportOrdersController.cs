using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LEGO4IDControl.Features.TransportOrders;

[ApiController]
[Route("/")]
public class TransportOrdersController : Controller
{
    private readonly IMediator _mediator;

    public TransportOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTransportOrder([FromBody] string id)
    {
        await _mediator.Send(new CreateTransportOrderCommand(id));

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetTransportOrder([FromQuery] string id)
    {
        var result = await _mediator.Send(new GetTransportOrderQuery(id));

        return Ok(result);
    }
}
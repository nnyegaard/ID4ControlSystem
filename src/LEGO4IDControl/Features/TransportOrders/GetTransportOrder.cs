using FluentValidation;
using LEGO4IDControl.Infrastructure;
using MediatR;

namespace LEGO4IDControl.Features.TransportOrders;

public record GetTransportOrderQuery(string Id) : IRequest<GetTransportOrderResult>;

public class GetTransportOrderQueryValidator : AbstractValidator<GetTransportOrderQuery>
{
    public GetTransportOrderQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public record GetTransportOrderResult(string Id, string Status);

public class GetTransportOrder : IRequestHandler<GetTransportOrderQuery, GetTransportOrderResult>
{
    public Task<GetTransportOrderResult> Handle(GetTransportOrderQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == "123")
        {
            return Task.Run(() => new GetTransportOrderResult("123", "Job in progress"), cancellationToken);
        }

        throw new NoOrderFoundException($"No order found with id {request.Id}");
    }
}



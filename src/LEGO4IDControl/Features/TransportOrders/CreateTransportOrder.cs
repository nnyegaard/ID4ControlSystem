using FluentValidation;
using MediatR;

namespace LEGO4IDControl.Features.TransportOrders;

public record CreateTransportOrderCommand(string Job) : IRequest<TransportOrderResult>;

public class CreateTransportOrderCommandValidator : AbstractValidator<CreateTransportOrderCommand>
{
    public CreateTransportOrderCommandValidator()
    {
        RuleFor(x => x.Job).NotEmpty();
    }
}

public record TransportOrderResult(string Id);

public class CreateTransportOrderHandler : IRequestHandler<CreateTransportOrderCommand, TransportOrderResult>
{
    public Task<TransportOrderResult> Handle(CreateTransportOrderCommand request, CancellationToken cancellationToken)
    {
        return Task.Run(() => new TransportOrderResult("123"), cancellationToken);
    }
}
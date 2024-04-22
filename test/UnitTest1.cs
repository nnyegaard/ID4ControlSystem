using FluentAssertions;
using LEGO4IDControl.Features.TransportOrders;
using LEGO4IDControl.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LEGO4IDControlTest;

public class UnitTest1
{
    [Fact]
    public async Task GetOneTransportOrderSuccessfully()
    {
        // Arrange
        var services = new ServiceCollection();
        var serviceProvider = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetTransportOrder).Assembly))
            .BuildServiceProvider();    
    
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var query = new GetTransportOrderQuery("123");
        
        // Act
        var response = await mediator.Send(query);

        // Assert
        response.Id.Should().Be("123");
    }
    
    [Fact]
    public Task GetOneTransportOrderFails()
    {
        // Arrange
        var services = new ServiceCollection();
        var serviceProvider = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetTransportOrder).Assembly))
            .BuildServiceProvider();    
    
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var query = new GetTransportOrderQuery("321");
        
        // Act
        Action act = () => mediator.Send(query);

        // Assert
        act.Should().Throw<NoOrderFoundException>();
        
        // TODO
        return Task.CompletedTask;
    }
}
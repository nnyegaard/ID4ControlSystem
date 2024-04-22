using System.Text;
using System.Text.Json;
using MediatR;
using MQTTnet;
using MQTTnet.Client;

namespace LEGO4IDControl.Features.AGVEvents;

public class MqttClientService : BackgroundService
{
    private readonly ILogger<MqttClientService> _logger;
    private readonly IMqttClient _client;
    private readonly MqttClientOptions _clientOptions;
    private readonly IMediator _mediator;

    public MqttClientService(ILogger<MqttClientService> logger, IMediator mediator)
    {
        _logger = logger;

        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();
        _clientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();

        _client.ApplicationMessageReceivedAsync += HandleMessageAsync;

        _mediator = mediator;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.ConnectAsync(_clientOptions, stoppingToken);
        await _client.SubscribeAsync("mqtt/test", cancellationToken: stoppingToken);
    }
    
    private async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var payload=Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
        _logger.LogInformation(payload);

        var agvEvent = JsonSerializer.Deserialize<AgvEvent>(payload);
        
        await _mediator.Send(agvEvent); //TODO: Create a handler for this
    }
}


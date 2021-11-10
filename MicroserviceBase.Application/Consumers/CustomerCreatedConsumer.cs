using MassTransit;
using MicroserviceBase.Domain.Events;
using Microsoft.Extensions.Logging;

namespace MicroserviceBase.Application.Consumers;

public class CustomerCreatedConsumer : IConsumer<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedConsumer> _logger;

    public CustomerCreatedConsumer(ILogger<CustomerCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        _logger.LogInformation(">>>>>>");
        _logger.LogDebug(context.Message.Nome);
        await Task.CompletedTask;
     }
}

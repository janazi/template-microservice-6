using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace MicroserviceBase.Application.Consumers
{
    public class CustomerCreatedConsumerDefinition : ConsumerDefinition<CustomerCreatedConsumer>
    {
        public CustomerCreatedConsumerDefinition()
        {
            var concurrentMessageLimit = Environment.ProcessorCount * 4;
#if DEBUG
            concurrentMessageLimit = 1;
#endif
            EndpointName = "customer-created-event";
            ConcurrentMessageLimit = concurrentMessageLimit;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CustomerCreatedConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r =>
            {
                r.Intervals(50, 100, 200);
            });

            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}

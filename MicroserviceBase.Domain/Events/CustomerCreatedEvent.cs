using MediatR;

namespace MicroserviceBase.Domain.Events;

public class CustomerCreatedEvent : INotification
{
    public string Description { get; set; }
}

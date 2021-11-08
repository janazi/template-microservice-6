using MediatR;

namespace MicroserviceBase.Domain.Events;

public class CustomerCreatedEvent : INotification
{
    public string Nome { get; }


    public CustomerCreatedEvent(string nome)
    {
        Nome = nome;
    }
}

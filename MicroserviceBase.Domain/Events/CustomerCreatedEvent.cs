namespace MicroserviceBase.Domain.Events;

public class CustomerCreatedEvent
{
    public string Nome { get; }


    public CustomerCreatedEvent(string nome)
    {
        Nome = nome;
    }
}

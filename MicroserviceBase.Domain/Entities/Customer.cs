using Flunt.Notifications;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Contracts;
using System;

namespace MicroserviceBase.Domain.Entities;

public class Customer : Notifiable<Notification>
{
    public Customer(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        Validate();
    }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }


    public void Patch(UpdateCustomerCommand command)
    {
        Nome = command.Nome;
        Validate();
    }


    private void Validate()
    {
        AddNotifications(new CustomerContract(this));
    }
}

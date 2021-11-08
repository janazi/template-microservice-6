using Flunt.Validations;
using MicroserviceBase.Domain.Entities;
using System;

namespace MicroserviceBase.Domain.Contracts;

public class CustomerContract : Contract<Customer>
{
    public CustomerContract(Customer c)
    {
        Requires()
            .IsNotNullOrEmpty(c.Nome, "Nome", "O campo nome deve ser preenchido");
        IsGreaterThan(c.DataNascimento, DateTime.Now.AddYears(-18), "Data Nascimento", "Customer menor de idade");
    }
}

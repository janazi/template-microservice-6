using Flunt.Validations;
using MicroserviceBase.Domain.Entities;

namespace MicroserviceBase.Domain.Contracts;

public class CustomerContract : Contract<Customer>
{
    public CustomerContract(Customer c)
    {
        Requires()
            .IsNotNullOrEmpty(c.Nome, "Nome", "O campo nome deve ser preenchido");

        IsGreaterThan(DateTime.Now.AddYears(-18),c.DataNascimento, "Data Nascimento", "Customer menor de idade");
    }
}

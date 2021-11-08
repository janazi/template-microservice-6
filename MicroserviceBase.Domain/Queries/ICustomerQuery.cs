using MicroserviceBase.Domain.Entities;

namespace MicroserviceBase.Domain.Queries;

public interface ICustomerQuery
{
    Task<Customer?> GetByCpf(string cnpj);
}

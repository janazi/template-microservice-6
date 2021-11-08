using MicroserviceBase.Domain.Entities;
using System.Threading.Tasks;

namespace MicroserviceBase.Domain.Queries;

public interface ICustomerQuery
{
    Task<Customer?> GetByCpf(string cnpj);
}

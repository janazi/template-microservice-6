using MicroserviceBase.Domain.Entities;
using MicroserviceBase.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceBase.Infrastructure.Data.Queries
{
    public class CustomerQuery : ICustomerQuery
    {
        private static readonly IList<Customer> customers = new List<Customer>();
        public Task<Customer?> GetByCpf(string cpf)
        {
            return Task.FromResult(customers.SingleOrDefault(c => c.CPF == cpf));
        }
    }
}

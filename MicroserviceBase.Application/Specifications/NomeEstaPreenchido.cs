using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands.Customers;

namespace MicroserviceBase.Application.Specifications
{
    public class NomeEstaPreenchido : ISpecification<CreateCustomerCommand>
    {
        public bool IsSatisfiedBy(CreateCustomerCommand c)
        {
            return !string.IsNullOrEmpty(c.Nome);
        }
    }
}

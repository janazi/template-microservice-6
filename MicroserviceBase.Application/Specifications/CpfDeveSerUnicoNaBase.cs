using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Queries;
using MicroserviceBase.Infra.Mvc;

namespace MicroserviceBase.Application.Specifications;

public class CpfDeveSerUnicoNaBase : ISpecification<CreateCustomerCommand>
{
    public bool IsSatisfiedBy(CreateCustomerCommand c)
    {
        var customerQuery = DependencyResolver.GetService<ICustomerQuery>();
        var existingCustomer = customerQuery?.GetByCpf(c.CPF);
        return existingCustomer is null;
    }
}

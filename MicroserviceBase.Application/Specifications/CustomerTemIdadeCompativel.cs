using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands.Customers;
using System;

namespace MicroserviceBase.Application.Specifications
{
    public class CustomerTemIdadeCompativel : ISpecification<CreateCustomerCommand>
    {
        public bool IsSatisfiedBy(CreateCustomerCommand command)
        {
            var dataNascimentoMaiorDeIdade = DateTime.Now.AddYears(-18);
            return command.DataNascimento < dataNascimentoMaiorDeIdade;
        }
    }
}

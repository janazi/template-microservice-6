using DomainValidationCore.Validation;
using MicroserviceBase.Application.Specifications;
using MicroserviceBase.Domain.Commands.Customers;

namespace MicroserviceBase.Application.Validations.Customers
{
    public class AddCustomerValidation : Validator<CreateCustomerCommand>
    {
        public AddCustomerValidation()
        {
            Add("NomeEstaPreenchido", new Rule<CreateCustomerCommand>(new NomeEstaPreenchido(), "Preencher o campo nome"));
            Add("CustomerTemIdadeCompativel", new Rule<CreateCustomerCommand>(new CustomerTemIdadeCompativel(), "Cliente de possuir mais de 18 anos"));
            Add("CpfDeveSerUnico", new Rule<CreateCustomerCommand>(new CpfDeveSerUnicoNaBase(), "CPF já cadastrado"));
        }
    }
}

using MediatR;
using MicroserviceBase.Domain.Entities;

namespace MicroserviceBase.Domain.Commands.Customers
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }

        public CreateCustomerCommand(string nome, DateTime dataNascimento, string cPF)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cPF;
        }
    }
}

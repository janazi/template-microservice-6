using System;

namespace MicroserviceBase.Domain.Commands.Customers
{
    public class UpdateCustomerCommand
    {
        public string Nome { get; init; }
        public DateTime DataNascimento { get; init; }
        public string CPF { get; init; }

        public UpdateCustomerCommand(string nome, DateTime dataNascimento, string cpf)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cpf;
        }
    }
}

﻿using System;
using MediatR;
using MicroserviceBase.Domain.Entities;

namespace MicroserviceBase.Domain.Commands.Customers
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}

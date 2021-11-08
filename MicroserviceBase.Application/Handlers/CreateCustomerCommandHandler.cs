﻿using Jnz.RedisRepository.Interfaces;
using MediatR;
using MicroserviceBase.Controllers.V1;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MicroserviceBase.Application.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly ILogger<CreateCustomerCommandHandler> _logger;
    private readonly IRedisRepository _redisRepository;

    public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IRedisRepository redisRepository)
    {
        this._logger = logger;
        _redisRepository = redisRepository;
    }
    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Entering Create Customer Handler");
        _logger.LogInformation(request.Nome);

        var naturalPerson = new NaturalPerson("1234", "Marcelo Janazi", new DateTime(1980, 10, 22), 'M');

        await _redisRepository.SetAsync(naturalPerson);

        //var c = await _redisRepository.GetAsync<NaturalPerson>("NaturalPerson", "27805028869");
        //if (c is not null) return c;

        var customer = new Customer(request.Nome, request.DataNascimento, request.CPF);
        if (customer.IsValid is false)
            return await Task.FromResult(customer);

        //TODO: INSERT
        return await Task.FromResult(customer);
    }
}
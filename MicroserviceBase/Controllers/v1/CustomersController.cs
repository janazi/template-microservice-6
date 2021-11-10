using Dapper;
using MassTransit;
using MediatR;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using MicroserviceBase.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MicroserviceBase.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;
        private readonly IPublishEndpoint _publisher;
        private readonly IConfiguration _configuration;

        public CustomersController(IMediator mediator, 
            ILogger<CustomersController> logger, 
            IPublishEndpoint publisher,
                IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;
            _publisher = publisher;
            _configuration = configuration;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IActionResult Patch([FromBody] UpdateCustomerCommand command, Guid id)
        {
            var customer = new Customer(command.Nome, command.DataNascimento, command.CPF);

            if (customer.IsValid is false)
                return BadRequest(customer);

            customer.Patch(command);

            if (customer.IsValid is false)
                return NoContent();

            return new AcceptedResult(nameof(Patch), customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            if (customer.IsValid is false)
                return BadRequest(customer.Notifications);

            await _publisher.Publish(new CustomerCreatedEvent(customer.Nome));
            return CreatedAtAction(nameof(Post), customer);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await using var connection = new MySqlConnection(_configuration.GetConnectionString(ConnectionStringName));

            var customers = await connection.QueryAsync<NaturalPerson>("Select * from NaturalPerson");

            return Ok();
        }

        private const string ConnectionStringName = "MariaDb";

    }
}

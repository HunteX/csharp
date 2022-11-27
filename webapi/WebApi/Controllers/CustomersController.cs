using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly DbManager _dbManager;

    public CustomersController(DbManager dbManager)
    {
        _dbManager = dbManager;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCustomerAsync([FromRoute] long id)
    {
        var customer = await _dbManager.GetCustomerByIdAsync(id);

        return customer != null ? Ok(customer) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer)
    {
        if (await _dbManager.GetCustomerByIdAsync(customer.Id) != null)
        {
            return Conflict();
        }

        var newCustomer = await _dbManager.CreateCustomerAsync(customer.Id, customer.Firstname, customer.Lastname);

        return Ok(newCustomer.Id);
    }
}
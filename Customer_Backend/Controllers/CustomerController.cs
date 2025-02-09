using CustomerBackend.Application.DTOs;
using CustomerBackend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(long id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetByCompanyId(long companyId)
        {
            var customers = await _customerService.GetCustomersByCompanyIdAsync(companyId);
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create([FromBody] CreateCustomerDTO customerDto)
        {
            var result = await _customerService.CreateCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDTO>> Update(long id, [FromBody] UpdateCustomerDTO customerDto)
        {
            if (id != customerDto.Id)
                return BadRequest();

            var result = await _customerService.UpdateCustomerAsync(customerDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

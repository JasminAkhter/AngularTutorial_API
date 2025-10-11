using AngularTutorial_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularTutorial_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();

                var result = customers.Select(x => new CustomerDto
                {
                    ID = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    Gender = x.Gender,
                    Address = x.Address
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                    return NotFound(new { Message = "Customer not found." });

                var result = new CustomerDto
                {
                    ID = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Gender = customer.Gender,
                    Address = customer.Address
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDto model)
        {
            await _context.Database.BeginTransactionAsync();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Customer entity = new Customer();
                entity.Name = model.Name;
                entity.Email = model.Email;
                entity.Phone = model.Phone;
                entity.Email = model.Email;
                entity.Gender = model.Gender;
                entity.Address = model.Address;

                await _context.Customers.AddAsync(entity);
                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();

                return Ok("Data Save Successfully!!");
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine("Error: ", ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDto model)
        {
            await _context.Database.BeginTransactionAsync();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingCustomer = await _context.Customers.FindAsync(id);

                if (existingCustomer == null)
                    return NotFound(new { Message = "Customer not found" });

                existingCustomer.Name = model.Name;
                existingCustomer.Email = model.Email;
                existingCustomer.Phone = model.Phone;
                existingCustomer.Gender = model.Gender;
                existingCustomer.Address = model.Address;

                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok(new {Customer = existingCustomer, Message = "Customer updated successfully!" });
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine("Error: ", ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.Database.BeginTransactionAsync();
            
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                    return NotFound(new { Message = "Customer not found" });

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok(new { Message = "Customer deleted successfully!" });
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine("Error: ", ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}

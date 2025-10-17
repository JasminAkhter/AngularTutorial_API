using AngularTutorial_API.DTOs;
using AngularTutorial_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularTutorial_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BankController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var banks = await _context.Banks.ToListAsync();

                var result = banks.Select(x => new BankDto
                {
                    BankID = x.BankID,
                    BankName = x.BankName,
                    AccountNumber = x.AccountNumber,
                    AccountType = x.AccountType,
                    BankAddress = x.BankAddress
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var bank = await _context.Banks.FindAsync(id);

                if (bank == null)
                    return NotFound(new { Message = "Bank not found." });

                var result = new BankDto
                {
                    BankID = bank.BankID,
                    BankName = bank.BankName,
                    AccountNumber = bank.AccountNumber,
                    AccountType = bank.AccountType,
                    BankAddress = bank.BankAddress
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BankDto model)
        {
            await _context.Database.BeginTransactionAsync();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Bank entity = new Bank
                {
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                    AccountType = model.AccountType,
                    BankAddress = model.BankAddress
                };

                await _context.Banks.AddAsync(entity);
                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();

                return Ok(new { Bank = entity, Message = "Bank created successfully!" });
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BankDto model)
        {
            await _context.Database.BeginTransactionAsync();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingBank = await _context.Banks.FindAsync(id);

                if (existingBank == null)
                    return NotFound(new { Message = "Bank not found." });

                existingBank.BankName = model.BankName;
                existingBank.AccountNumber = model.AccountNumber;
                existingBank.AccountType = model.AccountType;
                existingBank.BankAddress = model.BankAddress;

                _context.Banks.Update(existingBank);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok(new { Bank = existingBank, Message = "Bank updated successfully!" });
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.Database.BeginTransactionAsync();
            try
            {
                var bank = await _context.Banks.FindAsync(id);
                if (bank == null)
                    return NotFound(new { Message = "Bank not found." });

                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok(new { Message = "Bank deleted successfully!" });
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine("Error: " + ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
    
}

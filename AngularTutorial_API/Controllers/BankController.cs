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
                var banks = await _context.Banks.Include(x => x.Branches).ToListAsync();

                var result = banks.Select(x => new BankDto
                {
                    BankID = x.BankID,
                    BankName = x.BankName,
                    AccountNumber = x.AccountNumber,
                    AccountType = x.AccountType,
                    BankAddress = x.BankAddress,
                    Branchs = x.Branches.Select(b => new BranchDto
                    {
                        BranchID = b.BranchID,
                        BranchName = b.BranchName,
                        Phone = b.Phone,
                        Email = b.Email,
                        Address = b.Address,
                        BankID = b.BankID
                    }).ToList()
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
                var bank = await _context.Banks
                    .Include(x => x.Branches)
                    .SingleOrDefaultAsync(x => x.BankID == id);

                if (bank == null)
                    return NotFound(new { Message = "Bank not found." });

                var result = new BankDto
                {
                    BankID = bank.BankID,
                    BankName = bank.BankName,
                    AccountNumber = bank.AccountNumber,
                    AccountType = bank.AccountType,
                    BankAddress = bank.BankAddress,
                    Branchs = bank.Branches.Select(b => new BranchDto
                    {
                        BranchID = b.BranchID,
                        BranchName = b.BranchName,
                        Phone = b.Phone,
                        Email = b.Email,
                        Address = b.Address,
                        BankID = b.BankID
                    }). ToList()
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

                Bank entity = new Bank();
                entity.BankName = model.BankName;
                entity.AccountNumber = model.AccountNumber;
                entity.AccountType = model.AccountType;
                entity.BankAddress = model.BankAddress;

                await _context.Banks.AddAsync(entity);
                await _context.SaveChangesAsync();


                foreach (var branch in model.Branchs)
                { 
                    Branch branchEntity = new Branch();
                    branchEntity.BankID = entity.BankID;
                    branchEntity.BranchName = branch.BranchName;
                    branchEntity.Phone = branch.Phone;
                    branchEntity.Email = branch.Email;
                    branchEntity.Address = branch.Address;

                    await _context.Branches.AddAsync(branchEntity);
                }
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

                var existingBank = await _context.Banks
                    .Include(b => b.Branches)
                    .FirstOrDefaultAsync(b => b.BankID == id);

                if (existingBank == null)
                    return NotFound(new { Message = "Bank not found." });

                existingBank.BankName = model.BankName;
                existingBank.AccountNumber = model.AccountNumber;
                existingBank.AccountType = model.AccountType;
                existingBank.BankAddress = model.BankAddress;

                if(model.Branchs != null && model.Branchs.Any())
                { 
                    foreach (var branchDto in model.Branchs)
                    {
                        var existingBranch = existingBank.Branches
                            .FirstOrDefault(b => b.BranchID == branchDto.BranchID);

                        if (existingBranch != null)
                        {
                            existingBranch.BranchName = branchDto.BranchName;
                            existingBranch.Phone = branchDto.Phone;
                            existingBranch.Email = branchDto.Email;
                            existingBranch.Address = branchDto.Address;
                        }
                        else
                        {
                            var newBranch = new Branch
                            {
                                BankID = existingBank.BankID,
                                BranchName = branchDto.BranchName,
                                Phone = branchDto.Phone,
                                Email = branchDto.Email,
                                Address = branchDto.Address,
                            };
                            _context.Branches.Add(newBranch); 
                        }
                    }
                }

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
                var bank = await _context.Banks
                    .Include(b => b.Branches)
                    .FirstOrDefaultAsync(b => b.BankID == id);

                if (bank == null)
                    return NotFound(new { Message = "Bank data not found." });


                // Delete all branches first
                if (bank.Branches.Any())
                {
                    _context.Branches.RemoveRange(bank.Branches);
                }



                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok(new { Message = "Data deleted successfully!" });
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

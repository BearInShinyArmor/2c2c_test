using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2c2c_test;
using _2c2p_test.Models;

namespace _2c2c_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TransactionModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetTransaction()
        {
            return await _context.Transaction.ToListAsync();
        }

        // GET: api/TransactionModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> GetTransactionModel(int id)
        {
            var transactionModel = await _context.Transaction.FindAsync(id);

            if (transactionModel == null)
            {
                return NotFound();
            }

            return transactionModel;
        }

        // PUT: api/TransactionModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionModel(int id, TransactionModel transactionModel)
        {
            if (id != transactionModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TransactionModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionModel>> PostTransactionModel(TransactionModel transactionModel)
        {
            _context.Transaction.Add(transactionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionModel", new { id = transactionModel.Id }, transactionModel);
        }

        // DELETE: api/TransactionModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionModel(int id)
        {
            var transactionModel = await _context.Transaction.FindAsync(id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transactionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }
    }
}

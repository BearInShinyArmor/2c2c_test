using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using _2c2c_test.Models;
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

        // GET: api/Transaction
        [HttpGet]
        public ActionResult<IEnumerable<TransactionOutModel>> GetTransaction()
        {
            List<TransactionModel> transactionModel = _context.Transaction.ToList();
            List<TransactionOutModel> result = new List<TransactionOutModel>(transactionModel.Count);
            foreach(TransactionModel tr in transactionModel)
            {
                result.Add(new TransactionOutModel(tr));
            }
            return result;
        }

        // GET: api/Transaction/Currensy=USD
        [HttpGet("Currensy={currensy}")]
        public ActionResult<IEnumerable<TransactionOutModel>> GetTransactionModel(string currensy)
        {
            List<TransactionModel> transactionModel = _context.Transaction.Where(x => x.Currency == currensy).ToList();
                
            if (transactionModel == null)
            {
                return NotFound();
            }

            List<TransactionOutModel> result = new List<TransactionOutModel>(transactionModel.Count);
            foreach (TransactionModel tr in transactionModel)
            {
                result.Add(new TransactionOutModel(tr));
            }
            return result;
        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }
    }
}

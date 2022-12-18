using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using _2c2c_test.Models;
using _2c2p_test.Models;
using System;

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
        
        // GET: api/Transaction/Status=A
        [HttpGet("Status={status}")]
        public ActionResult<IEnumerable<TransactionOutModel>> GetTransactionModel(TransactionStatusEnum status)
        {
            List<TransactionModel> transactionModel = _context.Transaction.Where(x => x.TransactionStatus == status).ToList();

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

        // GET: api/Transaction/From=2019-01-23&To=2019-01-24
        [HttpGet("From={start}&To={end}")]
        public ActionResult<IEnumerable<TransactionOutModel>> GetTransactionModel(DateTime start, DateTime end)
        {
            if (end.Hour == 0 & end.Minute == 0 && end.Second == 00)
            {
                end = end.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            List<TransactionModel> transactionModel = _context.Transaction.Where(x => ((x.TransactionDate >= start) && (x.TransactionDate <= end))).ToList();

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

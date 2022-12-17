using _2c2p_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2c_test.Repositories
{
    public class TransactionRepository
    {
        private readonly AppDbContext context;

        public TransactionRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void SaveTransaction(TransactionModel transaction)
        {
            context.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
            
        }
    }
}

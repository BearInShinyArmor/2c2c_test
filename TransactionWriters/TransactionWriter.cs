using _2c2c_test.Repositories;
using _2c2p_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2c_test.TransactionWriters
{
    public class TransactionWriter : ITransactionWriter
    {
        private readonly TransactionRepository transactionRepository;

        public TransactionWriter(TransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public void WriteToDB(List<TransactionModel> transactions)
        {
            foreach(TransactionModel transaction in transactions)
            {
                transactionRepository.SaveTransaction(transaction);
            }
        }
    }
}

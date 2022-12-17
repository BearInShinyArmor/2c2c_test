using _2c2p_test.Models;
using System.Collections.Generic;

namespace _2c2c_test.TransactionWriters
{
    public interface ITransactionWriter
    {
        void WriteToDB(List<TransactionModel> transactions);
    }
}
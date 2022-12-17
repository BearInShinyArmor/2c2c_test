using System;

namespace _2c2p_test.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string InnerTransactionID { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatusEnum TransactionStatus { get; set; }

    }
    public enum TransactionStatusEnum
    {
        A, R, D
    }
}

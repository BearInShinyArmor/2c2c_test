using System;
using System.ComponentModel.DataAnnotations;

namespace _2c2p_test.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        [Required]
        public string InnerTransactionID { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public TransactionStatusEnum TransactionStatus { get; set; }

    }
    public enum TransactionStatusEnum
    {
        A, R, D
    }
}

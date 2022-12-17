using _2c2p_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2c_test.Models
{
    public class TransactionOutModel
    {
        public TransactionOutModel(string id, string payment, string status)
        {
            this.id = id;
            this.payment = payment;
            Status = status;
        }
        public TransactionOutModel(TransactionModel trM)
        {
            this.id = trM.InnerTransactionID;
            this.payment = trM.Amount+" "+trM.Currency;
            Status = trM.TransactionStatus.ToString();
        }

        public string id { get; set; }
        public string payment { get; set; }
        public string Status { get; set; }

    }
}

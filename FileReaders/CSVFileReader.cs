using _2c2p_test.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace _2c2p_test.FileReaders
{
    internal class CSVFileReader : IFileReader
    {
        public List<TransactionModel> ReadFile(out List<string> errors,IFormFile file)
        {
            errors = new List<string>();
            List<TransactionModel> result = new List<TransactionModel>();
            string str;
            var sr = new StreamReader(file.OpenReadStream());
            int i = 0;
            while ((str = sr.ReadLine()) != null)
            {
                i++;
                try
                {
                    var strs = str.Split(';');
                    foreach(string st in strs)
                    {
                        if (string.IsNullOrEmpty(st))
                        {
                            throw new Exception(" empty value");
                        }
                    }

                    TransactionModel tmp= new TransactionModel { 
                        InnerTransactionID = strs[0],
                        Amount = float.Parse(strs[1].Replace(",", "").Replace(".", ",")),
                        Currency = strs[2],
                        TransactionDate = DateTime.Parse(strs[3]),
                        TransactionStatus = CSVTransactionStatusToNormal(strs[4])

                    };
                    
                    result.Add(tmp);
                }
                catch (Exception e)
                {
                    errors.Add("an error occurred while processing line " + i+e.Message);
                }

            }
            return result;
        }

        private TransactionStatusEnum CSVTransactionStatusToNormal(string v)
        {
            switch (v)
            {
                case "Approved":
                    return TransactionStatusEnum.A;
                    break;
                case "Failed":
                    return TransactionStatusEnum.R;
                    break;
                case "Finished":
                    return TransactionStatusEnum.D;
                    break;
                default: throw new Exception();
            }
        }
    }
}
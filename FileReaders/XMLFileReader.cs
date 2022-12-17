using _2c2p_test.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace _2c2p_test.FileReaders
{
    internal class XMLFileReader : IFileReader
    {
        public List<TransactionModel> ReadFile(IFormFile file)
        {
            List<TransactionModel> result = new List<TransactionModel>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(new StreamReader(file.OpenReadStream()).ReadToEnd());
            var xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    string innerTransactionID = xnode.Attributes.GetNamedItem("id").Value;
                    DateTime transactionDate = DateTime.Parse(xnode.ChildNodes[0].InnerText);
                    float amount = float.Parse(xnode.ChildNodes[1].ChildNodes[0].InnerText.Replace(".",","));
                    string currency = xnode.ChildNodes[1].ChildNodes[1].InnerText;
                    TransactionStatusEnum transactionStatus = XMLTransactionStatusToNormal(xnode.ChildNodes[2].InnerText);
                    result.Add(new TransactionModel
                    {
                        InnerTransactionID = innerTransactionID,
                        Amount = amount,
                        Currency = currency,
                        TransactionDate = transactionDate,
                        TransactionStatus = transactionStatus
                    }
                       );
                }
            }
            return result;
        }
        private TransactionStatusEnum XMLTransactionStatusToNormal(string v)
        {
            switch (v)
            {
                case "Approved":
                    return TransactionStatusEnum.A;
                    break;
                case "Rejected":
                    return TransactionStatusEnum.R;
                    break;
                case "Done":
                    return TransactionStatusEnum.D;
                    break;
                default: throw new Exception();
            }
        }
    }
}
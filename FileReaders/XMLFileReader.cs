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
        public List<TransactionModel> ReadFile(out List<string> errors,IFormFile file)
        {
            errors = new List<string>();
            List<TransactionModel> result = new List<TransactionModel>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(new StreamReader(file.OpenReadStream()).ReadToEnd());
            var xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            { int i = 0;
                foreach (XmlElement xnode in xRoot)
                {
                    i++;
                    try
                    {
                        string innerTransactionID = xnode.Attributes.GetNamedItem("id").Value;
                        if (string.IsNullOrEmpty(innerTransactionID)) throw new Exception(" empty value");
                        DateTime transactionDate = DateTime.Parse(xnode.ChildNodes[0].InnerText);
                        float amount = float.Parse(xnode.ChildNodes[1].ChildNodes[0].InnerText.Replace(".", ","));
                        string currency = xnode.ChildNodes[1].ChildNodes[1].InnerText;
                        if (string.IsNullOrEmpty(currency)) throw new Exception(" empty value");
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
                    catch(Exception e)
                    {
                        errors.Add("an error occurred while processing " + i+ " entry"+e.Message);
                    }
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
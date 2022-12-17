using _2c2p_test.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace _2c2p_test.FileReaders

{
    public interface IFileReader
    {
        List<TransactionModel> ReadFile(out List<string> errors, IFormFile file);
    }
}
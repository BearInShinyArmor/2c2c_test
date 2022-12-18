using _2c2c_test.Models;
using _2c2c_test.Repositories;
using _2c2c_test.TransactionWriters;
using _2c2p_test.FileReaders;
using _2c2p_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2c_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TransactionRepository transactionRepository;
        public HomeController(ILogger<HomeController> logger, TransactionRepository transactionRepository)
        {
            _logger = logger;
            this.transactionRepository = transactionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [RequestSizeLimit(1048576)]
        public IActionResult AddFile(IFormFile file)
        {
            string extention = file.FileName.Split(".").Last();
            IFileReader fileReader;
            switch (extention)
            {
                case "csv":
                    fileReader = new CSVFileReader();
                    break;
                case "xml":
                    fileReader = new XMLFileReader();
                    break;
                default:
                    return Content("Unknown format");
            }
            List<string> errors = null;
            List<TransactionModel> transactions = fileReader.ReadFile(out errors ,file);
            if (errors.Count>0)
            {
                Response.StatusCode = 400;
                string error="";
                foreach(string er in errors)
                {
                    error += er+"; ";
                }
                return Content(error);
            }
            TransactionWriter transactionWriter = new TransactionWriter(transactionRepository);
            transactionWriter.WriteToDB(transactions);
            return StatusCode(200);
        }
    }
}

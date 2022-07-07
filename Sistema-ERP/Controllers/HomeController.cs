using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema_ERP.Filters;
using Sistema_ERP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_ERP.Controllers
{
    [PagUsuarioLogado]
    public class HomeController : Controller
    {       
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
    }
}

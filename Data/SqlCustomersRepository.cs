using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SqlCustomersRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

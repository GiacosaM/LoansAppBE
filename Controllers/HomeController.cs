using BE_LoansApp.DataAccess;
using BE_LoansApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BE_LoansApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ThingsContext _context;

        public HomeController(ThingsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View( await _context.Things.ToListAsync());
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
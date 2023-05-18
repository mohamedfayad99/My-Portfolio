using books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace books.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDBC _db;
        public HomeController(ApplicationDBC db)
        {
            _db = db;
        }
        public IActionResult Index(int id)
        {
            var res = new HomeModel()
            {
                owner = _db.Owners.SingleOrDefault(d => d.Id == 2),
                portfolioItems = _db.PortfolioItems.ToList(),
                Contactme = _db.ContactmeItems.Find(id)
                
            };
            return View(res);
        }

        [HttpPost,ActionName("Contact")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HomeModel model)
        {

                Contactme obj = new Contactme
                {
                    Name = model.Contactme.Name,
                    Email = model.Contactme.Email,
                    Phone = model.Contactme.Phone,
                    Messsage = model.Contactme.Messsage
                };
                _db.ContactmeItems.Add(obj);
                _db.SaveChanges();
                TempData["sending"] = "Sending Done ";
                return RedirectToAction(nameof(Index));

            return View(model.Contactme);
           // return RedirectToAction("Index");
        }

    }
}
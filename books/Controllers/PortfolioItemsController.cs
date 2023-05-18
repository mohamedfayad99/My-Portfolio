using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using books.Models;
namespace books.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly ApplicationDBC _context;
        private readonly IWebHostEnvironment _hosting;


        public PortfolioItemsController(ApplicationDBC context, IWebHostEnvironment hosting)
        {
            _context = context;
            this._hosting = hosting;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_context.PortfolioItems);
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(int? id)
        {


            if (id == null || _context.PortfolioItems == null)
            {
                return NotFound();
            }

            var portfolioItem = _context.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioItem model)
        {
            if (ModelState.IsValid)
            {

                if (model.File != null)
                {
                    string wwwroot = _hosting.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(model.File.FileName);
                    string extension = Path.GetExtension(model.File.FileName);
                    string path = Path.Combine(wwwroot + "/me/img/portfolio/", filename + extension);
                    using (var filestream = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        model.File.CopyTo(filestream);
                    }
                }

                PortfolioItem pi = new PortfolioItem
                {
                    ProjectNAme = model.ProjectNAme,
                    Description = model.Description,
                    ImageURL = model.File.FileName

                };
                _context.PortfolioItems.Add(pi);
                _context.SaveChanges();
                TempData["success"] = "PortFolioItem  Added";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
            //return RedirectToAction("Index");
        }

        //// GET: PortfolioItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.PortfolioItems == null)
            {
                return NotFound();
            }

            var portfolioItem = _context.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            PortfolioItem portfolio = new PortfolioItem

            {
                Id = portfolioItem.Id,
                ProjectNAme = portfolioItem.ProjectNAme,
                Description = portfolioItem.Description,
                ImageURL = portfolioItem.ImageURL

            };
            return View(portfolio);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PortfolioItem model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
                try
                {
                if (model.UpdateFile != null)
                {
                    string wwwroot = _hosting.WebRootPath;
                    string? filename = Path.GetFileNameWithoutExtension(model.UpdateFile.FileName);
                    string? extension = Path.GetExtension(model.UpdateFile.FileName);
                    string? upload = Path.Combine(wwwroot + "/me/img/portfolio/", filename + extension);
                    model.UpdateFile.CopyTo(new FileStream(upload, FileMode.OpenOrCreate));

                    PortfolioItem portfolio = new PortfolioItem
                    {
                        Id = model.Id,
                        ProjectNAme = model.ProjectNAme,
                        Description = model.Description,
                        ImageURL = model.UpdateFile.FileName
                    };
                    _context.PortfolioItems.Update(portfolio);
                    _context.SaveChanges();
                    TempData["success"] = "PortFolio Updated";
                }
                else
                {   
                    PortfolioItem obj=new PortfolioItem();
                    var y = _context.PortfolioItems.Find(model.Id);
                    if (!y.ProjectNAme.Equals(model.ProjectNAme) || !y.Description.Equals(model.Description))
                    {
                        y.Id = model.Id;
                        y.ProjectNAme = model.ProjectNAme;
                        y.Description = model.Description;
                        y.ImageURL = model.ImageURL;
                        _context.SaveChanges();
                        TempData["success"] = "PortFolio123 Updated";
                        return RedirectToAction(nameof(Index));
                    }
                }

            }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           // }
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.PortfolioItems == null)
            {
                return NotFound();
            }

            var portfolioItem = _context.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var potofolioobject = _context.PortfolioItems.SingleOrDefault(d => d.Id == id);
            if (potofolioobject == null)
            {
                return NotFound();
            }
            _context.PortfolioItems.Remove(potofolioobject);
            _context.SaveChanges();
            TempData["success"] = "PortFolioItem Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return (_context.PortfolioItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

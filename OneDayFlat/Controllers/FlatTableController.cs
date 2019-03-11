using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneDayFlat.Data;
using OneDayFlat.Models;

namespace OneDayFlat.Controllers
{
    public class FlatTableController : Controller
    {
        private readonly OneDayFlatContext _context;

        public FlatTableController(OneDayFlatContext context)
        {
            _context = context;
        }

        // GET: Flats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flat.ToListAsync());
        }

        // GET: Flats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Flat
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Flats/Create
        [Authorize(Roles="admin")]
        public IActionResult Create()
        {
            return View();
        }

        //private readonly IHostingEnvironment he;
        //public FlatTableController(IHostingEnvironment e)
        //{
        //    he = e;
        //}
        //[Authorize(Roles = "admin")]
        //public IActionResult ShowFields(string fulName, IFormFile pic)
        //{
        //    ViewData["fname"] = fulName;
        //    if (pic != null )
        //    {
        //        var fileName = Path.Combine(he.WebRootPath,Path.GetFileName(pic.FileName));
        //        pic.CopyTo(_context.Flat.);
        //        ViewData["fileLocation"] = fileName;
        //    }
           
        //    return View();
        //}




        // POST: Flats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Author")] Flat flats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flats);
        }

        // GET: Flats/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Flat.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Flats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author")] Flat flats)
        {
            if (id != flats.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(flats.RoomID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flats);
        }

        // GET: Flats/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Flat
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Flats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Flat.FindAsync(id);
            _context.Flat.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Flat.Any(e => e.RoomID == id);
        }
    }
}

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
        IHostingEnvironment _appEnvironment;

        public FlatTableController(OneDayFlatContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
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
        public async Task<IActionResult> Create([Bind("Id,Name,City,OwnerName,PhoneNumber,Description")] Flat flats)
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

        public IActionResult ImageIndex()
        {
            return View(_context.Image.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                tblImage file = new tblImage { Name = uploadedFile.FileName, Path = path };
                _context.Image.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("ImageIndex");
        }
        public IActionResult CreateImage()
        {
            return View(_context.Flat.ToList());
        }

        [HttpPost]
        public IActionResult ImageCreate(FlatViewImage pvm)
        {
            Flat flat = new Flat { City = pvm.City };
            if (pvm.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(pvm.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)pvm.Image.Length);
                }
                // установка массива байтов
                flat.Image = imageData;
            }
            _context.Flat.Add(flat);
            _context.SaveChanges();

            return RedirectToAction("ImageCreate");
        }
    }
}


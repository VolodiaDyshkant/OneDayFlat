using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneDayFlat.Data;
using OneDayFlat.Models;

namespace OneDayFlat.Controllers
{
    public class FlatTableViewController : Controller
    {
        public readonly OneDayFlatContext _context;

        public FlatTableViewController(OneDayFlatContext context)
        {
            _context = context;
        }

        // GET: FlatTableView
        public async Task<IActionResult> Index()
        {
            var flatTableContext = _context.Flat.Include(f => f.Days);
            return View(await flatTableContext.ToListAsync());
        }

        // GET: FlatTableView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flat = await _context.Flat
                .Include(f => f.Days)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (flat == null)
            {
                return NotFound();
            }

            return View(flat);
        }

        // GET: FlatTableView/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            
            return View();
        }
        //public IActionResult Create()
        //{
        //    return View(_context.Flat.ToList());
        //}
        // POST: FlatTableView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(FlatViewImage pvm)
        {

            
            Flat flat = new Flat { OwnerName = pvm.OwnerName, PhoneNumber=pvm.PhoneNumber, Description=pvm.Description, Price=pvm.Price };
            
            if (pvm.Days == null)
            {
                List<Day> DaysList=new List<Day>();
                int a = 30;
                while (a > 0)
                {
                    DaysList.Add(new Day { Booked = false });
                    --a;
                }
                flat.Days = DaysList;
            }
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

            return View(flat);
        }

        //GET: FlatTableView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flat = await _context.Flat.FindAsync(id);
            if (flat == null)
            {
                return NotFound();
            }

            return View(flat);
        }

        // POST: FlatTableView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Edit(int? id, FlatViewImage pvm)
        {
            Flat flat = await _context.Flat.FindAsync(id);
            
            flat.OwnerName = pvm.OwnerName;
            flat.PhoneNumber = pvm.PhoneNumber;
            flat.Description = pvm.Description;
            flat.Price = pvm.Price;
           
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
            if (id != null)
            {
                _context.Flat.Update(flat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                //flat = await _context.Flat.FirstOrDefaultAsync(p => p.RoomID == id);
                //if (flat != null)
                //    return View(flat);
            }
            return NotFound();
        }
      


        // GET: FlatTableView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flat = await _context.Flat
                .Include(f => f.Days)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (flat == null)
            {
                return NotFound();
            }

            return View(flat);
        }

        // POST: FlatTableView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flat = await _context.Flat.FindAsync(id);
            _context.Flat.Remove(flat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlatExists(int id)
        {
            return _context.Flat.Any(e => e.RoomID == id);
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            User user = await _context.User.FindAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}

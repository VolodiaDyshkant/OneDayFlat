using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneDayFlat.Data;
using OneDayFlat.Models;

namespace OneDayFlat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly OneDayFlatContext _context;

        public DaysController(OneDayFlatContext context)
        {
            _context = context;
        }

        // GET: api/Days
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Day>>> GetDay()
        {
            return await _context.Day.ToListAsync();
        }
        
        // GET: api/Days/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Day>> GetDay(int id)
        {
            var day = await _context.Day.FindAsync(id);

            if (day == null)
            {
                return NotFound();
            }

            return day;
        }

        // PUT: api/Days/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDay(int id, Day day)
        {
            if (id != day.DayID)
            {
                return BadRequest();
            }

            _context.Entry(day).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Days
        [HttpPost]
        public async Task<IActionResult> PostDay(DateTime date, int id)
        {

            var flat = await _context.Flat.FindAsync(id);
            foreach(var item in flat.Days)
            {
                if(item.Date==date)
                {
                    item.Booked = true;
                }
            }
            _context.Flat.Update(flat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","FlatTableView");
            
        }

        // DELETE: api/Days/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Day>> DeleteDay(int id)
        {
            var day = await _context.Day.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            _context.Day.Remove(day);
            await _context.SaveChangesAsync();

            return day;
        }

        private bool DayExists(int id)
        {
            return _context.Day.Any(e => e.DayID == id);
        }
    }
}

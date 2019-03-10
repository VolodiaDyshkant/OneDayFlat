using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneDayFlat.Data;
using OneDayFlat.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OneDayFlat.Controllers
{
    [Route("api/[controller]")]
    public class APIDayController : Controller
    {
        // GET: api/<controller>
        OneDayFlatContext db;
        public APIDayController(OneDayFlatContext context)
        {
            this.db = context;
            if (!db.Day.Any())
            {
                db.Day.Add(new Day { Booked = true });
                db.Day.Add(new Day { Booked = false });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Day> Get()
        {
            return db.Day.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Day day = db.Day.FirstOrDefault(x => x.DayID == id);
            if(day==null)
            {
                return NotFound();
            }
            return new ObjectResult(day);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Day day)
        {
            if (day == null)
            {
                return BadRequest();
            }

            db.Day.Add(day);
            db.SaveChanges();
            return Ok(day);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Day day)
        {
            if (day == null)
            {
                return BadRequest();
            }
            if (!db.Day.Any(x => x.DayID == day.DayID))
            {
                return NotFound();
            }

            db.Update(day);
            db.SaveChanges();
            return Ok(day);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Day day = db.Day.FirstOrDefault(x => x.DayID == id);
            if (day == null)
            {
                return NotFound();
            }
            db.Day.Remove(day);
            db.SaveChanges();
            return Ok(day);
        }
    }
}

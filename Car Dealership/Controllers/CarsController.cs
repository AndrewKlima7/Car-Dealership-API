using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Car_Dealership.Models;

namespace Car_Dealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        DealershipContext dc = new DealershipContext();

        private readonly DealershipContext _context;

        public CarsController(DealershipContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpGet]
        [Route("m={make}")]
        public List<Car> SearchByMake(string make)
        {
            List<Car> cars = dc.Cars.Where(x => x.Make.ToLower() == make.ToLower()).ToList();

            return cars;
        }

        [HttpGet]
        [Route("md={model}")]
        public List<Car> SearchByModel(string model)
        {
            List<Car> cars = dc.Cars.Where(x => x.Model.ToLower() == model.ToLower()).ToList();

            return cars;
        }

        [HttpGet]
        [Route("y={year}")]
        public List<Car> SearchByYear(int year)
        {
            List<Car> cars = dc.Cars.Where(x => x.Year == year).ToList();

            return cars;
        }

        [HttpGet]
        [Route("c={color}")]
        public List<Car> SearchByColor(string color)
        {
            List<Car> cars = dc.Cars.Where(x => x.Color.ToLower() == color.ToLower()).ToList();

            return cars;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}

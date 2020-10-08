using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PlanetsController : ControllerBase
    {
        private readonly EducationAPIDbContext _context;
        public PlanetsController(EducationAPIDbContext context)
        {
            _context = context;
        }
        //Get planet list with endpoint api/planets
        [HttpGet]
        public async Task<ActionResult<List<Planets>>> GetPlanets()
        {
            var planets = await _context.Planets.ToListAsync();
            return planets;
        }
        //Get one planet question with endpong api/planets/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Planets>> GetPlanet(int id)
        {
            var planet = await _context.Planets.FindAsync(id);
            if (planet == null)
            {
                return NotFound();
            }
            else
            {
                return planet;
            }
        }
        // Post: add a planet question
        [HttpPost]
        public async Task<ActionResult<Planets>> AddPlanet(Planets newPlanet)
        {
            if (ModelState.IsValid)
            {
                _context.Planets.Add(newPlanet);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPlanet), new { id = newPlanet.QuestionId }, newPlanet);
            }

            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePlanet(Planets updatedPlanet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(updatedPlanet).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlanet(int id)
        {
            var planet = await _context.Planets.FindAsync(id);
            if (planet == null)
            {
                return NotFound();
            }
            else
            {
                _context.Planets.Remove(planet);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}

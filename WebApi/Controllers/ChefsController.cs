using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChefsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetChefs()
        {
            var chefs = _context.Chefs.ToList();
            return Ok(chefs);
        }

        [HttpGet("{id}")]
        public IActionResult GetChef(int id)
        {
            var chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            return Ok(chef);
        }

        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            _context.Chefs.Add(chef);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetChef), new { id = chef.Id }, chef);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateChef(int id, Chef chef)
        {
            var existingChef = _context.Chefs.FirstOrDefault(c => c.Id == id);
            if (existingChef == null)
            {
                return NotFound("Kategori bulunamadı.");
            }
            existingChef.NameSurname = chef.NameSurname;
            existingChef.Description = chef.Description;
            existingChef.ImageUrl = chef.ImageUrl;

            _context.Entry(existingChef).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok("Başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            _context.Chefs.Remove(chef);
            _context.SaveChanges();
            return Ok("Başarılı");
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Entidades;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class FavoriteBooksController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public FavoriteBooksController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteBooks
        [HttpGet]
        public IEnumerable<FavoriteBook> GetFavoriteBook()
        {
            return _context.FavoriteBook;
        }

        // GET: api/FavoriteBooks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavoriteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favoriteBook = await _context.FavoriteBook.FindAsync(id);

            if (favoriteBook == null)
            {
                return NotFound();
            }

            return Ok(favoriteBook);
        }

        // PUT: api/FavoriteBooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteBook([FromRoute] int id, [FromBody] FavoriteBook favoriteBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != favoriteBook.id)
            {
                return BadRequest();
            }

            _context.Entry(favoriteBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteBookExists(id))
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

        // POST: api/FavoriteBooks
        [HttpPost]
        public async Task<IActionResult> PostFavoriteBook([FromBody] FavoriteBook favoriteBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FavoriteBook.Add(favoriteBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoriteBook", new { id = favoriteBook.id }, favoriteBook);
        }

        // DELETE: api/FavoriteBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favoriteBook = await _context.FavoriteBook.FindAsync(id);
            if (favoriteBook == null)
            {
                return NotFound();
            }

            _context.FavoriteBook.Remove(favoriteBook);
            await _context.SaveChangesAsync();

            return Ok(favoriteBook);
        }

        private bool FavoriteBookExists(int id)
        {
            return _context.FavoriteBook.Any(e => e.id == id);
        }
    }
}
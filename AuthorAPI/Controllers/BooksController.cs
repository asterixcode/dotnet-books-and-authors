using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorAPI.DataAccess;
using AuthorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AuthorDbContext _dbContext;

        public BooksController(AuthorDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> ListAllBooks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                return _dbContext.Books.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                await _dbContext.Books.AddAsync(book);
                
                Author AuthorToAddNewBook = 
                    await _dbContext
                        .Authors
                        .Include(a => a.Books)
                        .FirstAsync(a => a.Id == id);
                
                AuthorToAddNewBook.Books.Add(book);
                
                _dbContext.Update(AuthorToAddNewBook);
                await _dbContext.SaveChangesAsync();
                
                return Created($"/{book.Isbn}", book);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{ISBN:int}")]
        public async Task<ActionResult<Book>> DeleteBook([FromRoute] int ISBN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                Book bookToDelete = _dbContext.Books.Find(ISBN);

                if (bookToDelete == null)
                {
                    return NotFound($"Book with ISBN = {ISBN} not found");
                }
                _dbContext.Books.Remove(bookToDelete);
                _dbContext.SaveChanges();

                return NoContent();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(500, e.Message);
            }
            
            return Ok();
        }
    }
}
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
    public class AuthorsController : ControllerBase
    {
        private AuthorDbContext authorDbContext;

        public AuthorsController(AuthorDbContext authorDbContext)
        {
            this.authorDbContext = authorDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> ListAuthorsAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                return await authorDbContext.Authors
                    .Include(b => b.Books)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthorAsync([FromBody] Author author)
        {
            try
            {
                authorDbContext.Authors.Add(author);
                await authorDbContext.SaveChangesAsync();
                return Created($"/{author.Id}", author);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
    }
}
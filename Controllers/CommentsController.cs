using ECommerceNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CommentsController : ControllerBase
    {

        private readonly AppDbContext context;
        public CommentsController(AppDbContext context)
        {
            this.context = context;

        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<List<Comments>>> Get()
        {
            try
            {
                return await context.Comments.ToListAsync();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> Get(int id)
        {
            try
            {
                var item = await context.Comments.FirstOrDefaultAsync(d => d.Id == id);
                return item ?? (ActionResult<Comments>)NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<ActionResult<Comments>> Post([FromBody] Comments comments)
        {
            try
            {
                if (!IsProdOrUserAvailable(comments))
                {
                    context.Comments.Add(comments);
                    await context.SaveChangesAsync();
                    return comments;
                }

                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Comments>> Put(int id, [FromBody] Comments comments)
        {
            try
            {
                var item = await context.Comments.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!IsProdOrUserAvailable(comments))
                {
                    item.Comment = comments.Comment;
                    item.Rating = comments.Rating;
                    item.ProductId = comments.ProductId;
                    item.UserId = comments.UserId;
                    await context.SaveChangesAsync();
                    return item;
                }

                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public class DeleteResponse
        {
            public int Id { get; set; }
            public bool Success { get; set; }
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResponse>> Delete(int id)
        {
            try
            {
                var item = await context.Comments.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.Comments.Remove(item);
                    await context.SaveChangesAsync();
                    return new DeleteResponse { Id = item.Id, Success = true };
                }

                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        private bool IsProdOrUserAvailable(Comments comment)
        {
            bool isProductOrUserAvail = false;
            if (comment.ProductId == 0 || comment.UserId == 0 || comment.Rating == 0)
            {
                isProductOrUserAvail = true;
            }

            return isProductOrUserAvail;
        }
    }
}
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
            //await initDatas();

            try
            {
                //return datas;
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
            //await initDatas();

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
            // initDatas();

            try
            {
                if (!string.IsNullOrEmpty(comments.Comment))
                {
                    //di.Id = await context.DataItems.MaxAsync(d => d.Id) + 1;
                    // ReSharper disable once MethodHasAsyncOverload
                    context.Comments.Add(comments);
                    await context.SaveChangesAsync();
                    //await updateDatas();

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
            //await initDatas();

            try
            {
                var item = await context.Comments.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(comments.Comment))
                {
                    item.Comment = comments.Comment;
                    await context.SaveChangesAsync();
                    //await updateDatas();
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
            //await initDatas();

            try
            {
                var item = await context.Comments.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.Comments.Remove(item);
                    await context.SaveChangesAsync();
                    //await updateDatas();
                    return new DeleteResponse { Id = item.Id, Success = true };
                }

                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
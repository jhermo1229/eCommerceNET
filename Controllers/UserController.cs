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

    public class UserController : ControllerBase
    {

        private readonly AppDbContext context;
        public UserController(AppDbContext context)
        {
            this.context = context;

        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            //await initDatas();

            try
            {
                //return datas;
                return await context.User.ToListAsync();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            //await initDatas();

            try
            {
                var item = await context.User.FirstOrDefaultAsync(d => d.Id == id);
                return item ?? (ActionResult<User>)NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            // initDatas();

            try
            {
                if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                {
                    //di.Id = await context.DataItems.MaxAsync(d => d.Id) + 1;
                    // ReSharper disable once MethodHasAsyncOverload
                    context.User.Add(user);
                    await context.SaveChangesAsync();
                    //await updateDatas();

                    return user;
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
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            //await initDatas();

            try
            {
                var item = await context.User.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                {
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
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
                var item = await context.User.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.User.Remove(item);
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
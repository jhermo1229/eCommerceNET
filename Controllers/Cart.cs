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

    public class CartController : ControllerBase
    {

        private readonly AppDbContext context;
        public CartController(AppDbContext context)
        {
            this.context = context;

        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<List<Cart>>> Get()
        {
            //await initDatas();

            try
            {
                //return datas;
                return await context.Cart.ToListAsync();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> Get(int id)
        {
            //await initDatas();

            try
            {
                var item = await context.Cart.FirstOrDefaultAsync(d => d.Id == id);
                return item ?? (ActionResult<Cart>)NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<ActionResult<Cart>> Post([FromBody] Cart carts)
        {
            // initDatas();

            try
            {
                //if (!string.IsNullOrEmpty(carts.ProductName) && !string.IsNullOrEmpty(carts.Description))
                //{
                    //di.Id = await context.DataItems.MaxAsync(d => d.Id) + 1;
                    // ReSharper disable once MethodHasAsyncOverload
                    context.Cart.Add(carts);
                    await context.SaveChangesAsync();
                    //await updateDatas();

                    return carts;
                //}

                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Cart>> Put(int id, [FromBody] Cart cart)
        {
            //await initDatas();

            try
            {
                var item = await context.Cart.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                //if (!string.IsNullOrEmpty(cart.ProductId) && !int.IsNullOrEmpty(cart.UserId))
                //{
                //    item.ProductName = cart.ProductName;
                //    item.Description = cart.Description;
                //    await context.SaveChangesAsync();
                //    //await updateDatas();
                //    return item;
                //}

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
                var item = await context.Cart.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.Cart.Remove(item);
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
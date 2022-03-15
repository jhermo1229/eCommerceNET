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

    public class ProductController : ControllerBase
    {

        private readonly AppDbContext context;
        public ProductController(AppDbContext context)
        {
            this.context = context;

        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<List<Products>>> Get()
        {
            //await initDatas();

            try
            {
                //return datas;
                return await context.Products.ToListAsync();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> Get(int id)
        {
            //await initDatas();

            try
            {
                var item = await context.Products.FirstOrDefaultAsync(d => d.Id == id);
                return item ?? (ActionResult<Products>)NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<ActionResult<Products>> Post([FromBody] Products products)
        {
            // initDatas();

            try
            {
                if (!string.IsNullOrEmpty(products.ProductName) && !string.IsNullOrEmpty(products.Description))
                {
                    //di.Id = await context.DataItems.MaxAsync(d => d.Id) + 1;
                    // ReSharper disable once MethodHasAsyncOverload
                    context.Products.Add(products);
                    await context.SaveChangesAsync();
                    //await updateDatas();

                    return products;
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
        public async Task<ActionResult<Products>> Put(int id, [FromBody] Products products)
        {
            //await initDatas();

            try
            {
                var item = await context.Products.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(products.ProductName) && !string.IsNullOrEmpty(products.Description))
                {
                    item.ProductName = products.ProductName;
                    item.Description = products.Description;
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
                var item = await context.Products.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.Products.Remove(item);
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
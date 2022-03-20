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

            try
            {
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

            try
            {
                if (!IsFieldEmpty(products))
                {
                    context.Products.Add(products);
                    await context.SaveChangesAsync();

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

            try
            {
                var item = await context.Products.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!IsFieldEmpty(products))
                {
                    item.ProductName = products.ProductName;
                    item.Description = products.Description;
                    item.Cost = products.Cost;
                    item.ImageUrl = products.ImageUrl;
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
                var item = await context.Products.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.Products.Remove(item);
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


        private bool IsFieldEmpty(Products product)
        {
            bool isEmptyOrNull = false;
            if (string.IsNullOrEmpty(product.ProductName) || string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.ImageUrl))
            {
                isEmptyOrNull = true;
            }

            return isEmptyOrNull;
        }
    }
}
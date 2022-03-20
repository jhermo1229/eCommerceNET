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

    public class PurchaseHistoryController : ControllerBase
    {

        private readonly AppDbContext context;
        public PurchaseHistoryController(AppDbContext context)
        {
            this.context = context;

        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<List<PurchaseHistory>>> Get()
        {
            try
            {
                return await context.PurchaseHistory.ToListAsync();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseHistory>> Get(int id)
        {
            try
            {
                var item = await context.PurchaseHistory.FirstOrDefaultAsync(d => d.Id == id);
                return item ?? (ActionResult<PurchaseHistory>)NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<ActionResult<PurchaseHistory>> Post([FromBody] PurchaseHistory purhist)
        {
            try
            {
                if (!IsCartOrUserAvailable(purhist))
                {
                    context.PurchaseHistory.Add(purhist);
                    await context.SaveChangesAsync();

                    return purhist;
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
        public async Task<ActionResult<PurchaseHistory>> Put(int id, [FromBody] PurchaseHistory purhist)
        {
            //await initDatas();

            try
            {
                var item = await context.PurchaseHistory.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!IsCartOrUserAvailable(purhist))
                {
                   item.PurchaseSuccess = purhist.PurchaseSuccess;
                    item.UserId = purhist.UserId;
                    item.CartId = purhist.CartId;
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
        {            try
            {
                var item = await context.PurchaseHistory.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.PurchaseHistory.Remove(item);
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

        private bool IsCartOrUserAvailable(PurchaseHistory purhist)
        {
            bool isCartOrUserAvail = false;
            if (purhist.CartId == 0 || purhist.UserId == 0)
            {
                isCartOrUserAvail = true;
            }

            return isCartOrUserAvail;
        }
    }
}
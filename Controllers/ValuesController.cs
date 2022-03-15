using ECommerceNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceNet.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {

        private readonly AppDbContext context;
        public ValuesController(AppDbContext context)
        {
            this.context = context;

        }

        /* example using sessions

         DO NOT USE THIS APPROACH IN YOUR ASSIGNMENT

        private async Task initDatas()
        {
            await HttpContext.Session.LoadAsync();
            var json = HttpContext.Session.GetString("datas");
            if (json == null)
            {
                datas = new List<DataItem> { new DataItem { Id = 1, String1 = "John", String2 = "Smith" } };
                json = JsonConvert.SerializeObject(datas);
                HttpContext.Session.SetString("datas", json);
                await HttpContext.Session.CommitAsync();
            }
            else
            {
                datas = JsonConvert.DeserializeObject<List<DataItem>>(json);
            }
        }

        private async Task updateDatas()
        {
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("datas", JsonConvert.SerializeObject(datas));
            await HttpContext.Session.CommitAsync();
        }
        */

        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<List<DataItem>>> Get()
        {
            //await initDatas();

            try
            {
                //return datas;
                return await context.DataItems.ToListAsync();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataItem>> Get(int id)
        {
            //await initDatas();

            try
            {
                var item = await context.DataItems.FirstOrDefaultAsync(d => d.Id == id);
                return item ?? (ActionResult<DataItem>)NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<ActionResult<DataItem>> Post([FromBody] DataItem di)
        {
            // initDatas();

            try
            {
                if (!string.IsNullOrEmpty(di.Data1) && !string.IsNullOrEmpty(di.Data2))
                {
                    //di.Id = await context.DataItems.MaxAsync(d => d.Id) + 1;
                    // ReSharper disable once MethodHasAsyncOverload
                    context.DataItems.Add(di);
                    await context.SaveChangesAsync();
                    //await updateDatas();

                    return di;
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
        public async Task<ActionResult<DataItem>> Put(int id, [FromBody] DataItem di)
        {
            //await initDatas();

            try
            {
                var item = await context.DataItems.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(di.Data1) && !string.IsNullOrEmpty(di.Data2))
                {
                    item.Data1 = di.Data1;
                    item.Data2 = di.Data2;
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
                var item = await context.DataItems.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.DataItems.Remove(item);
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
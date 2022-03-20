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

            try
            {
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
                if (!IsFieldEmpty(user))
                {
                    context.User.Add(user);
                    await context.SaveChangesAsync();
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
            try
            {
                var item = await context.User.FirstOrDefaultAsync(d => d.Id == id);

                if (item == null)
                    return NotFound();

                if (!IsFieldEmpty(user))
                {
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Email = user.Email;
                    item.Address = user.Address;
                    item.ShippingAddress = user.ShippingAddress;
                    item.MobileNumber = user.MobileNumber;
                    item.Username = user.Username;
                    item.Password = user.Password;
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
                var item = await context.User.FirstOrDefaultAsync(d => d.Id == id);

                if (item != null)
                {
                    context.User.Remove(item);
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

        private bool IsFieldEmpty(User user)
        {
            bool isEmptyOrNull = false;
            if(string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Address) || string.IsNullOrEmpty(user.ShippingAddress) || string.IsNullOrEmpty(user.MobileNumber)
                || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                isEmptyOrNull = true;
            }

            return isEmptyOrNull;
        }
    }
}
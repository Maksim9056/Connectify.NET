using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConnectify.NET.Data;
using APIConnectify.NET.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace APIConnectify.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly DB _context;

        public UsersControllers(DB context)
        {
            _context = context;
        }

        // GET: api/UsersControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users // Assuming Group is a direct navigation property      .Include(u => u.Friends)
                  .Include(u => u.Picture).ToListAsync();
        }

        // GET: api/UsersControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Users>>> GetUsers(int id)
        {
            List<Users> users1 = new List<Users>();
            var users = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            foreach (var user in users.Friends)
            {
                var users2 = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == id);
                users1.Add(users2);
            }
          

            return users1;
        }

        // GET: api/UsersControllers/5
        [HttpGet("group{id}")]
        public async Task<ActionResult<List<Group>>> GetUsersGroup(int id)
        {
            List<Group> users1 = new List<Group>();
            var users = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            foreach (var user in users.Group)
            {
                var users2 = await _context.Group.FirstOrDefaultAsync(u => u.Id == user);
                users1.Add(users2);
            }


            return users1;
        }


        // GET: api/UsersControllers/5
        [HttpGet("friend{id}")]
        public async Task<ActionResult<List<Users>>> GetFriends(int id)
        {
            List<Users> users1 = new List<Users>();

            var users = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            foreach (var user in users.Friends)
            {


                  var users3 = await _context.Friends.FirstOrDefaultAsync(u => u.Id == user);
               
                    var users2 = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == users3.UserId);
                    bool d = id == users2.Id;
                    if (d == true)
                    {
                        var us = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == users3.Friend);
                        users1.Add(us);
                    }
                    else
                    {
                        users1.Add(users2);

                    }
                
            }
            return users1;
        }

        // PUT: api/UsersControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost("POST")]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }
        // GET: api/Users/5
        [HttpGet("GETCheck/{mail},{password}")]
        public async Task<ActionResult<APIConnectify.NET.Models.Users>> GetUsersCheck(string mail, string password)
        {
            var answer = await _context.Users.Include(u => u.Picture).FirstOrDefaultAsync(u => u.Email == mail && u.Password == password);        // DELETE: api/UsersControllers/5

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Include(u => u.Picture).Any(e => e.Id == id);
        }
    }
}

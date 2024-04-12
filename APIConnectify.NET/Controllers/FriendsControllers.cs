using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConnectify.NET.Data;
using APIConnectify.NET.Models;

namespace APIConnectify.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsControllers : ControllerBase
    {
        private readonly DB _context;

        public FriendsControllers(DB context)
        {
            _context = context;
        }

        // GET: api/FriendsControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friends>>> GetFriend()
        {
            return await _context.Friends.ToListAsync();
        }

        // GET: api/FriendsControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(int id)
        {
            var friends = await _context.Friends.FirstOrDefaultAsync(u=>u.Id ==id);

            if (friends == null)
            {
                return NotFound();
            }

            return friends;
        }

        // PUT: api/FriendsControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriends(int id, Friends friends)
        {
            if (id != friends.Id)
            {
                return BadRequest();
            }

            _context.Entry(friends).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendsExists(id))
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

        // POST: api/FriendsControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friends>> PostFriends(List<APIConnectify.NET.Models.Friends> friends)
        {
            Friends friends1 = null;
            foreach(var friend in friends)
            {
                _context.Friends.Add(friend);
                await _context.SaveChangesAsync();

                var friends2 = await _context.Friends.FirstOrDefaultAsync(u =>u.UserId == friend.UserId && u.Friend== friend.Friend);

                friends1 =  friend;
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == friend.UserId);

                 user.Friends.Add(friends2.Id);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var user1 = await _context.Users.FirstOrDefaultAsync(u => u.Id == friend.Friend);

                user1.Friends.Add(friends2.Id);

                _context.Entry(user1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
       

            return CreatedAtAction("GetFriends", new { id = friends1.Id }, friends1);
        }

        // DELETE: api/FriendsControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriends(int id)
        {
            var friends = await _context.Friends.FirstOrDefaultAsync(u=>u.Id ==id);
            if (friends == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friends);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendsExists(int id)
        {
            return _context.Friends.Any(e => e.Id == id);
        }
    }
}

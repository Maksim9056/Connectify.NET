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
            return await _context.Friends.Include(u => u.FriendId).ToListAsync();
        }

        // GET: api/FriendsControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(int id)
        {
            var friends = await _context.Friends.Include(u => u.FriendId).FirstOrDefaultAsync(u=>u.Id ==id);

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
        public async Task<ActionResult<Friends>> PostFriends(Friends friends)
        {
            _context.Friends.Add(friends);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriends", new { id = friends.Id }, friends);
        }

        // DELETE: api/FriendsControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriends(int id)
        {
            var friends = await _context.Friends.Include(u => u.FriendId).FirstOrDefaultAsync(u=>u.Id ==id);
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
            return _context.Friends.Include(u => u.FriendId).Any(e => e.Id == id);
        }
    }
}

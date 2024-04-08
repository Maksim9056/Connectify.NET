using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConnectify.NET.Data;
using APIConnectify.NET.Models;
using Microsoft.AspNetCore.SignalR;

namespace APIConnectify.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsChatsControllers : ControllerBase
    {
        private readonly DB _context;

        public GroupsChatsControllers(DB context)
        {
            _context = context;
        }

        // GET: api/GroupsChatsControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupsChats>>> GetGroupsChats()
        {
            return await _context.GroupsChats.ToListAsync();
        }

        // GET: api/GroupsChatsControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<GroupsChats>>> GetGroupsChats(int id)
        {
            //var groupsChats = await _context.GroupsChats.Include(u => u.Group).Include(u=>u.Files).Include(u => u.Users).FirstOrDefaultAsync(u => u.Id == id).;
            var groupsChats = await _context.GroupsChats.Where(u => u.Group == id).ToListAsync();

            if (groupsChats == null)
            {
                return NotFound();
            }
            if (groupsChats.Count() ==0)
            {
                return NotFound();

            }
            return groupsChats;
        }

        // PUT: api/GroupsChatsControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupsChats(int id, GroupsChats groupsChats)
        {
            if (id != groupsChats.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupsChats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupsChatsExists(id))
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

        // POST: api/GroupsChatsControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupsChats>> PostGroupsChats(GroupsChats groupsChats)
        {
            try
            {
                //bobretsovms21 @st.ithub.ru

                _context.GroupsChats.Add(groupsChats);
                await _context.SaveChangesAsync();

                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return CreatedAtAction("GetGroupsChats", new { id = groupsChats.Id }, groupsChats);
        }

        // DELETE: api/GroupsChatsControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupsChats(int id)
        {
            var groupsChats = await _context.GroupsChats.FirstOrDefaultAsync(u => u.Id == id);
            if (groupsChats == null)
            {
                return NotFound();
            }

            _context.GroupsChats.Remove(groupsChats);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupsChatsExists(int id)
        {
            return _context.GroupsChats.Any(e => e.Id == id);
        }
    }
}

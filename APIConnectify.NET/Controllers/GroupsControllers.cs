using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConnectify.NET.Data;
using APIConnectify.NET.Models;
using Newtonsoft.Json;

namespace APIConnectify.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsControllers : ControllerBase
    {
        private readonly DB _context;

        public GroupsControllers(DB context)
        {
            _context = context;
        }

        // GET: api/GroupsControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Groups.Include(u => u.Participants).ToListAsync();
        }

        // GET: api/GroupsControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var @group = await _context.Groups.Include(u => u.Participants).FirstOrDefaultAsync(u => u.Id == id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // PUT: api/GroupsControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.Id)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/GroupsControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<Group>> PostGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
        }


        // DELETE: api/GroupsControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var @group = await _context.Groups.Include(u => u.Participants).FirstOrDefaultAsync(u => u.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Include(u => u.Participants).Any(e => e.Id == id);
        }
    }
}

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
        public async Task<ActionResult<List<GroupChatsSelect>>> GetGroupsChats(int id)
        {
            //var groupsChats = await _context.GroupsChats.Include(u => u.Group).Include(u=>u.Files).Include(u => u.Users).FirstOrDefaultAsync(u => u.Id == id).;
            var groupsChats = await _context.GroupsChats.Where(u => u.Group == id).ToListAsync();
            bool tr = false;
            List<Users> grouUsersSelects = new List<Users>();

            List<GroupChatsSelect> groupChatsSelects = new List<GroupChatsSelect>();
            GroupUsers sQ = new GroupUsers();
            for (int i = 0; i< groupsChats.Count(); i++)
            {
                 var user = await _context.Users.Include(u => u.Group).Include(u => u.Friends).Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == groupsChats[i].Users);
                 
                
                var @group = await _context.Group.FirstOrDefaultAsync(u => u.Id == groupsChats[i].Group);
                if (tr == false)
                {
                    foreach (var s in @group.Participants)
                    {
                        Users users1 = new Users();
                        Users users = await _context.Users.Include(u => u.Group).Include(u => u.Friends).Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == s);

                        if (users == null)
                        {
                            users1.Username = "Пользователь удален";
                        }
                        else
                        {
                            users1 = users;
                        }


                        grouUsersSelects.Add(users1);
                    }
                    sQ.GroupName = @group.GroupName;
                    sQ.Id = @group.Id;
                    sQ.Participants = grouUsersSelects;
                }
             

                GroupChatsSelect groupChatsSelect = new GroupChatsSelect(groupsChats[i].Id, sQ, user, groupsChats[i].Messages, groupsChats[i].Bytes);
                groupChatsSelects.Add(groupChatsSelect);

            }

            if (groupsChats == null)
            {
                return NotFound();
            }
            if (groupsChats.Count() ==0)
            {
                return NotFound();

            }
            return groupChatsSelects;
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

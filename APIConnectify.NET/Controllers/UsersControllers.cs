using APIConnectify.NET.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIConnectify.NET.Controllers
{
    public class UsersControllers : Controller
    {

        private readonly DB _context;

        public UsersControllers(DB context)
        {
            _context = context;
        }

        // GET: BookModels
        // GET: api/Users/GET
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<APIConnectify.NET.Models.Users>>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.Group) // Assuming Group is a direct navigation property
                .Include(u => u.Friends)
                .Include(u => u.Picture)
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<APIConnectify.NET.Models.Users>> GetUsers(int id)
        {
            var answer = await _context.Users.Include(u => u.Group).Include(u => u.Friends).Include(u => u.Picture).FirstOrDefaultAsync(u=>u.Id ==id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }



        // GET: api/Users/5
        [HttpGet("GETCheck/{mail},{password}")]
        public async Task<ActionResult<APIConnectify.NET.Models.Users>> GetUsersCheck(string mail,string password)
        {
            var answer = await _context.Users.Include(u => u.Group).Include(u => u.Friends).Include(u => u.Picture).FirstOrDefaultAsync(u => u.Email == mail&&u.Password == password);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutUsers(int id, APIConnectify.NET.Models.Users answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<APIConnectify.NET.Models.Users>> PostUsers(APIConnectify.NET.Models.Users answer)
        {
            _context.Users.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Users/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var answer = await _context.Users.Include(u => u.Group).Include(u => u.Friends).Include(u => u.Picture).FirstOrDefaultAsync(u => u.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Users.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Include(u => u.Group).Include(u => u.Friends).Include(u => u.Picture).Any(e => e.Id == id);
        }
    }
}

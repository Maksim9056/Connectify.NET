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
        // GET: api/Answers/GET
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<APIConnectify.NET.Models.Users>>> GetAnswer()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<APIConnectify.NET.Models.Users>> GetAnswer(int id)
        {
            var answer = await _context.User.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutAnswer(int id, APIConnectify.NET.Models.Users answer)
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
                if (!AnswerExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<APIConnectify.NET.Models.Users>> PostAnswer(APIConnectify.NET.Models.Users answer)
        {
            _context.User.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Users/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _context.User.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.User.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}

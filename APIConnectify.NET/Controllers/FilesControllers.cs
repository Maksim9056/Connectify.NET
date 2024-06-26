﻿using System;
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
    public class FilesControllers : ControllerBase
    {
        private readonly DB _context;

        public FilesControllers(DB context)
        {
            _context = context;
        }

        // GET: api/FilesControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Files>>> GetFiles()
        {
            return await _context.Files.ToListAsync();
        }

        // GET: api/FilesControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Files>> GetFiles(int id)
        {
            var files = await _context.Files.FindAsync(id);

            if (files == null)
            {
                return NotFound();
            }

            return files;
        }

        // PUT: api/FilesControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFiles(int id, Files files)
        {
            if (id != files.Id)
            {
                return BadRequest();
            }

            _context.Entry(files).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilesExists(id))
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

        // POST: api/FilesControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Files>> PostFiles(Files files)
        {
            _context.Files.Add(files);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFiles", new { id = files.Id }, files);
        }


        // DELETE: api/FilesControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiles(int id)
        {
            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }

            _context.Files.Remove(files);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilesExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
        }
    }
}

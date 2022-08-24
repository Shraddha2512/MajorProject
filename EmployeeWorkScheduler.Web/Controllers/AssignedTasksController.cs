using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeWorkScheduler.Web.Data;
using EmployeeWorkScheduler.Web.Models;

namespace EmployeeWorkScheduler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssignedTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AssignedTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignedTask>>> GetAssignedTasks()
        {
            return await _context.AssignedTasks.ToListAsync();
        }

        // GET: api/AssignedTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignedTask>> GetAssignedTask(int id)
        {
            var assignedTask = await _context.AssignedTasks.FindAsync(id);

            if (assignedTask == null)
            {
                return NotFound();
            }

            return assignedTask;
        }

        // PUT: api/AssignedTasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignedTask(int id, AssignedTask assignedTask)
        {
            if (id != assignedTask.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(assignedTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignedTaskExists(id))
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

        // POST: api/AssignedTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AssignedTask>> PostAssignedTask(AssignedTask assignedTask)
        {
            _context.AssignedTasks.Add(assignedTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignedTask", new { id = assignedTask.TaskId }, assignedTask);
        }

        // DELETE: api/AssignedTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignedTask>> DeleteAssignedTask(int id)
        {
            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            _context.AssignedTasks.Remove(assignedTask);
            await _context.SaveChangesAsync();

            return assignedTask;
        }

        private bool AssignedTaskExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.TaskId == id);
        }
    }
}

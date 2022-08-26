using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeWorkScheduler.Web.Data;
using EmployeeWorkScheduler.Web.Models;
using Microsoft.Extensions.Logging;

namespace EmployeeWorkScheduler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AssignedTasksController> _logger;  // changes


        public AssignedTasksController(
                   ApplicationDbContext context,
                   ILogger<AssignedTasksController> logger)    //block changed
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/AssignedTasks
        [HttpGet]

        public async Task<IActionResult> GetAssignedTasks()
        {
            try
            {
                var pc = await _context.AssignedTasks.ToListAsync(); 
                if (pc == null)
                {
                    _logger.LogWarning("No Tasks were found");
                    return NotFound();
                }
                _logger.LogInformation("Extracted all the tasks");
                return Ok(pc);
            }
            catch
            {
                _logger.LogError("Attempt made to retrieve information");
                return BadRequest();
            }
        }

        //public async Task<ActionResult<IEnumerable<AssignedTask>>> GetAssignedTasks()
        //{
        //    return await _context.AssignedTasks.ToListAsync();
        //}

        // GET: api/AssignedTasks/5            // Third test here (block changed)
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAssignedTask(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var assignedtask = await _context.AssignedTasks.FindAsync(id);
                if (assignedtask == null) { return NotFound(); }
                return Ok(assignedtask);
            }
            catch
            {
                return BadRequest();
            }


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

        // DELETE: api/AssignedTasks/5        // Changes for delete test
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssignedTask(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var issueAssignedTask = await _context.AssignedTasks.FindAsync(id);
                if (issueAssignedTask == null)
                {
                    return NotFound();
                }

                _context.AssignedTasks.Remove(issueAssignedTask);
                await _context.SaveChangesAsync();

                return Ok(issueAssignedTask);
            }
            catch
            {
                return BadRequest();
            }

        }

        private bool AssignedTaskExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.TaskId == id);
        }
    }
}

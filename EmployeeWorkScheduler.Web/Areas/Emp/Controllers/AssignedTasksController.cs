using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeWorkScheduler.Web.Data;
using EmployeeWorkScheduler.Web.Models;

namespace EmployeeWorkScheduler.Web.Areas.Emp.Controllers
{
    [Area("Emp")]
    public class AssignedTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignedTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emp/AssignedTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssignedTasks.Include(a => a.Admin).Include(a => a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Emp/AssignedTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .Include(a => a.Admin)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }

        // GET: Emp/AssignedTasks/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Admins, "ManagerId", "ManagerId");
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            return View();
        }

        // POST: Emp/AssignedTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,Description,AssignedDate,DueDate,Status,StatusUpdateDate,Priority,EmpId,ManagerId,Comment")] AssignedTask assignedTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignedTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Admins, "ManagerId", "ManagerId", assignedTask.ManagerId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", assignedTask.EmpId);
            return View(assignedTask);
        }

        // GET: Emp/AssignedTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            if (assignedTask == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Admins, "ManagerId", "ManagerId", assignedTask.ManagerId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", assignedTask.EmpId);
            return View(assignedTask);
        }

        // POST: Emp/AssignedTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Description,AssignedDate,DueDate,Status,StatusUpdateDate,Priority,EmpId,ManagerId,Comment")] AssignedTask assignedTask)
        {
            if (id != assignedTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignedTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignedTaskExists(assignedTask.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Admins, "ManagerId", "ManagerId", assignedTask.ManagerId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", assignedTask.EmpId);
            return View(assignedTask);
        }

        // GET: Emp/AssignedTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .Include(a => a.Admin)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }

        // POST: Emp/AssignedTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            _context.AssignedTasks.Remove(assignedTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignedTaskExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.TaskId == id);
        }
    }
}

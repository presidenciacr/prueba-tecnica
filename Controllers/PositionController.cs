using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaPresidencia.Data;
using PruebaTecnicaPresidencia.Models;

namespace PruebaTecnicaPresidencia.Controllers
{
    public class PositionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Intentionally using N+1 query here
        public async Task<IActionResult> Index()
        {
            var positions = await _context.Positions.ToListAsync();
            
            // N+1 query loading employees for each position
            foreach (var position in positions)
            {
                position.Employees = await _context.Employees
                    .Where(e => e.PositionId == position.Id)
                    .ToListAsync();
            }

            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                if (position.MaxSalary < position.MinSalary)
                {
                    ModelState.AddModelError("MaxSalary", "Maximum salary cannot be less than minimum salary");
                    return View(position);
                }

                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Position position)
        {
            if (id != position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (position.MaxSalary < position.MinSalary)
                {
                    ModelState.AddModelError("MaxSalary", "Maximum salary cannot be less than minimum salary");
                    return View(position);
                }

                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Positions.AnyAsync(e => e.Id == id))
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
            return View(position);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await _context.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            if (position.Employees.Any())
            {
                ModelState.AddModelError("", "Cannot delete position while it has employees assigned");
                return View(position);
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KonsulYudiris.Data;
using KonsulYudiris.Models;
using Microsoft.AspNetCore.Authorization;

namespace KonsulYudiris.Controllers
{
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
              return _context.Case != null ? 
                          View(await _context.Case.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Case'  is null.");
        }


        

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var @case = await _context.Case
                .FirstOrDefaultAsync(m => m.CaseID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: Cases/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaseID,CaseHolder,Consultant,CaseType")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@case);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: Cases/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var @case = await _context.Case.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,CaseHolder,Consultant,CaseType")] Case @case)
        {
            if (id != @case.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.CaseID))
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
            return View(@case);
        }

        // GET: Cases/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var @case = await _context.Case
                .FirstOrDefaultAsync(m => m.CaseID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Case == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Case'  is null.");
            }
            var @case = await _context.Case.FindAsync(id);
            if (@case != null)
            {
                _context.Case.Remove(@case);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
          return (_context.Case?.Any(e => e.CaseID == id)).GetValueOrDefault();
        }
    }
}

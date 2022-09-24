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
    public class ConsultantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultants
        public async Task<IActionResult> Index()
        {
              return _context.Consultant != null ? 
                          View(await _context.Consultant.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Consultant'  is null.");
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Case != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Case'  is null.");
        }

        public async Task<IActionResult> ShowSearchResult(String SearchPhrase)
        {
            return _context.Case != null ?
                        View("Index", await _context.Consultant.Where(j => j.ConsultantName.Contains(SearchPhrase)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Case'  is null.");
        }

        // GET: Consultants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultant == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultant
                .FirstOrDefaultAsync(m => m.ConsultantID == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // GET: Consultants/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultantID,ConsultantName,ConsultantPhone,ConsultantSpecialty,ConsultantDetails")] Consultant consultant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultant);
        }

        // GET: Consultants/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultant == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultant.FindAsync(id);
            if (consultant == null)
            {
                return NotFound();
            }
            return View(consultant);
        }

        // POST: Consultants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultantID,ConsultantName,ConsultantPhone,ConsultantSpecialty,ConsultantDetails")] Consultant consultant)
        {
            if (id != consultant.ConsultantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantExists(consultant.ConsultantID))
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
            return View(consultant);
        }

        // GET: Consultants/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultant == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultant
                .FirstOrDefaultAsync(m => m.ConsultantID == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // POST: Consultants/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultant'  is null.");
            }
            var consultant = await _context.Consultant.FindAsync(id);
            if (consultant != null)
            {
                _context.Consultant.Remove(consultant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultantExists(int id)
        {
          return (_context.Consultant?.Any(e => e.ConsultantID == id)).GetValueOrDefault();
        }
    }
}

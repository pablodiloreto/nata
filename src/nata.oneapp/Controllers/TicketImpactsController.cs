using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nata.Data;
using nata.Models;

namespace nata.Controllers
{
    public class TicketImpactsController : Controller
    {
        private readonly NataDbContext _context;

        public TicketImpactsController(NataDbContext context)
        {
            _context = context;
        }

        // GET: TicketImpacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketImpacts.ToListAsync());
        }

        // GET: TicketImpacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketImpacts = await _context.TicketImpacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketImpacts == null)
            {
                return NotFound();
            }

            return View(ticketImpacts);
        }

        // GET: TicketImpacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketImpacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,Name")] TicketImpacts ticketImpacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketImpacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketImpacts);
        }

        // GET: TicketImpacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketImpacts = await _context.TicketImpacts.FindAsync(id);
            if (ticketImpacts == null)
            {
                return NotFound();
            }
            return View(ticketImpacts);
        }

        // POST: TicketImpacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Name")] TicketImpacts ticketImpacts)
        {
            if (id != ticketImpacts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketImpacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketImpactsExists(ticketImpacts.Id))
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
            return View(ticketImpacts);
        }

        // GET: TicketImpacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketImpacts = await _context.TicketImpacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketImpacts == null)
            {
                return NotFound();
            }

            return View(ticketImpacts);
        }

        // POST: TicketImpacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketImpacts = await _context.TicketImpacts.FindAsync(id);
            _context.TicketImpacts.Remove(ticketImpacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketImpactsExists(int id)
        {
            return _context.TicketImpacts.Any(e => e.Id == id);
        }
    }
}

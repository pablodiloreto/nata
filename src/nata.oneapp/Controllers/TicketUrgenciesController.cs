using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nata.Data;
using nata.Models;

namespace nata.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketUrgenciesController : Controller
    {
        private readonly NataDbContext _context;

        public TicketUrgenciesController(NataDbContext context)
        {
            _context = context;
        }

        // GET: TicketUrgencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketUrgencies.ToListAsync());
        }

        // GET: TicketUrgencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketUrgencies = await _context.TicketUrgencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketUrgencies == null)
            {
                return NotFound();
            }

            return View(ticketUrgencies);
        }

        // GET: TicketUrgencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketUrgencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,Name")] TicketUrgencies ticketUrgencies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketUrgencies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketUrgencies);
        }

        // GET: TicketUrgencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketUrgencies = await _context.TicketUrgencies.FindAsync(id);
            if (ticketUrgencies == null)
            {
                return NotFound();
            }
            return View(ticketUrgencies);
        }

        // POST: TicketUrgencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Name")] TicketUrgencies ticketUrgencies)
        {
            if (id != ticketUrgencies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketUrgencies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketUrgenciesExists(ticketUrgencies.Id))
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
            return View(ticketUrgencies);
        }

        // GET: TicketUrgencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketUrgencies = await _context.TicketUrgencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketUrgencies == null)
            {
                return NotFound();
            }

            return View(ticketUrgencies);
        }

        // POST: TicketUrgencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketUrgencies = await _context.TicketUrgencies.FindAsync(id);
            _context.TicketUrgencies.Remove(ticketUrgencies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketUrgenciesExists(int id)
        {
            return _context.TicketUrgencies.Any(e => e.Id == id);
        }
    }
}

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
    public class TicketsController : Controller
    {
        private readonly NataDbContext _context;

        public TicketsController(NataDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var nataDbContext = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency);
            return View(await nataDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.Contact)
                .Include(t => t.Contract)
                .Include(t => t.TicketImpact)
                .Include(t => t.TicketType)
                .Include(t => t.TicketUrgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email");
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name");
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContractId,AssignedTo,Description,DateFrom,ContactId,TicketTypeId,EstimatedHours,CreatedBy,TicketImpactId,TicketUrgencyId,TicketPriority")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", tickets.ContactId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", tickets.ContractId);
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name", tickets.TicketImpactId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", tickets.TicketTypeId);
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name", tickets.TicketUrgencyId);
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", tickets.ContactId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", tickets.ContractId);
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name", tickets.TicketImpactId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", tickets.TicketTypeId);
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name", tickets.TicketUrgencyId);
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContractId,AssignedTo,Description,DateFrom,ContactId,TicketTypeId,EstimatedHours,CreatedBy,TicketImpactId,TicketUrgencyId,TicketPriority")] Tickets tickets)
        {
            if (id != tickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.Id))
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
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", tickets.ContactId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", tickets.ContractId);
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name", tickets.TicketImpactId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", tickets.TicketTypeId);
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name", tickets.TicketUrgencyId);
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.Contact)
                .Include(t => t.Contract)
                .Include(t => t.TicketImpact)
                .Include(t => t.TicketType)
                .Include(t => t.TicketUrgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tickets = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(tickets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}

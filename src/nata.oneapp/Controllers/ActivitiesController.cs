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
    [Authorize(Roles = "Admin,SuperUser,User")]
    public class ActivitiesController : Controller
    {
        private readonly NataDbContext _context;
        private readonly ApplicationDbContext _userContext;

        public ActivitiesController(NataDbContext context, ApplicationDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: Activities
        public async Task<IActionResult> Index(string searchClient, string searchUserName, string searchString)
        {

            var clientsQuery = from a in _context.Accounts
                               orderby a.Name
                               select a;

            var usersQuery = from a in _userContext.Users
                             orderby a.UserName
                             select a;


            var activitiesResults = _context.Activities.Include(t => t.Ticket).ThenInclude(t => t.Contract).ThenInclude(a => a.Account).OrderBy(d => d.Date).AsQueryable();




            if (!string.IsNullOrEmpty(searchString))
            {
                activitiesResults = activitiesResults.Where(n => n.Details.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchClient))
            {
                activitiesResults = activitiesResults.Where(c => c.Ticket.Contract.Account.Id.Equals(Convert.ToInt32(searchClient)));
            }

            if (!string.IsNullOrEmpty(searchUserName))
            {
                activitiesResults = activitiesResults.Where(u => u.UserId.Equals(searchUserName));
            }

            var activitiesViewModel = new ActivitiesViewModel
            {
                Accounts = new SelectList(await clientsQuery.Distinct().ToListAsync(), "Id", "Name"),
                Users = new SelectList(await usersQuery.Distinct().ToListAsync(), "Id", "UserName"),
                Activities = await activitiesResults.ToListAsync(),
            };



            return View(activitiesViewModel);
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .Include(a => a.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // GET: Activities/Create
        public IActionResult Create(string? TicketId)
        {

            var ticketsResults = _context.Tickets.Where(c => c.Contract.AccountId == 0).AsQueryable();

            //var userIdentity = _userContext.Users
            //    .Where(u => u.UserName == User.Identity.Name).ToList();

            if (!string.IsNullOrEmpty(TicketId))
            {
                ticketsResults = _context.Tickets.Where(n => n.Contract.AccountId.Equals(Convert.ToInt32(TicketId))).Where(s => s.Status.Equals(true));
            }

            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", TicketId);
            ViewData["TicketId"] = new SelectList(ticketsResults, "Id", "Name");
            ViewData["AssignedTo"] = new SelectList(_userContext.Users, "Id", "UserName");

            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketId,Date,UserId,Efforts")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Name", activities.TicketId);
            return View(activities);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .Include(t => t.Ticket)
                .ThenInclude(t => t.Contract)
                .ThenInclude(t => t.Account)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (activities == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets.Where(t => t.Contract.AccountId == activities.Ticket.Contract.AccountId), "Id", "Name", activities.TicketId);
            return View(activities);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,Date,UserId,Efforts")] Activities activities)
        {
            if (id != activities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivitiesExists(activities.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Name", activities.TicketId);
            return View(activities);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .Include(a => a.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activities = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(activities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivitiesExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}

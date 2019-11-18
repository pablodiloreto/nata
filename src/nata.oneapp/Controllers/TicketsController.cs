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
    public class TicketsController : Controller
    {
        private readonly NataDbContext _context;
        private readonly ApplicationDbContext _userContext;

        public TicketsController(NataDbContext context, ApplicationDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string searchStatus, string searchClient, string userName, string searchString)
        {
            //var nataDbContext = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency);
            //return View(await nataDbContext.ToListAsync());


            IQueryable<bool> statusQuery = from a in _context.Tickets
                                           select a.Status;

            IQueryable<string> clientsQuery = from a in _context.Accounts
                                               orderby a.Name
                                               select a.Name;

            IQueryable<string> usersQuery = from a in _userContext.Users
                                              orderby a.UserName
                                              select a.UserName;

            IQueryable<string> searchQuery = from a in _context.Tickets
                                              orderby a.Name
                                              select a.Name;


            var ticketsResults = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                //contracts = contracts.Include(c => c.Client).Where(n => n.Name.Contains(searchString));
                ticketsResults = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency).Where(n => n.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchClient))
            {
                ticketsResults = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency).Where(n => n.Contract.Account.Name.Equals(searchClient));
            }

            if (!string.IsNullOrEmpty(searchStatus))
            {
                ticketsResults = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency).Where(s => s.Status == Convert.ToBoolean(searchStatus));

            }


            //var assignetToUsername = await _userContext.Users
            //    .FirstOrDefaultAsync(u => u.Id == tickets.AssignedTo);

            //var createdByUsername = await _userContext.Users
            //    .FirstOrDefaultAsync(u => u.Id == tickets.CreatedBy);

            var ticketsViewModel = new TicketsViewModel
            {
                Status = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Accounts = new SelectList(await clientsQuery.Distinct().ToListAsync()),
                Users = new SelectList(await usersQuery.Distinct().ToListAsync()),
                Tickets = await ticketsResults.ToListAsync(),
                //AssignedToUsername = assignetToUsername.UserName,
                //CreatedByUsername = createdByUsername.UserName
            };



            return View(ticketsViewModel);

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

            var assignetToUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == tickets.AssignedTo);

            var createdByUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == tickets.CreatedBy);

            var ticketViewModel = new TicketViewModel
            {
                Ticket = tickets,
                AssignedToUsername = assignetToUsername.UserName,
                CreatedByUsername = createdByUsername.UserName

                //Status = new SelectList(await statusQuery.Distinct().ToListAsync()),
                //Accounts = new SelectList(await clientsQuery.Distinct().ToListAsync()),
                //Contracts = await contractsResults.ToListAsync()
            };


            return View(ticketViewModel);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {

            var users = _userContext.Users.ToList();

            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email");
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name");
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name");
            ViewData["AssignedTo"] = new SelectList(_userContext.Users, "Id", "UserName");

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

            var assignetToUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == tickets.AssignedTo);

            var createdByUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == tickets.CreatedBy);

            var ticketViewModel = new TicketViewModel
            {
                Ticket = tickets,
                AssignedToUsername = assignetToUsername.UserName,
                CreatedByUsername = createdByUsername.UserName

                //Status = new SelectList(await statusQuery.Distinct().ToListAsync()),
                //Accounts = new SelectList(await clientsQuery.Distinct().ToListAsync()),
                //Contracts = await contractsResults.ToListAsync()
            };

            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", tickets.ContactId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", tickets.ContractId);
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name", tickets.TicketImpactId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", tickets.TicketTypeId);
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name", tickets.TicketUrgencyId);
            ViewData["AssignedTo"] = new SelectList(_userContext.Users, "Id", "Username", tickets.AssignedTo);
            ViewData["CreatedBy"] = new SelectList(_userContext.Users, "Id", "Username", tickets.CreatedBy);

            return View(ticketViewModel);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

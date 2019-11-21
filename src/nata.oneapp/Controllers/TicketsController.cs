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
        public async Task<IActionResult> Index(string searchStatus, string searchClient, string searchUserName, string searchString, string searchPriority)
        {

            IQueryable<bool> statusQuery = from a in _context.Tickets
                                           orderby a.Status
                                           select a.Status;

            IQueryable<byte> priorityQuery = from a in _context.Tickets
                                           orderby a.TicketPriority
                                           select a.TicketPriority;

            var clientsQuery = from a in _context.Accounts
                                        orderby a.Name
                                        select a;

            var usersQuery = from a in _userContext.Users
                                        orderby a.UserName
                                        select a;


            var ticketsResults = _context.Tickets.Include(t => t.Contact).Include(t => t.Contract).Include(t => t.TicketImpact).Include(t => t.TicketType).Include(t => t.TicketUrgency).OrderBy(d => d.DateFrom).AsQueryable();




            if (!string.IsNullOrEmpty(searchString))
            {
                ticketsResults = ticketsResults.Where(n => n.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchClient))
            {
                ticketsResults = ticketsResults.Where(c => c.Contract.Account.Id.Equals(Convert.ToInt32(searchClient)));
            }

            if (!string.IsNullOrEmpty(searchPriority))
            {
                ticketsResults = ticketsResults.Where(p => p.TicketPriority.Equals(Convert.ToByte(searchPriority)));
            }

            if (!string.IsNullOrEmpty(searchStatus))
            {
                ticketsResults = ticketsResults.Where(s => s.Status == Convert.ToBoolean(searchStatus));
            }

            if (!string.IsNullOrEmpty(searchUserName))
            {
                ticketsResults = ticketsResults.Where(u => u.AssignedTo.Equals(searchUserName));
            }

            var ticketsViewModel = new TicketsViewModel
            {
                Status = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Accounts = new SelectList(await clientsQuery.Distinct().ToListAsync(), "Id", "Name"),
                Users = new SelectList(await usersQuery.Distinct().ToListAsync(), "Id", "UserName"),
                Priority = new SelectList(await priorityQuery.Distinct().ToListAsync()),
                Tickets = await ticketsResults.ToListAsync(),
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

            var ticket= await _context.Tickets
                .Include(t => t.Contact)
                .Include(t => t.Contract)
                .ThenInclude(t => t.Account)
                .Include(t => t.TicketImpact)
                .Include(t => t.TicketType)
                .Include(t => t.TicketUrgency)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var assignedToUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == ticket.AssignedTo);

            var createdByUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == ticket.CreatedBy);

            var ticketsViewModel = new TicketsViewModel
            {
                Ticket = ticket,
                AssignedToUserName = assignedToUsername.UserName,
                CreatedByUserName = createdByUsername.UserName,
                Account = ticket.Contract.Account.Name
            };


            return View(ticketsViewModel);
        }

        // GET: Tickets/Create
        public IActionResult Create(string? ContractId)
        {

            //var users = _userContext.Users.ToList();
            //var clients = _context.Accounts.ToList();

            var contractsResults = _context.Contracts.Where(c => c.AccountId == 0).AsQueryable();

            //var ticketViewModel = new TicketViewModel
            //{
            //    AccountId = AccountId
            //};


            if (!string.IsNullOrEmpty(ContractId))
            {
                contractsResults = _context.Contracts.Where(n => n.AccountId.Equals(Convert.ToInt32(ContractId)));
            }

            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email");
            ViewData["ContractId"] = new SelectList(contractsResults, "Id", "Name");
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name");
            ViewData["AssignedTo"] = new SelectList(_userContext.Users, "Id", "UserName");
            ViewData["CreatedBy"] = new SelectList(_userContext.Users.Where(u => u.UserName == User.Identity.Name), "Id", "UserName");
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", ContractId);

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContractId,AssignedTo,Description,DateFrom,ContactId,TicketTypeId,EstimatedHours,CreatedBy,TicketImpactId,TicketUrgencyId,TicketPriority,Status")] Tickets tickets)
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
                .ThenInclude(t => t.Account)
                .Include(t => t.TicketImpact)
                .Include(t => t.TicketType)
                .Include(t => t.TicketUrgency)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tickets == null)
            {
                return NotFound();
            }

            var assignedToUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == tickets.AssignedTo);

            var createdByUsername = await _userContext.Users
                .FirstOrDefaultAsync(u => u.Id == tickets.CreatedBy);

            //var ticketViewModel = new TicketViewModel
            //{
            //    Ticket = tickets,
            //    AssignedToUsername = assignedToUsername.UserName,
            //    CreatedByUsername = createdByUsername.UserName
            //};

            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", tickets.ContactId);
            ViewData["ContractId"] = new SelectList(_context.Contracts.Where(c => c.AccountId == tickets.Contract.AccountId), "Id", "Name", tickets.ContractId);
            ViewData["TicketImpactId"] = new SelectList(_context.TicketImpacts, "Id", "Name", tickets.TicketImpactId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", tickets.TicketTypeId);
            ViewData["TicketUrgencyId"] = new SelectList(_context.TicketUrgencies, "Id", "Name", tickets.TicketUrgencyId);
            ViewData["AssignedTo"] = new SelectList(_userContext.Users.ToList(), "Id", "UserName", tickets.AssignedTo);
            ViewData["CreatedBy"] = new SelectList(_userContext.Users.ToList(), "Id", "UserName", tickets.CreatedBy);
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", tickets.Contract.AccountId);

            return View(tickets);

        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContractId,AssignedTo,Description,DateFrom,ContactId,TicketTypeId,EstimatedHours,CreatedBy,TicketImpactId,TicketUrgencyId,TicketPriority,Status")] Tickets tickets)
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
            ViewData["AssignedTo"] = new SelectList(_userContext.Users.ToList(), "Id", "UserName", tickets.AssignedTo);
            ViewData["CreatedBy"] = new SelectList(_userContext.Users.ToList(), "Id", "UserName", tickets.CreatedBy);
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

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

namespace nata.Views
{
    [Authorize(Roles = "Admin,SuperUser,User")]
    public class AccountContactsController : Controller
    {
        private readonly NataDbContext _context;

        public AccountContactsController(NataDbContext context)
        {
            _context = context;
        }

        // GET: AccountContacts
        public async Task<IActionResult> Index()
        {
            var nataDbContext = _context.AccountContacts.Include(a => a.Account).Include(a => a.Contact);
            return View(await nataDbContext.ToListAsync());
        }

        // GET: AccountContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountContacts = await _context.AccountContacts
                .Include(a => a.Account)
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (accountContacts == null)
            {
                return NotFound();
            }

            return View(accountContacts);
        }

        // GET: AccountContacts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name");
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Name");
            return View();
        }

        // POST: AccountContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,ContactId")] AccountContacts accountContacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountContacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", accountContacts.AccountId);
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Name", accountContacts.ContactId);
            return View(accountContacts);
        }

        // GET: AccountContacts/Edit/5
        public async Task<IActionResult> Edit(int? AccountId, int? ContactId)
        {
            if (AccountId == null || ContactId == null)
            {
                return NotFound();
            }

            var accountContacts = await _context.AccountContacts.Where(a => a.AccountId == AccountId && a.ContactId == ContactId).FirstOrDefaultAsync();
            if (accountContacts == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", accountContacts.AccountId);
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Name", accountContacts.ContactId);
            return View(accountContacts);
        }

        // POST: AccountContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,ContactId")] AccountContacts accountContacts)
        {
            if (id != accountContacts.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountContacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountContactsExists(accountContacts.AccountId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", accountContacts.AccountId);
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Name", accountContacts.ContactId);
            return View(accountContacts);
        }

        // GET: AccountContacts/Delete/5
        public async Task<IActionResult> Delete(int? AccountId, int? ContactId)
        {
            if (AccountId == null || ContactId == null)
            {
                return NotFound();
            }

            var accountContacts = await _context.AccountContacts.Include("Account").Include("Contact").FirstOrDefaultAsync(m => m.AccountId == AccountId && m.ContactId == ContactId);

            if (accountContacts == null)
            {
                return NotFound();
            }

            return View(accountContacts);
        }

        // POST: AccountContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int AccountId, int ContactId)
        {
            var accountContacts = await _context.AccountContacts.FirstOrDefaultAsync(m => m.AccountId == AccountId && m.ContactId == ContactId);
            _context.AccountContacts.Remove(accountContacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool AccountContactsExists(int id)
        {
            return _context.AccountContacts.Any(e => e.AccountId == id);
        }
    }
}

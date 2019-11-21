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
    public class AccountsController : Controller
    {
        private readonly NataDbContext _context;

        public AccountsController(NataDbContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index(string searchStatus, string searchString)
        {

            IQueryable<bool> accountsQuery = from a in _context.Accounts
                                                    select a.Status;

            var accounts = from a in _context.Accounts
                           .OrderBy(a => a.Name)
                           select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(n => n.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchStatus) && (searchStatus.ToString() != "all"))
            {
                accounts = accounts.Where(s => s.Status == Convert.ToBoolean(searchStatus));
            }
            if (string.IsNullOrEmpty(searchStatus))
            {
                accounts = accounts.Where(s => s.Status == true);
            }

            var accountsViewModel = new AccountsViewModel
            {
                Status = new SelectList(await accountsQuery.Distinct().ToListAsync(), bool.Parse("True")),
                Accounts = await accounts.ToListAsync()
            };

            return View(accountsViewModel);

        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // GET: Accounts/Create
        [Authorize(Roles = "Admin,SuperUser")]
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phone,CountryId,Email,Status")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", accounts.CountryId);
            return View(accounts);
        }

        // GET: Accounts/Edit/5
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", accounts.CountryId);
            return View(accounts);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phone,CountryId,Email,Status")] Accounts accounts)
        {
            if (id != accounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountsExists(accounts.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", accounts.CountryId);
            return View(accounts);
        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountsExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}

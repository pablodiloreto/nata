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
    public class ContractsController : Controller
    {
        private readonly NataDbContext _context;

        public ContractsController(NataDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(string searchStatus, string searchClient, string searchString)
        //public async Task<IActionResult> Index()
        {
            //var contractsResults = _context.Contracts.Include(c => c.Client).Where(n => n.Name.Contains(searchString));

            //return View(await nataDbContext.ToListAsync());

            IQueryable<bool> statusQuery = from a in _context.Contracts
                                           select a.Status;

            IQueryable<string> clientsQuery = from a in _context.Contracts
                                              orderby a.Account.Name
                                              select a.Account.Name;

            //var contracts = from a in _context.Contracts
            //                join v in _context.Accounts on a.ClientId equals v.Id
            //               select a;

            var contractsResults = _context.Contracts.Include(a => a.Account).Include(ct => ct.ContractType).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                //contracts = contracts.Include(c => c.Client).Where(n => n.Name.Contains(searchString));
                contractsResults = _context.Contracts.Include(a => a.Account).Include(ct => ct.ContractType).Where(n => n.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchClient))
            {
                contractsResults = _context.Contracts.Include(a => a.Account).Include(ct => ct.ContractType).Where(n => n.Account.Name.Equals(searchClient));
            }

            if (!string.IsNullOrEmpty(searchStatus))
            {
                contractsResults = _context.Contracts.Include(a => a.Account).Include(ct => ct.ContractType).Where(s => s.Status == Convert.ToBoolean(searchStatus));

            }

            var contractsViewModel = new ContractsViewModel
            {
                Status = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Accounts = new SelectList(await clientsQuery.Distinct().ToListAsync()),
                Contracts = await contractsResults.ToListAsync()
            };



            return View(contractsViewModel);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts
                .Include(c => c.Account)
                .Include(c => c.ContractType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contracts == null)
            {
                return NotFound();
            }

            return View(contracts);
        }

        // GET: Contracts/Create
        [Authorize(Roles = "Admin,SuperUser")]
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name");
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Create([Bind("Id,Name,AccountId,ContractTypeId,DateFrom,DateTo,Hours,Status")] Contracts contracts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contracts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", contracts.AccountId);
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Name", contracts.ContractTypeId);
            return View(contracts);
        }

        // GET: Contracts/Edit/5
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", contracts.AccountId);
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Name", contracts.ContractTypeId);
            return View(contracts);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AccountId,ContractTypeId,DateFrom,DateTo,Hours,Status")] Contracts contracts)
        {
            if (id != contracts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contracts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractsExists(contracts.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", contracts.AccountId);
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Name", contracts.ContractTypeId);
            return View(contracts);
        }

        // GET: Contracts/Delete/5
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts
                .Include(c => c.Account)
                .Include(c => c.ContractType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contracts == null)
            {
                return NotFound();
            }

            return View(contracts);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contracts = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contracts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractsExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}

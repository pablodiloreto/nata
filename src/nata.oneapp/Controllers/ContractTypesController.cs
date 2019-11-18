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
    [Authorize(Roles = "Admin")]
    public class ContractTypesController : Controller
    {
        private readonly NataDbContext _context;

        public ContractTypesController(NataDbContext context)
        {
            _context = context;
        }

        // GET: ContractTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContractTypes.ToListAsync());
        }

        // GET: ContractTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractTypes = await _context.ContractTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractTypes == null)
            {
                return NotFound();
            }

            return View(contractTypes);
        }

        // GET: ContractTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContractTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,HoursRequired,Status")] ContractTypes contractTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contractTypes);
        }

        // GET: ContractTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractTypes = await _context.ContractTypes.FindAsync(id);
            if (contractTypes == null)
            {
                return NotFound();
            }
            return View(contractTypes);
        }

        // POST: ContractTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,HoursRequired,Status")] ContractTypes contractTypes)
        {
            if (id != contractTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractTypesExists(contractTypes.Id))
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
            return View(contractTypes);
        }

        // GET: ContractTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractTypes = await _context.ContractTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractTypes == null)
            {
                return NotFound();
            }

            return View(contractTypes);
        }

        // POST: ContractTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractTypes = await _context.ContractTypes.FindAsync(id);
            _context.ContractTypes.Remove(contractTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractTypesExists(int id)
        {
            return _context.ContractTypes.Any(e => e.Id == id);
        }
    }
}

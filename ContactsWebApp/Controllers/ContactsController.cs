using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactsWebApp.Data;
using ContactsWebApp.Models;

namespace ContactsWebApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactsModels
        public async Task<IActionResult> Index()
        {
              return _context.ContactsModel != null ? 
                          View(await _context.ContactsModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ContactsModel'  is null.");
        }
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.ContactsModel.Where(c => c.Name.Contains(SearchPhrase)).ToListAsync());
        }
        public async Task<IActionResult> ShowSearchForm(String SearchPhrase)
        {
            return View(await _context.ContactsModel.ToListAsync());
        }


        // GET: ContactsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactsModel == null)
            {
                return NotFound();
            }

            var contactsModel = await _context.ContactsModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (contactsModel == null)
            {
                return NotFound();
            }

            return View(contactsModel);
        }

        // GET: ContactsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Email,PhoneNumber,Address,City,PostalCode,Country")] ContactsModel contactsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactsModel);
        }

        // GET: ContactsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactsModel == null)
            {
                return NotFound();
            }

            var contactsModel = await _context.ContactsModel.FindAsync(id);
            if (contactsModel == null)
            {
                return NotFound();
            }
            return View(contactsModel);
        }

        // POST: ContactsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Email,PhoneNumber,Address,City,PostalCode,Country")] ContactsModel contactsModel)
        {
            if (id != contactsModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsModelExists(contactsModel.id))
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
            return View(contactsModel);
        }

        // GET: ContactsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactsModel == null)
            {
                return NotFound();
            }

            var contactsModel = await _context.ContactsModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (contactsModel == null)
            {
                return NotFound();
            }

            return View(contactsModel);
        }

        // POST: ContactsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactsModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactsModel'  is null.");
            }
            var contactsModel = await _context.ContactsModel.FindAsync(id);
            if (contactsModel != null)
            {
                _context.ContactsModel.Remove(contactsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsModelExists(int id)
        {
          return (_context.ContactsModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

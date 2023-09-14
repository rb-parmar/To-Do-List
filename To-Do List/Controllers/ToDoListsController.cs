using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class ToDoListsController : Controller
    {
        private readonly ToDoListDbContext _context;

        public ToDoListsController(ToDoListDbContext context)
        {
            _context = context;
        }

        // GET: ToDoLists
        public async Task<IActionResult> Index()
        {
              return _context.ToDoList != null ? 
                          View(await _context.ToDoList.Include(l => l.Items).ToListAsync()) :
                          Problem("Entity set 'ToDoListDbContext.ToDoList'  is null.");
        }

        // GET: ToDoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoList == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList
                .Include(l => l.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListName")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoList);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "ToDoLists", new {id = toDoList.Id});
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDoList == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ListName")] ToDoList toDoList)
        {
            if (id != toDoList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListExists(toDoList.Id))
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
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDoList == null)
            {
                return NotFound();
            }

            var toDoList = await _context.ToDoList
                .Include(l => l.Items)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDoList == null)
            {
                return Problem("Entity set 'ToDoListDbContext.ToDoList'  is null.");
            }
            var toDoList = await _context.ToDoList.FindAsync(id);
            if (toDoList != null)
            {
                foreach(var item in toDoList.Items)
                {
                    _context.ToDoItem.Remove(item);
                }
                _context.ToDoList.Remove(toDoList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListExists(int id)
        {
          return (_context.ToDoList?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult HandleToggle(int id)
        {
            ToDoItem foundItem = _context.ToDoItem.First(i => i.Id == id);
            if (foundItem == null)
            {
                return NotFound();
            } else
            {
                foundItem.IsComplete = !foundItem.IsComplete;
                _context.SaveChanges();
                return RedirectToAction("Details");
            }


            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;
using System.Threading.Tasks;

namespace ToDoListApp.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ToDoListDbContext _context;

        public ToDoItemsController(ToDoListDbContext context)
        {
            _context = context;
        }

        //index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoItems.ToListAsync());
        }

        //item details
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null) 
            { 
                return NotFound();
            }
            var ToDoItem = await _context.TodoItems.FirstOrDefaultAsync(m=> m.Id == Id);

            if (ToDoItem == null)
            { 
                return NotFound();
            }
            return View(ToDoItem);
        }

        //create item

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //handling http request and validaton

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,IsCompleted")] ToDoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        //edit items

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems.FindAsync(Id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,TaskName,IsCompleted")] ToDoItem todoItem)
        {
            if (id != todoItem.Id) {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(todoItem.Id))
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
            return View(todoItem);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var todoItem = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        private bool ToDoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}

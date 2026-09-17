using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;

namespace PortfolioWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Messages
        public async Task<IActionResult> Index()
        {
            var messages = await _context.ContactForms
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return View(messages);
        }

        // GET: /Messages/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var message = await _context.ContactForms
                .FirstOrDefaultAsync(x => x.ContactFormId == id);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: /Messages/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.ContactForms
                .FirstOrDefaultAsync(x => x.ContactFormId == id);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: /Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.ContactForms
                .FirstOrDefaultAsync(x => x.ContactFormId == id);

            if (message == null)
            {
                return NotFound();
            }

            _context.ContactForms.Remove(message);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
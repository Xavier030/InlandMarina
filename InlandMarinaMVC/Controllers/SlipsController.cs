using InlandMarinaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InlandMarinaMVC.Controllers
{
    public class SlipsController : Controller
    {
        private readonly InlandMarinaContext _context;

        public SlipsController(InlandMarinaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? dockId, string searchQuery)
        {
            IQueryable<Slip> query = _context.Slips;

            if (dockId.HasValue)
            {
                query = query.Where(s => s.DockID == dockId);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                if (int.TryParse(searchQuery, out int slipId))
                {
                    query = query.Where(s => s.ID == slipId);
                }
                else
                {
                    query = query.Where(s =>
                        s.Width.ToString().Contains(searchQuery) ||
                        s.Length.ToString().Contains(searchQuery) ||
                        s.Dock.Name.Contains(searchQuery)
                    );
                }
            }

            var slips = await query
                .Include(s => s.Dock)
                .Include(s => s.Leases) 
                .ToListAsync();

            ViewBag.Docks = new SelectList(_context.Docks, "ID", "Name", dockId);
            ViewData["SearchQuery"] = searchQuery;

            return View(slips);
        }

        public IActionResult Search(string query)
        {
            return RedirectToAction("Index", new { searchQuery = query });
        }

        [Authorize]
        public async Task<IActionResult> Lease(int id)
        {
            var slip = await _context.Slips
                .Include(s => s.Dock)
                .Include(s => s.Leases)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (slip == null)
            {
                return NotFound();
            }

            if (slip.Leases.Any(l => l.SlipID == id && l.Active))
            {
                TempData["ErrorMessage"] = "This slip is already leased.";
                return RedirectToAction("Index");
            }

            var customer = await GetCurrentCustomerAsync();
            if (customer == null)
            {
                return RedirectToAction("Login", "Customer", new { returnUrl = Url.Action("Lease", "Slips", new { id }) });
            }

            var lease = new Lease
            {
                SlipID = slip.ID,
                CustomerID = customer.ID,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1), 
                Active = true
            };

            _context.Add(lease);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "You have successfully leased this slip!";
            return RedirectToAction("MySlips", "Customer");
        }

        private async Task<Customer> GetCurrentCustomerAsync()
        {
            var username = User.Identity.Name;
            return await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        }
    }
}

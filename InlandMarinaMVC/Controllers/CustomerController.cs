using InlandMarinaData;
using InlandMarinaMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly InlandMarinaContext _context;

        public CustomerController(InlandMarinaContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                if (CustomerDB.CustomerExists(_context, model.Username))
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View(model);
                }

                // Create new customer
                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    City = model.City,
                    Username = model.Username,
                    Password = model.Password
                };

                // Add to database
                if (CustomerDB.AddCustomer(_context, customer))
                {
                    // Optionally log in the user after registration
                    HttpContext.Session.SetInt32("CustomerId", customer.ID);
                    HttpContext.Session.SetString("CustomerName", $"{customer.FirstName} {customer.LastName}");

                    TempData["Message"] = "Registration successful, please log in.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Registration failed. Please try again.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = CustomerDB.ValidateCustomer(_context, model.Username, model.Password);
                if (customer != null)
                {
                    HttpContext.Session.SetInt32("CustomerId", customer.ID);
                    HttpContext.Session.SetString("CustomerName", $"{customer.FirstName} {customer.LastName}");

                    // Set up authentication cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, customer.Username),
                        new Claim(ClaimTypes.NameIdentifier, customer.ID.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties).Wait();

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }
        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }
        // slips
        [Authorize]
        [Authorize]
        public async Task<IActionResult> MySlips()
        {

            var customer = await GetCurrentCustomerAsync();
            var customerId = customer.ID;

            var leases = await _context.Leases
                .Where(l => l.CustomerID == customerId && l.Active) 
                .Include(l => l.Slip)
                    .ThenInclude(s => s.Dock)
                .ToListAsync();

            return View(leases);
        }

        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // 可以设置其他属性如过期时间等
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private async Task<Customer> GetCurrentCustomerAsync()
        {
            var username = User.Identity.Name;
            return await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        }
    }
}

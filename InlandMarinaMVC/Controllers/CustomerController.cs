using InlandMarinaData.Data;
using InlandMarinaData.Entities;
using InlandMarinaMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                    Password = model.Password // Will be hashed in CustomerDB.AddCustomer
                };

                // Add to database
                if (CustomerDB.AddCustomer(_context, customer))
                {
                    // Log in the user after registration
                    await Authenticate(customer.Username);

                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = CustomerDB.ValidateCustomer(_context, model.Username, model.Password);
                if (customer != null)
                {
                    // Set session variables
                    HttpContext.Session.SetInt32("CustomerId", customer.ID);
                    HttpContext.Session.SetString("CustomerName", $"{customer.FirstName} {customer.LastName}");

                    // Set up authentication
                    await Authenticate(customer.Username);

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    TempData["SuccessMessage"] = $"Welcome back, {customer.FirstName}!";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MySlips()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == username);

            if (customer == null)
                return RedirectToAction("Login");

            var leases = await _context.Leases
                .Where(l => l.CustomerID == customer.ID && l.Active)
                .Include(l => l.Slip)
                    .ThenInclude(s => s.Dock)
                .Include(l => l.Customer)
                .ToListAsync();

            return View(leases);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == username);

            if (customer == null)
                return RedirectToAction("Login");

            var profileModel = new ProfileViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                City = customer.City,
                Username = customer.Username
            };

            return View(profileModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity?.Name;
                if (string.IsNullOrEmpty(username))
                    return RedirectToAction("Login");

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Username == username);

                if (customer == null)
                    return RedirectToAction("Login");

                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Phone = model.Phone;
                customer.City = model.City;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }

            return View("Profile", model);
        }

        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Customer")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
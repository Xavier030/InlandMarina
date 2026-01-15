using Microsoft.EntityFrameworkCore;
using InlandMarinaData.Entities;
using System.Security.Cryptography;
using System.Text;

namespace InlandMarinaData.Data
{
    public static class CustomerDB
    {
        // Check if customer exists by username
        public static bool CustomerExists(InlandMarinaContext context, string username)
        {
            return context.Customers.Any(c => c.Username == username);
        }

        // Add new customer
        public static bool AddCustomer(InlandMarinaContext context, Customer customer)
        {
            try
            {
                // Hash password before saving
                customer.Password = HashPassword(customer.Password);
                context.Customers.Add(customer);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Validate customer credentials
        public static Customer? ValidateCustomer(InlandMarinaContext context, string username, string password)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Username == username);
            if (customer != null && VerifyPassword(password, customer.Password))
            {
                return customer;
            }
            return null;
        }

        // Hash password using SHA256
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Verify password
        private static bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashOfInput = HashPassword(inputPassword);
            return hashOfInput == storedHash;
        }
    }
}
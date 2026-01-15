using InlandMarinaData.Data;
using InlandMarinaData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public static class CustomerDB
    {
        public static List<Customer> GetCustomers(InlandMarinaContext context)
        {
            return context.Customers.ToList();
        }

        public static bool AddCustomer(InlandMarinaContext context, Customer customer)
        {
            if (context.Customers.Any(c => c.Username == customer.Username))
            {
                return false;
            }

            context.Customers.Add(customer);
            return context.SaveChanges() > 0;
        }
        public static bool CustomerExists(InlandMarinaContext context, string username)
        {
            return context.Customers.Any(c => c.Username == username);
        }

        public static Customer ValidateCustomer(InlandMarinaContext context, string username, string password)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Username == username);

            if (customer != null )
            {
                return customer;
            }

            return null;
        }
    }
}

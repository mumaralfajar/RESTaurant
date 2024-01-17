using Microsoft.EntityFrameworkCore;
using RESTaurant.Data;
using RESTaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTaurant.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomer(Customer Customer)
        {
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
            return Customer;
        }

        public async Task<Customer> UpdateCustomer(int id, Customer Customer)
        {
            _context.Entry(Customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Customer;
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var Customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(Customer);
            await _context.SaveChangesAsync();
            return Customer;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}

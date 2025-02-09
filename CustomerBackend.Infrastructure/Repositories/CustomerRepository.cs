using CustomerBackend.Domain.Entities;
using CustomerBackend.Domain.Interfaces.IRepositories;
using CustomerBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            return await _context.Customers
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Company)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetByCompanyIdAsync(long companyId)
        {
            return await _context.Customers
                .Include(c => c.Company)
                .Where(c => c.CompanyId == companyId && c.IsActive)
                .ToListAsync();
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            customer.CreatedDate = DateTime.UtcNow;
            customer.IsActive = true;

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            customer.UpdatedDate = DateTime.UtcNow;
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null)
            {
                customer.IsActive = false;
                customer.UpdatedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Customers
                .AnyAsync(c => c.Id == id && c.IsActive);
        }
    }
}

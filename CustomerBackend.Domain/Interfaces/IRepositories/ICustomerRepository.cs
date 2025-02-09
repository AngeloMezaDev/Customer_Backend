using CustomerBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend.Domain.Interfaces.IRepositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(long id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<IEnumerable<Customer>> GetByCompanyIdAsync(long companyId);
        Task<Customer> AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}

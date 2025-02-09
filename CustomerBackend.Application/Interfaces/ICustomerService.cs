using CustomerBackend.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerBackend.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();

        Task<CustomerDTO> GetCustomerByIdAsync(long id);

        Task<IEnumerable<CustomerDTO>> GetCustomersByCompanyIdAsync(long companyId);

        Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO customerDto);

        Task<CustomerDTO> UpdateCustomerAsync(UpdateCustomerDTO customerDto);

        Task<bool> DeleteCustomerAsync(long id);
    }
}
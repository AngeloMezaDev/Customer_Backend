using AutoMapper;
using CustomerBackend.Application.DTOs;
using CustomerBackend.Application.Interfaces;
using CustomerBackend.Domain.Entities;
using CustomerBackend.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerBackend.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(long id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Cliente con ID {id} no encontrado");

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersByCompanyIdAsync(long companyId)
        {
            var customers = await _customerRepository.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var result = await _customerRepository.AddAsync(customer);
            return _mapper.Map<CustomerDTO>(result);
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(UpdateCustomerDTO customerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerDto.Id);
            if (existingCustomer == null)
                throw new KeyNotFoundException($"Cliente con ID {customerDto.Id} no encontrado");

            _mapper.Map(customerDto, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return _mapper.Map<CustomerDTO>(existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(long id)
        {
            if (!await _customerRepository.ExistsAsync(id))
                return false;

            await _customerRepository.DeleteAsync(id);
            return true;
        }
    }
}
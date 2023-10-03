using AutoMapper;
using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenerictRepository<Customer> _generictRepository;
        private readonly ILoggerManager _logger;

        public CustomerService(IGenerictRepository<Customer> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateCustomer(CustomerDTO customer)
        {

            try
            {
                _logger.LogInfo("CustomerService: CreateCustomer");

                // Ánh xạ dữ liệu từ DTO (customer) thành thực thể (Customer)
                var newCustomer = new Customer

                {
                    CustomerName = customer.CustomerName,
                    ContactName = customer.ContactName,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country
                }
                ;
                _generictRepository.Create(newCustomer);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerService: CreateCustomer" + ex);
                return false;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                _logger.LogInfo("CustomerService: DeleteCustomer");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerService: DeleteCustomer" + ex);
                return false;
            }
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
          // Truy vấn danh sách các Customer từ thực thể Customer
            var customers = _generictRepository.GetAll().Select(customer => new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                ContactName = customer.ContactName,
                Address = customer.Address,
                City = customer.City,
                PostalCode = customer.PostalCode,
                Country = customer.Country
            }).ToList();
            
            return customers;
        }

        public CustomerDTO GetCustomerById(int id)
        {
            var customer = _generictRepository.GetById(id);
            // Ánh xạ dữ liệu từ thực thể (Customer) thành DTO (CustomerDTO)
            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                ContactName = customer.ContactName,
                Address = customer.Address,
                City = customer.City,
                PostalCode = customer.PostalCode,
                Country = customer.Country
            };
        }

        public bool UpdateCustomer(CustomerDTO customer)
        {
            try
            {
                _logger.LogInfo("CustomerService: UpdateCustomer");

                // Ánh xạ dữ liệu từ DTO (customer) thành thực thể (Customer)
                var updateCustomer = new Customer
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    ContactName = customer.ContactName,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country
                };

                _generictRepository.Update(updateCustomer);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerService: UpdateCustomer" + ex);
                return false;
            }
        }
    }
}

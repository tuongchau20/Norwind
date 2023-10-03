
using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenerictRepository<Employee> _generictRepository;
        private readonly ILoggerManager _logger;

        public EmployeeService(IGenerictRepository<Employee> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateEmployee(EmployeeDTO employee)
        {
            try
            {
                _logger.LogInfo("EmployeeService: CreateEmployee");

                var newEmployee = new Employee
                {
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    BirthDate = employee.BirthDate,
                    Photo = employee.Photo,
                    Notes = employee.Notes
                };

                _generictRepository.Create(newEmployee);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeService: CreateEmployee" + ex);
                return false;
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                _logger.LogInfo("EmployeeService: DeleteEmployee");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeService: DeleteEmployee" + ex);
                return false;
            }
        }

   

        public IEnumerable<EmployeeDTO> GetAllEmployee()
        {
            var employees = _generictRepository.GetAll().Select(employee => new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                BirthDate = employee.BirthDate,
                Photo = employee.Photo,
                Notes = employee.Notes
            }).ToList();

            return employees;
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            var employee = _generictRepository.GetById(id);
            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                BirthDate = employee.BirthDate,
                Photo = employee.Photo,
                Notes = employee.Notes
            };
        }

        public bool UpdateEmployee(EmployeeDTO employee)
        {
            try
            {
                _logger.LogInfo("EmployeeService: UpdateEmployee");

                var updateEmployee = new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    BirthDate = employee.BirthDate,
                    Photo = employee.Photo,
                    Notes = employee.Notes
                };

                _generictRepository.Update(updateEmployee);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("EmployeeService: UpdateEmployee" + ex);
                return false;
            }
        }
    }
}

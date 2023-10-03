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
    public class SupplierService : ISupplierService
    {
        private readonly IGenerictRepository<Supplier> _generictRepository;
        private readonly ILoggerManager _logger;

        public SupplierService(IGenerictRepository<Supplier> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateSupplier(SupplierDTO supplier)
        {
            try
            {
                _logger.LogInfo("SupplierService: CreateSupplier");

                var newSupplier = new Supplier
                {
                    SupplierName = supplier.SupplierName,
                    ContactName = supplier.ContactName,
                    Address = supplier.Address,
                    City = supplier.City,
                    PostalCode = supplier.PostalCode,
                    Country = supplier.Country,
                    Phone = supplier.Phone
                };

                _generictRepository.Create(newSupplier);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("SupplierService: CreateSupplier" + ex);
                return false;
            }
        }

        public bool DeleteSupplier(int id)
        {
            try
            {
                _logger.LogInfo("SupplierService: DeleteSupplier");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("SupplierService: DeleteSupplier" + ex);
                return false;
            }
        }

        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            var suppliers = _generictRepository.GetAll().Select(supplier => new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                Address = supplier.Address,
                City = supplier.City,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone
            }).ToList();

            return suppliers;
        }

        public SupplierDTO GetSupplierById(int id)
        {
            var supplier = _generictRepository.GetById(id);
            return new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                Address = supplier.Address,
                City = supplier.City,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone
            };
        }

        public bool UpdateSupplier(SupplierDTO supplier)
        {
            try
            {
                _logger.LogInfo("SupplierService: UpdateSupplier");

                var updateSupplier = new Supplier
                {
                    SupplierId = supplier.SupplierId,
                    SupplierName = supplier.SupplierName,
                    ContactName = supplier.ContactName,
                    Address = supplier.Address,
                    City = supplier.City,
                    PostalCode = supplier.PostalCode,
                    Country = supplier.Country,
                    Phone = supplier.Phone
                };

                _generictRepository.Update(updateSupplier);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("SupplierService: UpdateSupplier" + ex);
                return false;
            }
        }

     
    }
}

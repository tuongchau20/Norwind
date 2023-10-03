using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IGenerictRepository<Shipper> _generictRepository;
        private readonly ILoggerManager _logger;

        public ShipperService(IGenerictRepository<Shipper> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateShipper(ShipperDTO shipper)
        {
            try
            {
                _logger.LogInfo("ShipperService: CreateShipper");

                var newShipper = new Shipper
                {
                    ShipperName = shipper.ShipperName,
                    Phone = shipper.Phone
                };

                _generictRepository.Create(newShipper);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ShipperService: CreateShipper" + ex);
                return false;
            }
        }

        public bool DeleteShipper(int id)
        {
            try
            {
                _logger.LogInfo("ShipperService: DeleteShipper");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ShipperService: DeleteShipper" + ex);
                return false;
            }
        }

    

        public IEnumerable<ShipperDTO> GetAllShipper()
        {
            var shippers = _generictRepository.GetAll().Select(shipper => new ShipperDTO
            {
                ShipperId = shipper.ShipperId,
                ShipperName = shipper.ShipperName,
                Phone = shipper.Phone
            }).ToList();

            return shippers;
        }

        public ShipperDTO GetShipperById(int id)
        {
            var shipper = _generictRepository.GetById(id);
            return new ShipperDTO
            {
                ShipperId = shipper.ShipperId,
                ShipperName = shipper.ShipperName,
                Phone = shipper.Phone
            };
        }

        public bool UpdateShipper(ShipperDTO shipper)
        {
            try
            {
                _logger.LogInfo("ShipperService: UpdateShipper");

                var updateShipper = new Shipper
                {
                    ShipperId = shipper.ShipperId,
                    ShipperName = shipper.ShipperName,
                    Phone = shipper.Phone
                };

                _generictRepository.Update(updateShipper);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ShipperService: UpdateShipper" + ex);
                return false;
            }
        }
    }
}

using AutoMapper;
using Norwind.Data;
using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenerictRepository<Order> _generictRepository;
        private readonly ILoggerManager _logger;
        private readonly NorthwindContext _context;

        public OrderService(IGenerictRepository<Order> generictRepository, ILoggerManager logger, NorthwindContext context)
        {
            _generictRepository = generictRepository;
            _logger = logger;
            _context = context;
        }

        public bool CreateOrder(OrderDTO order)
        {
            try
            {
                _logger.LogInfo("OrderService: CreateOrder");

                var newOrder = new Order
                {
                    CustomerId = order.CustomerId,
                    EmployeeId = order.EmployeeId,
                    OrderDate = order.OrderDate,
                    ShipperId = order.ShipperId
                };

                _generictRepository.Create(newOrder);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderService: CreateOrder" + ex);
                return false;
            }
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                _logger.LogInfo("OrderService: DeleteOrder");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderService: DeleteOrder" + ex);
                return false;
            }
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var orders = _generictRepository.GetAll().Select(order => new OrderDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                ShipperId = order.ShipperId
            }).ToList();

            return orders;
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _generictRepository.GetById(id);
            return new OrderDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                ShipperId = order.ShipperId
            };
        }

        public bool UpdateOrder(OrderDTO order)
        {
            try
            {
                _logger.LogInfo("OrderService: UpdateOrder");

                var updateOrder = new Order
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    EmployeeId = order.EmployeeId,
                    OrderDate = order.OrderDate,
                    ShipperId = order.ShipperId
                };

                _generictRepository.Update(updateOrder);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderService: UpdateOrder" + ex);
                return false;
            }
        }
    }
}

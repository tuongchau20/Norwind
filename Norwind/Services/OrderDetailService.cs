using Norwind.DTO;
using Norwind.Helpers;
using Norwind.Models;
using Norwind.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Norwind.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IGenerictRepository<OrderDetail> _generictRepository;
        private readonly ILoggerManager _logger;

        public OrderDetailService(IGenerictRepository<OrderDetail> generictRepository, ILoggerManager logger)
        {
            _generictRepository = generictRepository;
            _logger = logger;
        }

        public bool CreateOrderDetail(OrderDetailDTO orderDetail)
        {
            try
            {
                _logger.LogInfo("OrderDetailService: CreateOrderDetail");

                var newOrderDetail = new OrderDetail
                {
                    OrderId = orderDetail.OrderId,
                    ProductId = orderDetail.ProductId,
                    Quantity = orderDetail.Quantity
                };

                _generictRepository.Create(newOrderDetail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderDetailService: CreateOrderDetail" + ex);
                return false;
            }
        }

        public bool CreateOrderDetails(OrderDetailDTO OrderDetails)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrderDetails(int id)
        {
            try
            {
                _logger.LogInfo("OrderDetailService: DeleteOrderDetail");
                _generictRepository.DeleteById(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderDetailService: DeleteOrderDetail" + ex);
                return false;
            }
        }

      

        public IEnumerable<OrderDetailDTO> GetAllOrderDetail()
        {
            var orderDetails = _generictRepository.GetAll().Select(orderDetail => new OrderDetailDTO
            {
                OrderDetailId = orderDetail.OrderDetailId,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            }).ToList();

            return orderDetails;
        }

        public IEnumerable<OrderDetailDTO> GetAllOrderDetails()
        {
            throw new NotImplementedException();
        }

        public OrderDetailDTO GetOrderDetailById(int id)
        {
            var orderDetail = _generictRepository.GetById(id);
            return new OrderDetailDTO
            {
                OrderDetailId = orderDetail.OrderDetailId,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            };
        }

        public OrderDetailDTO GetOrderDetailsById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderDetail(OrderDetailDTO orderDetail)
        {
            try
            {
                _logger.LogInfo("OrderDetailService: UpdateOrderDetail");

                var updateOrderDetail = new OrderDetail
                {
                    OrderDetailId = orderDetail.OrderDetailId,
                    OrderId = orderDetail.OrderId,
                    ProductId = orderDetail.ProductId,
                    Quantity = orderDetail.Quantity
                };

                _generictRepository.Update(updateOrderDetail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("OrderDetailService: UpdateOrderDetail" + ex);
                return false;
            }
        }

        public bool UpdateOrderDetails(OrderDetailDTO orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}

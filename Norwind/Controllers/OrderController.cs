using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Norwind.Data;
using Norwind.DTO;
using Norwind.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Norwind.Controllers
{
    public interface IOrderController
    {
        Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO);
        Task<IActionResult> DeleteOrder(int id);
        Task<ActionResult<OrderDTO>> GetOrder(int id);
        Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders();
        Task<IActionResult> UpdateOrder(int id, OrderDTO orderDTO);
    }

    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderController
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(NorthwindContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _context.Orders
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    EmployeeId = o.EmployeeId,
                    OrderDate = o.OrderDate,
                    ShipperId = o.ShipperId
                })
                .ToListAsync();

            foreach (var order in orders)
            {
                var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == order.CustomerId);
                if (customer != null)
                {
                    var customerDto = new CustomerDTO()
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName
                    };
                    order.Customer = customerDto;
                }
                var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == order.EmployeeId);
                if (employee != null)
                {
                    var employeeDto = new EmployeeDTO()
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName
                    };
                    order.Employee = employeeDto;
                }
                var shipper = _context.Shippers.FirstOrDefault(x => x.ShipperId == order.ShipperId);
                if (shipper != null)
                {
                    var shipperDto = new ShipperDTO()
                    {
                        ShipperId = shipper.ShipperId,
                        ShipperName = shipper.ShipperName,
                        Phone = shipper.Phone
                    };
                    order.Shipper = shipperDto;
                }

                var details = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).Select(s => new OrderDetailDTO()
                {
                    OrderDetailId = s.OrderDetailId,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList();
                order.Details = details;
                foreach (var detail in details)
                {
                    var product = _context.Products.FirstOrDefault(x => x.ProductId == detail.ProductId);
                    if (product != null)
                    {
                        var productDto = new ProductDTO()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Price = product.Price

                        };
                        detail.Product = productDto;
                    }

                }
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    EmployeeId = o.EmployeeId,
                    OrderDate = o.OrderDate,
                    ShipperId = o.ShipperId
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var order = new Order
            {
                CustomerId = orderDTO.CustomerId,
                EmployeeId = orderDTO.EmployeeId,
                OrderDate = orderDTO.OrderDate,
                ShipperId = orderDTO.ShipperId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO orderDTO)
        {
            var existingOrder = await _context.Orders.FindAsync(id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.CustomerId = orderDTO.CustomerId;
            existingOrder.EmployeeId = orderDTO.EmployeeId;
            existingOrder.OrderDate = orderDTO.OrderDate;
            existingOrder.ShipperId = orderDTO.ShipperId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

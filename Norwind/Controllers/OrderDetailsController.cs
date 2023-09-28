using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Norwind.Data;
using Norwind.DTO;
using Norwind.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norwind.Controllers
{
    [Route("api/orderdetails")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrderDetailController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetOrderDetails()
        {
            var orderDetails = await _context.OrderDetails
                .Select(od => new OrderDetailDTO
                {
                    OrderDetailId = od.OrderDetailId,
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity
                })
                .ToListAsync();

            return Ok(orderDetails);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailDTO>> GetOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails
            .Where(od => od.OrderDetailId == id)
            .Select(od => new OrderDetailDTO
            {
                OrderDetailId = od.OrderDetailId,
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity
            })
            .FirstOrDefaultAsync();

        if (orderDetail == null)
        {
            return NotFound();
        }

        return Ok(orderDetail);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetailDTO orderDetailDTO)
    {
        var orderDetail = new OrderDetail
        {
            OrderId = orderDetailDTO.OrderId,
            ProductId = orderDetailDTO.ProductId,
            Quantity = orderDetailDTO.Quantity
        };

        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderDetailId }, orderDetail);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailDTO orderDetailDTO)
    {
        var existingOrderDetail = await _context.OrderDetails.FindAsync(id);

        if (existingOrderDetail == null)
        {
            return NotFound();
        }

        existingOrderDetail.OrderId = orderDetailDTO.OrderId;
        existingOrderDetail.ProductId = orderDetailDTO.ProductId;
        existingOrderDetail.Quantity = orderDetailDTO.Quantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);
        if (orderDetail == null)
        {
            return NotFound();
        }

        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}

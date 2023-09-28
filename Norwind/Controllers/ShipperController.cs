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
    [Route("api/shippers")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<ShipperController> _logger;

        public ShipperController(NorthwindContext context, ILogger<ShipperController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDTO>>> GetShippers()
        {
            var shippers = await _context.Shippers
                .Select(s => new ShipperDTO
                {
                    ShipperId = s.ShipperId,
                    ShipperName = s.ShipperName,
                    Phone = s.Phone
                })
                .ToListAsync();

            return Ok(shippers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipperDTO>> GetShipper(int id)
    {
        var shipper = await _context.Shippers
            .Where(s => s.ShipperId == id)
            .Select(s => new ShipperDTO
            {
                ShipperId = s.ShipperId,
                ShipperName = s.ShipperName,
                Phone = s.Phone
            })
            .FirstOrDefaultAsync();

        if (shipper == null)
        {
            return NotFound();
        }

        return Ok(shipper);
    }

    [HttpPost]
    public async Task<ActionResult<Shipper>> CreateShipper(ShipperDTO shipperDTO)
    {
        var shipper = new Shipper
        {
            ShipperName = shipperDTO.ShipperName,
            Phone = shipperDTO.Phone
        };

        _context.Shippers.Add(shipper);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShipper), new { id = shipper.ShipperId }, shipper);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShipper(int id, ShipperDTO shipperDTO)
    {
        var existingShipper = await _context.Shippers.FindAsync(id);

        if (existingShipper == null)
        {
            return NotFound();
        }

        existingShipper.ShipperName = shipperDTO.ShipperName;
        existingShipper.Phone = shipperDTO.Phone;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipper(int id)
    {
        var shipper = await _context.Shippers.FindAsync(id);
        if (shipper == null)
        {
            return NotFound();
        }

        _context.Shippers.Remove(shipper);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}

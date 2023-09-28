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
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(NorthwindContext context, ILogger<SupplierController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
            var suppliers = await _context.Suppliers
                .Select(s => new SupplierDTO
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    ContactName = s.ContactName,
                    Address = s.Address,
                    City = s.City,
                    PostalCode = s.PostalCode,
                    Country = s.Country,
                    Phone = s.Phone
                })
                .ToListAsync();

            return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDTO>> GetSupplier(int id)
    {
        var supplier = await _context.Suppliers
            .Where(s => s.SupplierId == id)
            .Select(s => new SupplierDTO
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                ContactName = s.ContactName,
                Address = s.Address,
                City = s.City,
                PostalCode = s.PostalCode,
                Country = s.Country,
                Phone = s.Phone
            })
            .FirstOrDefaultAsync();

        if (supplier == null)
        {
            return NotFound();
        }

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<Supplier>> CreateSupplier(SupplierDTO supplierDTO)
    {
        var supplier = new Supplier
        {
            SupplierName = supplierDTO.SupplierName,
            ContactName = supplierDTO.ContactName,
            Address = supplierDTO.Address,
            City = supplierDTO.City,
            PostalCode = supplierDTO.PostalCode,
            Country = supplierDTO.Country,
            Phone = supplierDTO.Phone
        };

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSupplier), new { id = supplier.SupplierId }, supplier);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(int id, SupplierDTO supplierDTO)
    {
        var existingSupplier = await _context.Suppliers.FindAsync(id);

        if (existingSupplier == null)
        {
            return NotFound();
        }

        existingSupplier.SupplierName = supplierDTO.SupplierName;
        existingSupplier.ContactName = supplierDTO.ContactName;
        existingSupplier.Address = supplierDTO.Address;
        existingSupplier.City = supplierDTO.City;
        existingSupplier.PostalCode = supplierDTO.PostalCode;
        existingSupplier.Country = supplierDTO.Country;
        existingSupplier.Phone = supplierDTO.Phone;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound();
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}

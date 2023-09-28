using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Norwind.Data;
using Norwind.DTO;
using Norwind.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Norwind.Services;
using Norwind.Helpers;

namespace Norwind.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILoggerManager _logger;

        public CustomerController(ICustomerService customerService, ILoggerManager logger)
        {
            _customerService = customerService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                _logger.LogInfo("CustomerController : GetAll");
                var products = _customerService.GetAllCustomers();
                var rs = new Respone()
                {
                    status = 200,
                    message = "Successfully",
                    data = products
                };
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController : GetAll" + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            _logger.LogInfo("CustomerController : GetById");
            return Ok(_customerService.GetCustomerById(id));
        }
        [HttpPost]
        public IActionResult Post(CustomerDTO customer)
        {
            try
            {
                _logger.LogInfo("CustomerController : Post");
                _customerService.CreateCustomer(customer);
                var rs = new Respone()
                {
                    status = 200,
                    message = "Successfully",
                };
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController : Post" + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(CustomerDTO customer)
        {
            try
            {
                _logger.LogInfo("CustomerController : Put");
                _customerService.UpdateCustomer(customer);
                var rs = new Respone()
                {
                    status = 200,
                    message = "Successfully",
                };
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController : Put" + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInfo("CustomerController : Delete");
                _customerService.DeleteCustomer(id);
                var rs = new Respone()
                {
                    status = 200,
                    message = "Successfully",
                };
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController : Delete" + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

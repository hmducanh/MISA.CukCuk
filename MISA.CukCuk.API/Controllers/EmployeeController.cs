using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;   
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeService.GetAll();
            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet("maxEmployeeId")]
        public IActionResult GetMaxEmployeeCode()
        {
            string maxEmployeeId = _employeeService.GetMaximumEmployeeCode();
            if(!string.IsNullOrEmpty(maxEmployeeId))
            {
                return Ok(maxEmployeeId);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                var rowAffect = _employeeService.Insert(employee);
                if (rowAffect > 0)
                {
                    return StatusCode(201, rowAffect);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (CustomException Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Du lieu khong hop le vui long thu lai",
                    data = Ex.Data
                };
                return StatusCode(400, mes);
            }
            catch (Exception Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Co loi xay ra vui long lien he MISA de duoc tro giup",
                };
                return StatusCode(500, mes);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var rowAffect = _employeeService.Delete(id);
            if(rowAffect > 0)
            {
                return StatusCode(200, "Da xoa thanh cong");
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            var rowAffect = _employeeService.Update(employee);
            if (rowAffect > 0)
            {
                return StatusCode(200, "Da sua thanh cong");
            }
            else
            {
                return NoContent();
            }
        }
    }
}

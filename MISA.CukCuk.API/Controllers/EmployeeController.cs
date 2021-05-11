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
    /// <summary>
    /// Controller quan ly nhan vien
    /// </summary>
    /// CreatedBy : hmducanh (08/05/2021)
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        // tiem
        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;   
        }
        // lay tat ca nhan vien
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
        // lay ma nhan vien lon nhat + 1
        [HttpGet("maxEmployeeCode")]
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
        // gui 1 nhan vien len
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
        // xoa 1 nhan vien
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
        // chinh sua 1 nhan vien co san
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
        // kiem tra ma nhan vien co ton tai hay khong
        [HttpGet("{EmployeeCode}")]
        public IActionResult CheckEmployeeCodeExist(string EmployeeCode)
        {
            var check = _employeeService.CheckEmployeeCodeExist(EmployeeCode);
            if (check != null)
                return Ok(check);
            else
                return NoContent();
        }
        
        // lay mot nhan vien = id
        [HttpGet("GetById/{employeeId}")]
        public IActionResult GetById(Guid employeeId)
        {
            Employee employee = _employeeService.GetById(employeeId);
            if(employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NoContent();
            }
        }
    }
}

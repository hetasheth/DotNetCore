using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.BAL.Manager;
using HRM.Models.Employee;

namespace HRM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPI : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeAPI(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EmployeeDetails> lst = _employeeManager.GetAllEmployees();
            return Ok(lst);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            EmployeeDetails employee = _employeeManager.GetEmployee(id);
            if (employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeDetails employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is null.");
            }
            bool flag = _employeeManager.AddEmployee(employee);
            if (flag == true)
                return Ok();
            else
                return NoContent();
        }

        [HttpPut]
        public IActionResult Put([FromBody] EmployeeDetails employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is null.");
            }
            bool flag = _employeeManager.UpdateEmployee(employee);
            if (flag == true)
                return Ok();
            else
                return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            bool flag = _employeeManager.DeleteEmployee(id);
            if (flag == true)
                return Ok();
            else
                return NoContent();
        }
    }
}

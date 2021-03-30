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

        /// <summary>
        /// Get API : /api/Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<EmployeeDetails> lst = _employeeManager.GetAllEmployees();
            return Ok(lst);
        }

        /// <summary>
        /// Get by id API : /api/Employee/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Post API : /api/Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Put API : /api/Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete API : /api/Employee/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

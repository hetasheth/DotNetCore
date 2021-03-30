using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.WebClient.Employee;
using HRM.Web.Models;
using HRM.Web.Filters;
using HRM.Models.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HRM.Web.Controllers
{
    [ResponseHeaderFilter]
    [AuthorizationFilter]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeClient _employeeClient;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeClient employeeClient, ILogger<EmployeeController> logger)
        {
            _employeeClient = employeeClient;
            _logger = logger;
        }

        /// <summary>
        /// Employee List page
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 500)]
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Time"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            _logger.LogInformation("Employee List");
            return View(_employeeClient.GetEmployees());
        }

        public ActionResult EmployeeForm()
        {
            return View(new EmployeeRequestVM());
        }

        /// <summary>
        /// Fill form data for update employee details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            try
            {
                EmployeeRequestVM employeeRequestVM = new EmployeeRequestVM();
                EmployeeDetails employeeDetails = _employeeClient.GetEmployeeById(id);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDetails, EmployeeRequestVM>());
                var mapper = new Mapper(config);
                employeeRequestVM = mapper.Map<EmployeeRequestVM>(employeeDetails);
                return View("EmployeeForm", employeeRequestVM);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception : " + ex.Message);
                return View("EmployeeForm", new EmployeeRequestVM());
            }
        }

        /// <summary>
        /// Post/Put method for add/update employee
        /// </summary>
        /// <param name="employeeRequestVM"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(EmployeeRequestVM employeeRequestVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeRequestVM, EmployeeDetails>());
                    var mapper = new Mapper(config);
                    EmployeeDetails employeeDetails = mapper.Map<EmployeeDetails>(employeeRequestVM);
                    bool result = false;
                    if (employeeRequestVM.Id == 0)
                    {
                        result = _employeeClient.AddEmployee(employeeDetails);
                    }
                    else
                    {
                        result = _employeeClient.UpdateEmployee(employeeDetails);
                    }
                    if (result == true)
                        return RedirectToAction("Index");
                }
                return View("Error", "Shared");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception : " + ex.Message);
                return View("Error", "Shared");
            }
        }

        /// <summary>
        /// Delete method 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    bool result = _employeeClient.DeleteEmployee(id);
                    if (result == true)
                        return RedirectToAction("Index");
                }
                return View("Error", "Shared");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception : " + ex.Message);
                return View("Error", "Shared");
            }
        }
    }
}

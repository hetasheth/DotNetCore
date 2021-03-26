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

namespace HRM.Web.Controllers
{
    [ResponseHeaderFilter]  
    [AuthorizationFilter]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeClient _employeeClient;

        public EmployeeController(IEmployeeClient employeeClient)
        {
            _employeeClient = employeeClient;
        }
        
        [ResponseCache(Duration =500)]
        public IActionResult Index()
        {
            return View(_employeeClient.GetEmployees());
        }

        public ActionResult EmployeeForm()
        {
            return View(new EmployeeRequestVM());
        }

        public IActionResult Edit(int id)
        {
            EmployeeRequestVM employeeRequestVM = new EmployeeRequestVM();
            EmployeeDetails employeeDetails = _employeeClient.GetEmployeeById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDetails, EmployeeRequestVM>());
            var mapper = new Mapper(config);
            employeeRequestVM = mapper.Map<EmployeeRequestVM>(employeeDetails);
            return View("EmployeeForm", employeeRequestVM);
        }

        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(EmployeeRequestVM employeeRequestVM)
        {
            if(ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeRequestVM, EmployeeDetails>());
                var mapper = new Mapper(config);
                EmployeeDetails employeeDetails = mapper.Map<EmployeeDetails>(employeeRequestVM);
                bool result = false;
                if(employeeRequestVM.Id==0)
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
            return View("Error","Shared");
        }

        public IActionResult Delete(int id)
        {
            if(id!=0)
            {
                bool result = _employeeClient.DeleteEmployee(id);
                if(result==true)
                    return RedirectToAction("Index");
            }
            return View("Error", "Shared");
        }
    }
}

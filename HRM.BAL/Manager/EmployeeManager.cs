using System;
using System.Collections.Generic;
using System.Text;
using HRM.Models.Employee;
using HRM.DAL.Repository;
using HRM.DAL.Database;
using AutoMapper;

namespace HRM.BAL.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Get all the employee list and map it to employee details list
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDetails> GetAllEmployees()
        {
            List<EmployeeDetails> employeeDetailsList = new List<EmployeeDetails>();
            List<Employee> employeesList = _employeeRepository.GetAllEmployees();
            if (employeesList != null)
            {
                foreach (var items in employeesList)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDetails>());
                    var mapper = new Mapper(config);
                    EmployeeDetails employeeDetails = mapper.Map<EmployeeDetails>(items);
                    employeeDetailsList.Add(employeeDetails);
                }
                return employeeDetailsList;
            }
            return employeeDetailsList;
        }

        /// <summary>
        /// Get employee by id and map it to employee details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeDetails GetEmployee(long id)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            Employee employee = _employeeRepository.GetEmployee(id);
            if (employee != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDetails>());
                var mapper = new Mapper(config);
                employeeDetails = mapper.Map<EmployeeDetails>(employee);
            }
            return employeeDetails;
        }

        /// <summary>
        /// Map employee details to employee and pass it to add method
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeDetails employee)
        {
            Employee emp = new Employee();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDetails, Employee>());
            var mapper = new Mapper(config);
            emp = mapper.Map<Employee>(employee);
            return _employeeRepository.AddEmployee(emp);
        }

        /// <summary>
        /// Map employee details to employee and pass it to update method
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(EmployeeDetails employee)
        {
            Employee emp = new Employee();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDetails, Employee>());
            var mapper = new Mapper(config);
            emp = mapper.Map<Employee>(employee);
            return _employeeRepository.UpdateEmployee(emp);
        }

        /// <summary>
        /// Method to call delete method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(long id)
        {
            return _employeeRepository.DeleteEmployee(id);
        }

    }
}

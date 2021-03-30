using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HRM.DAL.Database;

namespace HRM.DAL.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }


        /// <summary>
        /// Method to get list of all employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllEmployees()
        {
            return _employeeContext.Employees.ToList();
        }

        /// <summary>
        /// Method to get employee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployee(long id)
        {
            return _employeeContext.Employees.FirstOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// Method for creating new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool AddEmployee(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            int x = _employeeContext.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }


        /// <summary>
        /// Method for updating existing employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee employee)
        {
            Employee emp = _employeeContext.Employees.FirstOrDefault(x => x.Id == employee.Id);
            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Salary = employee.Salary;
                emp.Department = employee.Department;
                emp.Designation = employee.Designation;
                emp.Email = employee.Email;
                emp.IsManager = employee.IsManager;
                emp.Manager = employee.Manager;
                emp.Phone = employee.Phone;
                int x = _employeeContext.SaveChanges();
                if (x > 0)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Method for deleting employee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(long id)
        {
            Employee employee = _employeeContext.Employees.Find(id);
            _employeeContext.Employees.Remove(employee);
            int x = _employeeContext.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }
    }
}

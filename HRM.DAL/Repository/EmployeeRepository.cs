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

        public List<Employee> GetAllEmployees()
        {
            return _employeeContext.Employees.ToList();
        }

        public Employee GetEmployee(long id)
        {
            return _employeeContext.Employees.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool AddEmployee(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            int x = _employeeContext.SaveChanges();
            if (x > 0)
                return true;
            return false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            Employee emp = _employeeContext.Employees.Where(x => x.Id == employee.Id).FirstOrDefault();
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

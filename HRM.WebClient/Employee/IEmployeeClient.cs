using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using HRM.Models.Employee;

namespace HRM.WebClient.Employee
{
    public interface IEmployeeClient
    {
        List<EmployeeDetails> GetEmployees();
        EmployeeDetails GetEmployeeById(int id);
        bool AddEmployee(EmployeeDetails employeeDetails);
        bool UpdateEmployee(EmployeeDetails employeeDetails);
        bool DeleteEmployee(int id);
    }
}

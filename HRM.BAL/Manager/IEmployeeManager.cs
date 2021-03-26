using System;
using System.Collections.Generic;
using System.Text;
using HRM.Models.Employee;

namespace HRM.BAL.Manager
{
    public interface IEmployeeManager
    {
        List<EmployeeDetails> GetAllEmployees();
        EmployeeDetails GetEmployee(long id);
        bool AddEmployee(EmployeeDetails employee);
        bool UpdateEmployee(EmployeeDetails employee);
        bool DeleteEmployee(long id);
    }
}

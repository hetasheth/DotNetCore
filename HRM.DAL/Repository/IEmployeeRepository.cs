using System;
using System.Collections.Generic;
using System.Text;
using HRM.DAL.Database;

namespace HRM.DAL.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        Employee GetEmployee(long id);
        bool AddEmployee(Employee employee);

        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(long id);
    }
}

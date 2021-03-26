using System;
using System.Collections.Generic;
using System.Text;
using HRM.DAL.Repository;
using HRM.DAL.Database;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.BAL.Helpers
{
    public static class DIHelper
    {
        public static void DependencyResolver(ref IServiceCollection services)
        {
            services.AddDbContext<EmployeeContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.DAL.Database
{
    public class EmployeeContext : DbContext
    {
        const string connectionString = "server = DSK-1087\\SQL2017;database=HRMDB;User ID = sa; password=Password12@;";
        public EmployeeContext() : base() { }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

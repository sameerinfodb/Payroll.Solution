using Payroll.Data.EntityFramework.Configuration;
using Payroll.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace Payroll.Data.EntityFramework
{
    internal class ApplicationDbContext : DbContext
    {
        internal ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        internal IDbSet<User> Users { get; set; }
        internal IDbSet<Role> Roles { get; set; }
        internal IDbSet<ExternalLogin> Logins { get; set; }
        internal IDbSet<Employee> Employees { get; set; }
        internal IDbSet<Department> Departments { get; set; }
        internal IDbSet<Salary> Salaries { get; set; }
        internal IDbSet<Payslip> Payslips { get; set; }
        internal IDbSet<TestEntity> TestEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new ClaimConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new SalaryConfiguration());
            modelBuilder.Configurations.Add(new PayslipConfiguration());
            modelBuilder.Configurations.Add(new TestEntityConfiguration());
        }
    }


    internal class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext("PayrollDbConnection");
        }
    }
}
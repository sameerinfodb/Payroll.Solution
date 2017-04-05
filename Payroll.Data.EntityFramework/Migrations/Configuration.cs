using Payroll.Domain.Entities;

namespace Payroll.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Payroll.Data.EntityFramework.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Payroll.Data.EntityFramework.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            Employee employee = new Employee()
            {
                FirstName = "Employee1",
                LastName = "Employee1",
                EmployeeCode = "M1012402",
                DateOfBirth = DateTime.Now.AddYears(-30),
                Status = 1,
                Id = Guid.NewGuid()
            };
            //Salary salary=new Salary()
            //{
            //    BasicSalary = 1000M,
            //    CostToCompany = 100000M,
            //    Employee = employee,
            //    EmployeeCode = employee.EmployeeCode,
            //    Gratuity = 1000M,
            //    HRA = 1000M,
            //    ID = Guid.NewGuid(),
            //    MedicalInsurance = 5000M,
            //    SpecialAllowance = 15000M
            //};
            //context.Salaries.AddOrUpdate(salary);

            //  context.Employees.AddOrUpdate(employee);
       

        }
    }
}

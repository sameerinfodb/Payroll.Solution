using Payroll.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Payroll.Data.EntityFramework.Configuration
{
    internal class EmployeeConfiguration :EntityTypeConfiguration<Employee>
    {
        internal EmployeeConfiguration()
        {
            ToTable("Employee");

            Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            HasKey(x => x.EmployeeCode)
                .Property(x => x.EmployeeCode)
                .HasColumnName("EmployeeCode")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.DateOfBirth)
                .HasColumnName("DOB")
                .HasColumnType("datetime2")
                .IsRequired();

            Property(x => x.DepartmentAssignedDate)
                .HasColumnName("DepartmentAssignedData")
                .HasColumnType("datetime2")
                .IsOptional();

            Property(x => x.Status)
              .HasColumnName("Status")
              .HasColumnType("int")
              .IsRequired();


            HasOptional<Department>(x => x.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(d => d.DepartmentCode);

            HasRequired(s => s.Salary)
              .WithRequiredPrincipal(e => e.Employee);



        }
    }
}

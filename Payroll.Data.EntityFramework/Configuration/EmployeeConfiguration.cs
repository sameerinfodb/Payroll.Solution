using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;

namespace Payroll.Data.EntityFramework.Configuration
{
    internal class EmployeeConfiguration :EntityTypeConfiguration<Employee>
    {
        internal EmployeeConfiguration()
        {
            ToTable("Employee");

            HasKey(x => x.EmployeeId)
                .Property(x => x.EmployeeId)
                .HasColumnName("EmployeeId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.EmployeeName)
                .HasColumnName("EmployeeName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            Property(x => x.DateOfBirth)
                .HasColumnName("DOB")
                .HasColumnType("datetime2");

            Property(x => x.DateOfJoin)
                .HasColumnName("DOJ")
                .HasColumnType("datetime2");

        }
    }
}

using Payroll.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Payroll.Data.EntityFramework.Configuration
{
    internal class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        internal DepartmentConfiguration()
        {
            ToTable("Department");

            Property(x => x.Id)
                .HasColumnName("ID")
                  .HasColumnOrder(1)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            HasKey(x => x.DepartmentCode)
               .Property(x => x.DepartmentCode)
               .HasColumnName("DepartmentCode")
               .HasColumnType("nvarchar")
               .HasMaxLength(50)
               .IsRequired();

            Property(x => x.DepartmentName)
               .HasColumnName("DepartmentName")
               .HasColumnType("nvarchar")
               .HasMaxLength(100)
               .IsRequired();


            Property(x => x.Status)
               .HasColumnName("Status")
               .HasColumnType("int")
               .IsRequired();

        }

    }
}

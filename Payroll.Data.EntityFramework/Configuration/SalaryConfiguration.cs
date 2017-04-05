using Payroll.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Payroll.Data.EntityFramework.Configuration
{
    internal class SalaryConfiguration:EntityTypeConfiguration<Salary>
    {

        internal SalaryConfiguration()
        {
            ToTable("Salaries");

            Property(x => x.ID)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("uniqueidentifier");

            HasKey(x => x.EmployeeCode)
                .Property(x => x.EmployeeCode)
                .HasColumnOrder(2)
               .HasColumnName("EmployeeCode")
               .HasColumnType("nvarchar")
               .HasMaxLength(50)
               .IsRequired();

            Property(x => x.BasicSalary)
                .HasColumnName("BasicPay")
                .HasPrecision(10, 2);

            Property(x => x.SpecialAllowance)
                .HasColumnName("SpecialAllowance")
                .HasPrecision(10, 2);

            Property(x => x.HRA)
                .HasColumnName("HRA")
                .HasPrecision(10, 2);


            Property(x => x.TravelAllowance)
                .HasColumnName("TravelAllowance")
                .HasPrecision(10, 2);

            Property(x => x.MedicalInsurance)
                .HasColumnName("MedicalInsurance")
                .HasPrecision(10, 2);

            Property(x => x.Gratuity)
                .HasColumnName("Gratuity")
                .HasPrecision(10, 2);

            Property(x => x.CostToCompany)
                .HasColumnName("CTC")
                .HasPrecision(10, 2);

            Property(x => x.Status)
             .HasColumnName("Status")
             .HasColumnType("int")
             .IsRequired();
        }
    }
}

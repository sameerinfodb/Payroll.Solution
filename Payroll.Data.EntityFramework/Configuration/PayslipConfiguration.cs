using Payroll.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Payroll.Data.EntityFramework.Configuration
{
    internal class PayslipConfiguration : EntityTypeConfiguration<Payslip>
    {
        internal PayslipConfiguration()
        {
            ToTable("Payslips");

            Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .HasColumnType("uniqueidentifier");


            HasKey(x => x.PayslipCode)
                .Property(x => x.PayslipCode)
                .HasColumnOrder(2)
                .HasColumnName("PayslipCode")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);
             

            Property(x => x.EmployeeCode)
                .HasColumnOrder(3)
               .HasColumnName("EmployeeCode")
               .HasColumnType("nvarchar")
               .HasMaxLength(50)
               .IsRequired();


            Property(x => x.PayslipDate)
                .HasColumnOrder(4)
               .HasColumnName("PayslipDate")
               .HasColumnType("datetime2");
         

            Property(x => x.BasicAmount)
              .HasColumnName("BasicAmount")
              .HasPrecision(10, 2);
            

            Property(x => x.HRAAmount)
                .HasColumnName("HRAAmount")
                 .HasPrecision(10, 2);
                 


            Property(x => x.SpecialAllowanceAmount)
               .HasColumnName("SpecialAllowanceAmount")
                .HasPrecision(10, 2);
            


            Property(x => x.EmployeeProvidentFund)
               .HasColumnName("EmployeeProvidentFund")
                .HasPrecision(10, 2);
     

            Property(x => x.OtherDeduction)
              .HasColumnName("OtherDeduction")
               .HasPrecision(10, 2);
      

            Property(x => x.TotalWorkingDays)
                .HasColumnName("TotalWorkingDays")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.TotalLossOfPay)
              .HasColumnName("TotalLossOfPay")
              .HasColumnType("int");


            Property(x => x.GrossSalary)
                .HasColumnName("GrossSalary")
                .HasPrecision(10, 2);

            Property(x => x.NetSalary)
               .HasColumnName("NetSalary")
                 .HasPrecision(10, 2);

            HasRequired<Employee>(e => e.Employee)
                .WithMany(e => e.Payslips)
                .HasForeignKey(x => x.EmployeeCode);


        }
    }
}

using System;

namespace Payroll.Domain.Entities
{
    public class Salary
    {
        public Guid ID { get; set; }
        public string EmployeeCode { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal HRA { get; set; }
        public decimal TravelAllowance { get; set; }
        public decimal MedicalInsurance { get; set; }
        public decimal Gratuity { get; set; }
        public decimal CostToCompany { get; set; }
        public int Status { get; set; }

        public virtual Employee Employee { get; set; }


    }
}

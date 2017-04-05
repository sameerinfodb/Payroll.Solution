using System;

namespace Payroll.Domain.Entities
{
    public class Payslip
    {
        private Employee _employee;
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public string PayslipCode { get; set; }
        public DateTime PayslipDate { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal HRAAmount { get; set; }
        public decimal SpecialAllowanceAmount { get; set; }
        public decimal EmployeeProvidentFund { get; set; }
        public decimal OtherDeduction { get; set; }
        public int TotalWorkingDays { get; set; }
        public int TotalLossOfPay { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }

        #region Navigation Property

        public virtual Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                EmployeeCode = value.EmployeeCode;
            }
        }

        #endregion



    }
}

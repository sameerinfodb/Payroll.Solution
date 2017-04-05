using System;
using System.Collections.Generic;

namespace Payroll.Domain.Entities
{
    public class Employee
    {
        #region Private Fields

        private Department _department;
        
        private ICollection<Payslip> _payslips;
        #endregion

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DepartmentAssignedDate { get; set; }
        public string DepartmentCode { get; set; }
      
        public virtual Salary Salary { get; set; }
        public int Status { get; set; }





        #region Navigation Properties

        

        public virtual Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                DepartmentCode = value.DepartmentCode;
            }
        }
        public ICollection<Payslip> Payslips
        {
            get { return _payslips ?? (_payslips = new List<Payslip>()); }
            set { _payslips = value; }
        }
        #endregion
    }
}


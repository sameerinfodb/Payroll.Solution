using System;
using System.Collections.Generic;

namespace Payroll.Domain.Entities
{
    public class Department
    {
        #region Fields
        private ICollection<Employee> _employees;
        #endregion

        public Guid Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int Status { get; set; }

        #region Navigation Properties
        public ICollection<Employee> Employees
        {
            get { return _employees ?? (_employees = new List<Employee>()); }
            set { _employees = value; }
        }
        #endregion
    }
}


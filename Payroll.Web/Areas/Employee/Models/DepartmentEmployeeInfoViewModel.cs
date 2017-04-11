using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Web.Areas.Employee.Models
{
    public class DepartmentEmployeeInfoViewModel
    {
        public DepartmentInfoViewModel departmentInfoViewModel { get; set; }
        public List<EmployeeInfoViewModel> employeeInfoViewModel { get; set; }
    }
}
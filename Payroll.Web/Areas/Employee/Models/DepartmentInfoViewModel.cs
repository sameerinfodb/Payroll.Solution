using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Web.Areas.Employee.Models
{
    public class DepartmentInfoViewModel
    {
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Department Assigned Date")]
        public DateTime DepartmentAssignedDate { get; set; }

        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }
    }
}
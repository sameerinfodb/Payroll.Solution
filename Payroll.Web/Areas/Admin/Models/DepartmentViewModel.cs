using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Web.Areas.Admin.Models
{
    public class DepartmentViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Currently only two status are supported i.e. 0- Not Active,1-Active")]
        public int Status { get; set; }
    }
}
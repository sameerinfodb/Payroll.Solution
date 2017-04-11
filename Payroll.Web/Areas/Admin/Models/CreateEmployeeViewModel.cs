using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Web.Areas.Admin.Models
{
    public class CreateEmployeeViewModel
    {
        
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Basic Salary")]
        [Range(1, 9999999999.99, ErrorMessage = "Invalid Basic Salary")]
        public decimal BasicSalary { get; set; }

        [Required]
        [Display(Name = "Special Allowance")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Special Allowance")]
        [Range(0, 9999999999.99, ErrorMessage = "Invalid Special Allowance")]
        public decimal SpecialAllowance { get; set; }

        [Required]
        [Display(Name = "HRA")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid HRA")]
        [Range(0, 9999999999.99, ErrorMessage = "Invalid HRA")]
        public decimal HRA { get; set; }

        [Required]
        [Display(Name = "Travel Allowance")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Travel Allowance")]
        [Range(0, 9999999999.99, ErrorMessage = "Invalid Travel Allowance")]
        public decimal TravelAllowance { get; set; }

        [Required]
        [Display(Name = "Medical Insurance")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Medical Insurance")]
        [Range(0, 9999999999.99, ErrorMessage = "Invalid Medical Insurance")]
        public decimal MedicalInsurance { get; set; }

        [Required]
        [Display(Name = "Gratuity")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Gratuity Amount")]
        [Range(0, 9999999999.99, ErrorMessage = "Invalid Gratuity Amount")]
        public decimal Gratuity { get; set; }

        [Required]
        [Display(Name = "Cost To Company")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Cost To Company Amount")]
        [Range(0, 9999999999.99, ErrorMessage = "Invalid Cost To Company Amount")]
        public decimal CostToCompany { get; set; }

        public DateTime DepartmentAssignedDate { get; set; }

     
        [Display(Name = "Department")]
        public string DepartmentCode { get; set; }

        public int Status { get; set; }
        public Guid SalaryId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Payroll.Web.Areas.Admin.Models
{
    public class SalaryViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string EmployeeCode { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Basic Salary")]
        [Range(0, 9999999999.99,ErrorMessage = "Invalid Basic Salary")]
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

        public int Status { get; set; }

    }
}
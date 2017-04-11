using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Payroll.Domain;
using Payroll.Domain.Entities;
using Payroll.Web.Areas.Employee.Models;
using Payroll.Web.Identity;

namespace Payroll.Web.Areas.Employee.Controllers
{
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser, Guid> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork, UserManager<IdentityUser, Guid> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        // GET: Employee/Home
        public ActionResult Index()
        {
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        public ActionResult DisplayPayslip()
        {
            IdentityUser user = _userManager.FindById(new Guid(HttpContext.User.Identity.GetUserId()));
            Domain.Entities.Employee employee = _unitOfWork.EmployeeRepository.FindById(user.EmployeeCode);
            List<Payslip> payslip =
                _unitOfWork.PayslipRepository.FindAllPayslipsByEmployeeCod(employee.EmployeeCode);
            return View(payslip);
        }

        public ActionResult DisplayDepartmentDetail(int filter = 0)
        {
            List<Domain.Entities.Employee> employees;
            IdentityUser user = _userManager.FindById(new Guid(HttpContext.User.Identity.GetUserId()));
            Domain.Entities.Employee employee = _unitOfWork.EmployeeRepository.FindById(user.EmployeeCode);
            if (employee.DepartmentCode == null)
            {
                return View("MessageDisplayView",model: "Department Details not available!!!");
            }
            else
            {
                Domain.Entities.Department department = _unitOfWork.DepartmentRepository.FindById(employee.DepartmentCode);

                return View(
                    new DepartmentInfoViewModel()
                    {
                        DepartmentCode = department.DepartmentCode,
                        DepartmentName = department.DepartmentName,
                        DepartmentAssignedDate = employee.DepartmentAssignedDate,
                        EmployeeCode = employee.EmployeeCode
                    });
            }
            
        }

        public ActionResult GetEmployeesByDepartment(string departmentCode,string employeeCode)
        {
            if(string.IsNullOrEmpty(departmentCode))
                return PartialView("_EmployeeListPartial",model: "Department Details not available");


            List<Domain.Entities.Employee> employees = _unitOfWork.EmployeeRepository.GetEmployeesByDeparmentId(departmentCode);
            if (employees.Count == 0)
            {
                return PartialView("_MeesageDisplayPartial", "Not records found ....");
            }
            return PartialView("_EmployeeListPartial", employees);
        }
        public ActionResult GetEmployeesByDeptDOJ(string departmentCode, DateTime doj)
        {
            if (string.IsNullOrEmpty(departmentCode))
                return PartialView("_EmployeeListPartial", model: "Department Details not available");

            List<Domain.Entities.Employee> employees = _unitOfWork.EmployeeRepository.GetEmployeesByDeptAssignedDate(departmentCode, doj);
            if (employees.Count == 0)
            {
                return PartialView("_MeesageDisplayPartial", "Not records found ....");
            }

            return PartialView("_EmployeeListPartial", employees);
        }
        public ActionResult PaySlipDetails(string payslipcode)
        {
            var payslip = _unitOfWork.PayslipRepository.FindById(payslipcode);

            return PartialView("_PayslipDetailPartial", payslip);
        }
    }
}
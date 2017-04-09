using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Payroll.Domain;
using Payroll.Domain.Entities;
using Payroll.Web.Areas.Admin.Models;

namespace Payroll.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/Employee
        public ActionResult Index()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return View(employees);
        }

        public ActionResult Create()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            SelectList deparSelectList = new SelectList(departments, "DepartmentCode", "DepartmentName");
            ViewBag.Departments = deparSelectList;
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateEmployeeViewModel createEmployeeViewModel)
        {
            decimal totalSalary = createEmployeeViewModel.BasicSalary;
            totalSalary += createEmployeeViewModel.HRA;
            totalSalary += createEmployeeViewModel.TravelAllowance;
            totalSalary += createEmployeeViewModel.MedicalInsurance;
            totalSalary += createEmployeeViewModel.Gratuity;
            totalSalary += createEmployeeViewModel.SpecialAllowance;

            if (createEmployeeViewModel.CostToCompany != totalSalary)
            {
                ModelState.AddModelError("","Invalid CTC.");
            }

            if (ModelState.IsValid)
            {
                if (createEmployeeViewModel.DepartmentCode != null)
                {
                    createEmployeeViewModel.DepartmentAssignedDate = DateTime.Now;
                }
                createEmployeeViewModel.Status = 1;

                return RedirectToAction("Index", "Employee", new {area = "Admin"});
            }
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            SelectList deparSelectList = new SelectList(departments, "DepartmentCode", "DepartmentName");
            ViewBag.Departments = deparSelectList;
            return View(createEmployeeViewModel);
        }

        public ActionResult Edit(string employeeCode)
        {
            var employee = _unitOfWork.EmployeeRepository.FindById(employeeCode);
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            SelectList deparSelectList = new SelectList(departments, "DepartmentCode", "DepartmentName", employee.DepartmentCode);

            ViewBag.Departments = deparSelectList;

            EmployeeViewModel vm = MapEntityToViewModel(employee);

            return View(vm);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {

                Domain.Entities.Department department = _unitOfWork.DepartmentRepository.FindById(employeeViewModel.DepartmentCode);
                Domain.Entities.Employee employee = MapViewModelToEntity(employeeViewModel, department);
                _unitOfWork.EmployeeRepository.Update(employee);
                _unitOfWork.SaveChanges();
              return  RedirectToAction("Index", "Employee", new { area = "Admin" });
            }

            var departments = _unitOfWork.DepartmentRepository.GetAll();
            SelectList deparSelectList = new SelectList(departments, "DepartmentCode", "DepartmentName", employeeViewModel.DepartmentCode);
            return View();
        }

        private Domain.Entities.Employee MapViewModelToEntity(EmployeeViewModel employeeViewModel, Department department)
        {
            Domain.Entities.Employee employee;
            if (department != null)
            {
                employee = new Domain.Entities.Employee()
                {
                    Id = employeeViewModel.Id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    EmployeeCode = employeeViewModel.EmployeeCode,
                    DateOfBirth = employeeViewModel.DateOfBirth,
                    Department = department,
                    DepartmentAssignedDate = DateTime.Now,
                    Status = employeeViewModel.Status
                };
            }
            else
            {
                employee = new Domain.Entities.Employee()
                {
                    Id = employeeViewModel.Id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    EmployeeCode = employeeViewModel.EmployeeCode,
                    DateOfBirth = employeeViewModel.DateOfBirth,
                    DepartmentAssignedDate =DateTime.MinValue,
                    Status = employeeViewModel.Status
                };
            }


            return employee;

        }

        private EmployeeViewModel MapEntityToViewModel(Domain.Entities.Employee employee)
        {
            return new EmployeeViewModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeCode = employee.EmployeeCode,
                DateOfBirth = employee.DateOfBirth,
                DepartmentCode = employee.DepartmentCode,
                DepartmentAssignedDate = employee.DepartmentAssignedDate,
                Status = employee.Status
            };
        }
    }
}
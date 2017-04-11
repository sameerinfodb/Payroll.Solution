using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Payroll.Domain;
using Payroll.Domain.Entities;
using Payroll.Web.Areas.Admin.Models;
using Payroll.Web.Identity;

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
            //if only active employee need to be displayed
           //var activeEmployees= employees.Where(e => e.Status == 1);
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
            Department department=null;
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            SelectList deparSelectList = new SelectList(departments, "DepartmentCode", "DepartmentName");
            ViewBag.Departments = deparSelectList;

            var totalSalary = TotalSalary(createEmployeeViewModel);

            if (createEmployeeViewModel.CostToCompany != totalSalary)
            {
                ModelState.AddModelError("","Invalid CTC.");
            }

            if (EmployeeExists(createEmployeeViewModel.EmployeeCode))
            {
                var errorMessage = String.Format("{0} Employee code already exists",
                    createEmployeeViewModel.EmployeeCode);
                ModelState.AddModelError("", errorMessage);
            }

            if (ModelState.IsValid)
            {
                if (createEmployeeViewModel.DepartmentCode != null)
                {
                    createEmployeeViewModel.DepartmentAssignedDate = DateTime.Now;
                    department = _unitOfWork.DepartmentRepository.FindById(createEmployeeViewModel.DepartmentCode);
                }
                createEmployeeViewModel.Status = 1;
                createEmployeeViewModel.Id = Guid.NewGuid();
                createEmployeeViewModel.SalaryId = Guid.NewGuid();


                Domain.Entities.Employee employee = MapEmployeeViewModelToEntity(createEmployeeViewModel,department);
                Domain.Entities.Salary employeeSalary = MapSalaryViewModelToEntity(createEmployeeViewModel, employee);
                try
                {

                    _unitOfWork.EmployeeRepository.Add(employee);
                    _unitOfWork.SalaryRepository.Add(employeeSalary);
                    _unitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(createEmployeeViewModel);
                }
                
                return RedirectToAction("Index", "Employee", new {area = "Admin"});
            }
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
            ViewBag.Departments = deparSelectList;
            return View(employeeViewModel);
        }

        #region Helper Method

        private Salary MapSalaryViewModelToEntity(CreateEmployeeViewModel createEmployeeViewModel, Domain.Entities.Employee employee)
        {
            Domain.Entities.Salary salary = new Salary()
            { 
                ID = createEmployeeViewModel.SalaryId,
                Employee = employee,
                BasicSalary = createEmployeeViewModel.BasicSalary,
                CostToCompany = createEmployeeViewModel.CostToCompany,
                HRA = createEmployeeViewModel.HRA,
                Gratuity = createEmployeeViewModel.Gratuity,
                MedicalInsurance = createEmployeeViewModel.MedicalInsurance,
                SpecialAllowance = createEmployeeViewModel.SpecialAllowance,
                Status = createEmployeeViewModel.Status

            };

            return salary;

        }

        private Domain.Entities.Employee MapEmployeeViewModelToEntity(CreateEmployeeViewModel createEmployeeViewModel, Department department)
        {
            Domain.Entities.Employee employee;
            if (department != null)
            {
                employee = new Domain.Entities.Employee()
                {
                    Id = createEmployeeViewModel.Id,
                    FirstName = createEmployeeViewModel.FirstName,
                    LastName = createEmployeeViewModel.LastName,
                    EmployeeCode = createEmployeeViewModel.EmployeeCode,
                    DateOfBirth = createEmployeeViewModel.DateOfBirth,
                    Department = department,
                    DepartmentAssignedDate = DateTime.Now,
                    Status = createEmployeeViewModel.Status
                };
            }
            else
            {
                employee = new Domain.Entities.Employee()
                {
                    Id = createEmployeeViewModel.Id,
                    FirstName = createEmployeeViewModel.FirstName,
                    LastName = createEmployeeViewModel.LastName,
                    EmployeeCode = createEmployeeViewModel.EmployeeCode,
                    DateOfBirth = createEmployeeViewModel.DateOfBirth,
                    DepartmentAssignedDate = DateTime.MinValue,
                    Status = createEmployeeViewModel.Status
                };
            }


            return employee;
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
                    DepartmentAssignedDate = DateTime.MinValue,
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

        private bool EmployeeExists(string employeeCode)
        {
            Domain.Entities.Employee employee = _unitOfWork.EmployeeRepository.FindById(employeeCode);

            if (employee != null)
                return true;
            else
            {
                return false;
            }
        }

        private static decimal TotalSalary(CreateEmployeeViewModel createEmployeeViewModel)
        {
            decimal totalSalary = createEmployeeViewModel.BasicSalary;
            totalSalary += createEmployeeViewModel.HRA;
            totalSalary += createEmployeeViewModel.TravelAllowance;
            totalSalary += createEmployeeViewModel.MedicalInsurance;
            totalSalary += createEmployeeViewModel.Gratuity;
            totalSalary += createEmployeeViewModel.SpecialAllowance;
            return totalSalary;
        } 
        #endregion

        public ActionResult Details(string employeeCode)
        {
            var employee = _unitOfWork.EmployeeRepository.FindById(employeeCode);
          //  var activeEmployee = employee.Status == 1 ? employee : null;
            return PartialView("_EmployeeDetailPartial", employee);
        }
      
        [HttpPost]
        public ActionResult Delete(string employeecode)
        {
            var employee = _unitOfWork.EmployeeRepository.FindById(employeecode);
            if (employee != null)
            {
                employee.Status = 0;
            }
            else
            {
                //TODO: Display alert to users 
            }

            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
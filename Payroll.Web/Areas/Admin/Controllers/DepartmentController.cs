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
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin/Department
        public ActionResult Index()
        {
            var deparments = _unitOfWork.DepartmentRepository.GetAll();
            var activeDepartments = deparments.Where(e => e.Status == 1);
            return View(activeDepartments);
        }

        public ActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Save(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                departmentViewModel.Id = Guid.NewGuid();
                departmentViewModel.Status = 1;
                Department department = MapViewModelToEntity(departmentViewModel);

                try
                {
                    _unitOfWork.DepartmentRepository.Add(department);
                    _unitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(departmentViewModel);
                }

                return RedirectToAction("Index", "Department", new { area = "Admin" });
            }
            return View(departmentViewModel);
        }

        private Department MapViewModelToEntity(DepartmentViewModel departmentViewModel)
        {
            Department department = new Department()
            {
                Id = departmentViewModel.Id,
                DepartmentCode = departmentViewModel.DepartmentCode,
                DepartmentName =  departmentViewModel.DepartmentName,
                Status = departmentViewModel.Status
            };
            return department;
        }

        public ActionResult Details(string departmentcode)
        {
            var deparment = _unitOfWork.DepartmentRepository.FindById(departmentcode);
           // var activeDepartment = deparment.Status == 1 ? deparment : null;
            return PartialView("_DepartmentDetailPartial", deparment);
        }

        public ActionResult Edit(string departmentcode)
        {

            var deparment = _unitOfWork.DepartmentRepository.FindById(departmentcode);
            DepartmentViewModel departmentViewModel = MapEntityToViewModel(deparment);
            return View(departmentViewModel);
        }

        private DepartmentViewModel MapEntityToViewModel(Department deparment)
        {
            return new DepartmentViewModel()
            {
                Id = deparment.Id,
                DepartmentCode = deparment.DepartmentCode,
                DepartmentName = deparment.DepartmentName

            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult Update(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                Domain.Entities.Department department = MapViewModelToEntity(departmentViewModel);
                _unitOfWork.DepartmentRepository.Update(department);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index", "Department", new { area = "Admin" });
            }

            return View(departmentViewModel);
        }
    }
}

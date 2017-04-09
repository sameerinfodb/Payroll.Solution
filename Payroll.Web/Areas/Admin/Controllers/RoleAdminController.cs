using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Payroll.Domain;
using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;
using Payroll.Web.Identity;


namespace Payroll.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        private RoleStore _roleStore;
       public  RoleAdminController(RoleStore roleStore)
        {
            _roleStore = roleStore;
        }
        // GET: Admin/RoleAdmin
        public ActionResult Index()
        {


            var roles = _roleStore.Roles;


            return View(roles);
        }

        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                _roleStore.CreateAsync(role).Wait();
                return RedirectToAction("Index");
            }

            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> Delete(IdentityRole role)
        {
           
            _roleStore.DeleteAsync(role).Wait();

            return RedirectToAction("Index");
        }


    }
}
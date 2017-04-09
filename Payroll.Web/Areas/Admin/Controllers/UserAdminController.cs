using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Payroll.Web.Areas.Admin.Models;
using Payroll.Web.Identity;

namespace Payroll.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private readonly UserManager<IdentityUser, Guid> _userManager;
        private readonly RoleStore _roleStore;

        public UserAdminController(UserManager<IdentityUser, Guid> userManager,RoleStore roleStore)
        {
            _userManager= userManager;
            _roleStore = roleStore;


        }
        // GET: Admin/UserAdmin
        public ActionResult Index()
        {


            var users = _userManager.Users.ToList();
            
            UserViewModel vm= new UserViewModel(_userManager);
            
            List<UserViewModel> list = vm.ConvertIdentityUserToUserViewModel(users);

            return View(list);
        }

        public ActionResult Create()
        {
            
           ViewBag.Roles = new SelectList(_roleStore.Roles, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //check if the we have user with same employee code
                if (_userManager.Users.Count(r => r.EmployeeCode==userViewModel.EmployeeCode)>0)
                {
                    AddErrors(new IdentityResult("EmployeeCode already taken."));
                }
                else
                {
                    var newIdentityUser = new IdentityUser(userViewModel.UserName, userViewModel.EmployeeCode);
                    IdentityResult result =await _userManager.CreateAsync(newIdentityUser,userViewModel.Password);
                    if (result.Succeeded)
                    {
                        IdentityRole role = await _roleStore.FindByIdAsync(new Guid(userViewModel.RoleId));
                        result = await _userManager.AddToRoleAsync(newIdentityUser.Id, role.Name);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                    else
                    {
                        AddErrors(result);
                    }

                }

            }
            ViewBag.Roles = new SelectList(_roleStore.Roles, "Id", "Name",userViewModel.RoleId);
            return View(userViewModel);
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Delete(UserViewModel userViewModel)
        {
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Payroll.Web.Identity;

namespace Payroll.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        private UserManager<IdentityUser, Guid> _userManager;
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

       [Display(Name="Role")]
        public string RoleId { get; set; }
        
        public virtual List<string> RoleNames { get; set; }

        public UserViewModel()
        {
            
        }
        public UserViewModel(UserManager<IdentityUser,Guid> userManager)
        {
            this._userManager = userManager;
        }

        public UserViewModel(Guid id,string name,List<string> roles,string password,string confirmPassword, UserManager<IdentityUser, Guid> userManager)
        {
            this.Id = id;
            this.UserName = name;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
            this.EmployeeCode = string.Empty;
            this.RoleNames= roles;
            this._userManager = userManager;
        }

        public UserViewModel(Guid id, string name, List<string> roles, string password, string confirmPassword, string employeeCode, UserManager<IdentityUser, Guid> userManager) :this(id, name, roles,password,confirmPassword, userManager)
        {
            this.EmployeeCode = employeeCode;
        }

       public List<UserViewModel> ConvertIdentityUserToUserViewModel(List<IdentityUser> users)
        {
            List<UserViewModel> userViewModels =
                users.ConvertAll(new Converter<IdentityUser, UserViewModel>(ConvertIdentityUserToViewModel));

            return userViewModels;
        }

        private UserViewModel ConvertIdentityUserToViewModel(IdentityUser input)
        {
            UserViewModel vm;
            List<string> role = _userManager.GetRolesAsync(input.Id).Result.ToList();
            if (String.IsNullOrEmpty(input.EmployeeCode))
            { 
                vm= new UserViewModel(input.Id,input.UserName, role, string.Empty, string.Empty, _userManager);
            }
            else
            {
                vm = new UserViewModel(input.Id, input.UserName, role, string.Empty, string.Empty, input.EmployeeCode,_userManager);
            }
            return vm;
        }

       
    }
}
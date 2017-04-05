using Microsoft.AspNet.Identity;
using System;

namespace Payroll.Web.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid();
        }

        public IdentityUser(string userName,string employeeCode)
            : this()
        {
            this.UserName = userName;
            this.EmployeeCode = employeeCode;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public string EmployeeCode { get; set; }
    }
}
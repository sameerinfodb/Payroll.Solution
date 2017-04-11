using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        internal EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Employee> GetEmployeesByDeparmentId(string departmentCode)
        {
            return new List<Employee>(Set.Where(p => p.DepartmentCode == departmentCode));
        }

        public List<Employee> GetEmployeesByDeptAssignedDate(string departmentCode,DateTime assignedDateTime)
        {
            return new List<Employee>(Set.Where(p=>p.DepartmentCode== departmentCode).Where(p=>p.DepartmentAssignedDate.CompareTo(assignedDateTime) > 0 ));
        }
    }
}


using System;
using System.Collections.Generic;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Repositories
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        List<Employee> GetEmployeesByDeparmentId(string departmentCode);
        List<Employee> GetEmployeesByDeptAssignedDate(string deparmentCode,DateTime assignedDateTime);

    }
}

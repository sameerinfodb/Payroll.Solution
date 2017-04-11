using System.Collections.Generic;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Repositories
{
    public interface IPayslipRepository:IRepository<Payslip>
    {
        List<Payslip> FindAllPayslipsByEmployeeCod(string EmployeeCode);
    }
}

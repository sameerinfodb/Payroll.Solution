using System.Collections.Generic;
using System.Linq;
using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class PayslipRepository:Repository<Payslip>,IPayslipRepository
    {
        internal PayslipRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Payslip> FindAllPayslipsByEmployeeCod(string employeeCode)
        {
            return new List<Payslip>(Set.Where(p => p.EmployeeCode == employeeCode));
        }
    }
}

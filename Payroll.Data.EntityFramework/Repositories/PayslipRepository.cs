using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class PayslipRepository:Repository<Payslip>,IPayslipRepository
    {
        internal PayslipRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

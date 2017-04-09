using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class SalaryRepository:Repository<Salary>,ISalaryRepository
    {
        internal SalaryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

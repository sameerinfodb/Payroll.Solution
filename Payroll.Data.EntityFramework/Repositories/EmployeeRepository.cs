using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        internal EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

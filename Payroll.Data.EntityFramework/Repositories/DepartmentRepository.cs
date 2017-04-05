using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

﻿using Payroll.Domain.Entities;
using Payroll.Domain.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Data.EntityFramework.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        internal UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public User FindByUserName(string username)
        {
            return Set.FirstOrDefault(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }
        public User FindByEmployeeCode(string employeeCode)
        {
            return Set.FirstOrDefault(x => x.EmployeeCode == employeeCode);
        }

        public Task<User> FindByEmployeeCodeAsync(string employeeCode)
        {
            return Set.FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);
        }

        public Task<User> FindByEmployeeCodeAsync(System.Threading.CancellationToken cancellationToken, string employeeCode)
        {
            return Set.FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode, cancellationToken);
        }
    }
}
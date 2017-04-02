﻿using System.Threading;
using System.Threading.Tasks;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Repositories
{

    public interface IUserRepository : IRepository<User>
    {
        User FindByUserName(string username);
        Task<User> FindByUserNameAsync(string username);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);
    }

}
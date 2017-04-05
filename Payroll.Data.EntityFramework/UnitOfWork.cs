using Payroll.Data.EntityFramework.Repositories;
using Payroll.Domain;
using Payroll.Domain.Repositories;
using System.Threading.Tasks;

namespace Payroll.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private IExternalLoginRepository _externalLoginRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IEmployeeRepository _employeeRepository;
        private ISalaryRepository _salaryRepository;
        private IPayslipRepository _payslipRepository;
        private IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members
        public IExternalLoginRepository ExternalLoginRepository
        {
            get { return _externalLoginRepository ?? (_externalLoginRepository = new ExternalLoginRepository(_context)); }
        }

        public IRoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository ?? (_employeeRepository = new EmployeeRepository(_context)); 
                
            }
        }
        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return _departmentRepository ?? (_departmentRepository = new DepartmentRepository(_context));

            }
        }

        public ISalaryRepository SalaryRepository
        {
            get
            {
                return _salaryRepository ?? (_salaryRepository = new SalaryRepository(_context));

            }
        }


        public IPayslipRepository PayslipRepository
        {
            get
            {
                return _payslipRepository ?? (_payslipRepository = new PayslipRepository(_context));

            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _externalLoginRepository = null;
            _roleRepository = null;
            _userRepository = null;
            _context.Dispose();
        }

      

        #endregion
    }
}

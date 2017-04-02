using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.Domain;
using Payroll.Domain.Entities;

namespace Payroll.Web.Services
{
    public class EmployeeService 
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public System.Threading.Tasks.Task CreateAsync(Employee employee)
        {
            if (employee == null)
                    throw new ArgumentNullException("employee");
            
            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChangesAsync();
        }

   
    }
}
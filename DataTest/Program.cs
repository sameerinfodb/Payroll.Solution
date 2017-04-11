using Payroll.Data.EntityFramework;
using Payroll.Domain;
using System;
using System.Collections.Generic;
using Payroll.Domain.Entities;
using Payroll.Web.Identity;

namespace DataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork UOW = new UnitOfWork("PayrollDbConnection");

            try
            {
                decimal basicSalary = 120000M;
                decimal CTC = 1000000M;
                decimal gratuity = 10000M;
                decimal HRA = 240000M;
                decimal medicalAllowance = 15000M;
                decimal specialAllowance = 603000M;
                decimal travelAllowance = 12000M;
                int departmentCount = 10;
                int employeeCount=0;
                int payslipCount=0;
                string EmployeeCode= string.Empty;
                string EmployeeName = string.Empty;
           
                string DepartmentCode = string.Empty;
                string DeparmentName = string.Empty;

                while (departmentCount != 0) //seeding department data
                {
                    DepartmentCode = string.Format("D{0}", departmentCount.ToString("D5"));
                    DeparmentName = string.Format("Department{0}", departmentCount);
                    Department department1 = CreateDepartment(DepartmentCode, DeparmentName);
                    UOW.DepartmentRepository.Add(department1);
                    employeeCount = 10;
                    while (employeeCount != 0)//seeding employee data
                    {
                        EmployeeCode = string.Format("M10{0}{1}", departmentCount, employeeCount.ToString("D5"));
                        EmployeeName = string.Format("Employee{0}{1}", departmentCount, employeeCount);
                        Employee employee = CreateEmployee(EmployeeName, EmployeeName, EmployeeCode, DateTime.Now.AddYears(-30), department1);
                        Salary salary = CreateSalary(basicSalary, CTC, employee, gratuity, HRA, medicalAllowance, specialAllowance, travelAllowance);

                        UOW.EmployeeRepository.Add(employee);
                        UOW.SalaryRepository.Add(salary);

                        payslipCount = 12;
                        while (payslipCount != 0)
                        {
                            Payslip payslip = CreatePayslip(basicSalary, string.Format("PAY{0}{1}{2}", departmentCount, employeeCount, payslipCount.ToString("D5")), new DateTime(2017, payslipCount, 1), HRA,
                                specialAllowance, CTC, 31, 0, employee);
                            UOW.PayslipRepository.Add(payslip);
                            payslipCount--;
                        }

                        employeeCount--;
                    }
                    UOW.SaveChanges();
                    departmentCount--;
                }
                //  Department department1 = CreateDepartment("D001", "Human Resource");
                //  Employee employee = CreateEmployee("Employee1", "Employee2", "M10124000", DateTime.Now.AddYears(-30), department1);
                //Salary salary = CreateSalary(basicSalary, CTC, employee, gratuity, HRA, medicalAllowance, specialAllowance);
                //Payslip payslip = CreatePayslip(basicSalary, "PAY001", DateTime.Now, HRA,
                //  specialAllowance, CTC, 31, 0, employee);


                //UOW.DepartmentRepository.Add(department1);
                //UOW.EmployeeRepository.Add(employee);
                //UOW.SalaryRepository.Add(salary);
                //UOW.PayslipRepository.Add(payslip);
                //UOW.SaveChanges();
                User admin = CreateUser("AdminUser");
                UOW.UserRepository.Add(admin);
                UOW.SaveChanges();

                Role role= CreateRole("Admin",new List<User>(){admin}) ;
                UOW.RoleRepository.Add(role);
                UOW.SaveChanges();

                admin.Roles.Add(role);
                UOW.UserRepository.Update(admin);
                UOW.SaveChanges();

                Console.WriteLine("Sample data has been loaded.Please ENTER to exit");
                
                Console.ReadKey();
            }
            catch (Exception exception)
            {

                Console.Write("Error Occurred :{0}", exception.Message);
                Console.ReadKey();
            }
        }

      
        private static Payslip CreatePayslip(decimal basicSalary,string payslipCode,DateTime payslipDate,
                                            decimal hra,decimal specialAllowance,decimal ctc,int totalWorkingDays,int totalLOP,Employee employee)
        {
            Payslip payslip = new Payslip()
            {
                Id = Guid.NewGuid(),
                Employee = employee,
                PayslipCode = payslipCode,
                PayslipDate = payslipDate,
                BasicAmount = basicSalary / 12,
                HRAAmount = hra / 12,
                SpecialAllowanceAmount = specialAllowance / 12,
                EmployeeProvidentFund = (Decimal.Multiply((basicSalary / 12), 0.12M)),
                OtherDeduction = Decimal.Multiply((ctc / 12), 0.30M),
                TotalWorkingDays = 31,
                TotalLossOfPay = 0,
                GrossSalary = (hra / 12) + (basicSalary / 12) + (specialAllowance / 12),
                NetSalary = ((hra / 12) + (basicSalary / 12) + (specialAllowance / 12)) - ((Decimal.Multiply((basicSalary / 12), 0.12M)) + Decimal.Multiply((ctc / 12), 0.30M))
                
            };
            return payslip;
            
        }

        private static Salary CreateSalary(decimal basicSalary, decimal ctc, Employee employee, decimal gratuity, decimal hra, decimal medicalAllowance, decimal specialAllowance,decimal travelAllowance)
        {
            Salary salary = new Salary()
            {
                BasicSalary = basicSalary,
                CostToCompany = ctc,
                Employee = employee,
                EmployeeCode = employee.EmployeeCode,
                Gratuity = gratuity,
                TravelAllowance = travelAllowance,
                HRA = hra,
                ID = Guid.NewGuid(),
                MedicalInsurance = medicalAllowance,
                SpecialAllowance = specialAllowance,
                Status = 1

            };
            return salary;
        }

        private static Department CreateDepartment(string departmentCode, string deparmentName)
        {
            return new Department()
            {
                Id = Guid.NewGuid(),
                DepartmentCode = departmentCode,
                DepartmentName = deparmentName,
                Status = 1
            };
        }

        private static User CreateUser(string userName)
        {

            return new User()
            {
                UserId = Guid.NewGuid(),
                UserName = userName,
                PasswordHash = "AN69+uF7x0FlEKdDsbCBRGwDOHYfLzk6bf9DZWJJQimDMFCCwVLVvYBpjUY9k9wmqg==",
                SecurityStamp = "5aa57060-7d87-4d9e-a360-112787f7d357",
                EmployeeCode= "M101000001"
            };
        }

        private static Role CreateRole(string roleName,List<User> users)
        {
            return new Role()
            {
                RoleId = Guid.NewGuid(),
                Name = roleName,
                Users = users
            };
        }

        private static Employee CreateEmployee(string firstName, string lastName, string employeeCode, DateTime dob, Department department)
        {
            Employee employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                EmployeeCode = employeeCode,
                DateOfBirth = dob,
                Status = 1,
                Id = Guid.NewGuid()
            };
            return employee;
        }
    }
}

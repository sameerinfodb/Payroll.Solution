namespace Payroll.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DepartmentCode = c.String(nullable: false, maxLength: 50),
                        DepartmentName = c.String(nullable: false, maxLength: 100),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentCode);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EmployeeCode = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DOB = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DepartmentAssignedData = c.DateTime(precision: 7, storeType: "datetime2"),
                        DepartmentCode = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeCode)
                .ForeignKey("dbo.Department", t => t.DepartmentCode)
                .Index(t => t.DepartmentCode);
            
            CreateTable(
                "dbo.Payslips",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PayslipCode = c.String(nullable: false, maxLength: 50),
                        EmployeeCode = c.String(nullable: false, maxLength: 50),
                        PayslipDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        BasicAmount = c.Decimal(nullable: false, precision: 10, scale: 2),
                        HRAAmount = c.Decimal(nullable: false, precision: 10, scale: 2),
                        SpecialAllowanceAmount = c.Decimal(nullable: false, precision: 10, scale: 2),
                        EmployeeProvidentFund = c.Decimal(nullable: false, precision: 10, scale: 2),
                        OtherDeduction = c.Decimal(nullable: false, precision: 10, scale: 2),
                        TotalWorkingDays = c.Int(nullable: false),
                        TotalLossOfPay = c.Int(nullable: false),
                        GrossSalary = c.Decimal(nullable: false, precision: 10, scale: 2),
                        NetSalary = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.PayslipCode)
                .ForeignKey("dbo.Employee", t => t.EmployeeCode, cascadeDelete: true)
                .Index(t => t.EmployeeCode);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EmployeeCode = c.String(nullable: false, maxLength: 50),
                        BasicPay = c.Decimal(nullable: false, precision: 10, scale: 2),
                        SpecialAllowance = c.Decimal(nullable: false, precision: 10, scale: 2),
                        HRA = c.Decimal(nullable: false, precision: 10, scale: 2),
                        TravelAllowance = c.Decimal(nullable: false, precision: 10, scale: 2),
                        MedicalInsurance = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Gratuity = c.Decimal(nullable: false, precision: 10, scale: 2),
                        CTC = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeCode)
                .ForeignKey("dbo.Employee", t => t.EmployeeCode)
                .Index(t => t.EmployeeCode);
            
            CreateTable(
                "dbo.ExternalLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        PasswordHash = c.String(maxLength: 4000),
                        SecurityStamp = c.String(maxLength: 4000),
                        EmployeeCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Employee", t => t.EmployeeCode)
                .Index(t => t.EmployeeCode);
            
            CreateTable(
                "dbo.Claim",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(maxLength: 4000),
                        ClaimValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.TestEntityTable",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExternalLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.User", "EmployeeCode", "dbo.Employee");
            DropForeignKey("dbo.Claim", "UserId", "dbo.User");
            DropForeignKey("dbo.Salaries", "EmployeeCode", "dbo.Employee");
            DropForeignKey("dbo.Payslips", "EmployeeCode", "dbo.Employee");
            DropForeignKey("dbo.Employee", "DepartmentCode", "dbo.Department");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.Claim", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "EmployeeCode" });
            DropIndex("dbo.ExternalLogin", new[] { "UserId" });
            DropIndex("dbo.Salaries", new[] { "EmployeeCode" });
            DropIndex("dbo.Payslips", new[] { "EmployeeCode" });
            DropIndex("dbo.Employee", new[] { "DepartmentCode" });
            DropTable("dbo.UserRole");
            DropTable("dbo.TestEntityTable");
            DropTable("dbo.Role");
            DropTable("dbo.Claim");
            DropTable("dbo.User");
            DropTable("dbo.ExternalLogin");
            DropTable("dbo.Salaries");
            DropTable("dbo.Payslips");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
        }
    }
}

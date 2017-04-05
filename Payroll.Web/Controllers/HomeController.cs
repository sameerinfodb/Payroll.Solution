using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("DepartmentDetails")]
        public ActionResult DisplayDepartmentDetails()
        {

            return View("DepartmentDetails");
        }
        [ActionName("PayslipList")]
        public ActionResult DisplayPayslipList()
        {
          
            return View("PayslipListing");
        }
    }
}
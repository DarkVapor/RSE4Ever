
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSE4Ever.QueryBuilder.Lib;
using System.Text;
using RSE4Ever.DataTablesMapping.Lib.Models;
using RSE4Ever.DataTablesMapping.Lib;
using RSE4Ever.DataTablesMapping.Lib.Mapping;
using RSE4Ever.Repositories;
using BForms.Models;
using RSE4Ever.Examples;
using BForms.Grid;

namespace RSE4Ever.Controllers
{
    public class HomeController : Controller
    {
       

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            GridEmployeeRepository _gridRepository = new GridEmployeeRepository();
            var gridModel = _gridRepository.ToBsGridViewModel();

            var model = new EmployeeViewModel
            {
                Grid = gridModel,
            };
            

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
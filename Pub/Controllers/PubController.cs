using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Collections;
using Pub.Models;


namespace Pub.Controllers
{
    public class PubController : Controller
    {
        // GET: Pub
        public ActionResult Index()
        {
            PubModel pubModel = new PubModel();
            List<EmployeeList> employeeList = pubModel.GetEmployeeList();
            return View(employeeList);
        }
    }
}
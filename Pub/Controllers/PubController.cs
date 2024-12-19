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
        public ActionResult Create()
        {
            PubModel pubModel = new PubModel();
            List<JobsList> jobsList = pubModel.GetJobsList();
            List<PublishersLis> publishersList = pubModel.GetPublishersList();

            var model = new EmployeeFormData
            {
                JobsList = jobsList,
                PublishersList = publishersList
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeFormData formData)
        {
            //if (ModelState.IsValid)
            //{
            //    var a = model.job_id;
            //    // 在這裡處理儲存員工資料的邏輯
            //    return RedirectToAction("Index");
            //}
            PubModel pubModel = new PubModel();
            pubModel.AddEmployee(formData);

            return RedirectToAction("Index");
        }
    }
}
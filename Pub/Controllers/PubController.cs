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
        public ActionResult Edit(string emp_id)
        {
            PubModel pubModel = new PubModel();
            QueryEmployee queryEmployee = pubModel.QueryEmployee(emp_id);
            List<JobsList> jobsList = pubModel.GetJobsList();
            List<PublishersLis> publishersList = pubModel.GetPublishersList();

            var model = new EmployeeEditFormData
            {
                QueryEmployee = queryEmployee,
                JobsList = jobsList,
                PublishersList = publishersList
            };
            return View(model);
        }
        public ActionResult Delete(string emp_id)
        {
            PubModel pubModel = new PubModel();
            QueryEmployee queryEmployee = pubModel.QueryEmployee(emp_id);
            List<JobsList> jobsList = pubModel.GetJobsList();
            List<PublishersLis> publishersList = pubModel.GetPublishersList();

            var model = new EmployeeEditFormData
            {
                QueryEmployee = queryEmployee,
                JobsList = jobsList,
                PublishersList = publishersList
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeFormData formData)
        {
            PubModel pubModel = new PubModel();
            pubModel.AddEmployee(formData);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeFormData formData)
        {
            PubModel pubModel = new PubModel();
            pubModel.UpdateEmployee(formData);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteEmployee(string emp_id)
        {
            PubModel pubModel = new PubModel();
            pubModel.DeleteEmployee(emp_id);

            return RedirectToAction("Index");
        }
    }
}
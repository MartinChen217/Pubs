using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Pub.Models
{
    public class PubModel
    {
        public List<EmployeeList> GetEmployeeList()
        {
            List<EmployeeList> rtnEmployeeList = new List<EmployeeList>();
            string sql = @"SELECT [emp_id]
                                  ,[fname]
                                  ,[lname]
                                  ,[hire_date]
	                              ,b.job_desc
	                              ,c.pub_name
	                              ,c.city
                              FROM [pubs].[dbo].[employee] a left join jobs b on a.job_id=b.job_id
                              left join publishers c on a.pub_id= c.pub_id";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-43GTN5A;Initial Catalog=pubs;Integrated Security=True"))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeList employeeList = new EmployeeList();
                            employeeList.emp_id = reader["emp_id"].ToString();
                            employeeList.name = reader["fname"].ToString() + " " + reader["lname"].ToString();
                            employeeList.hire_date = reader["hire_date"].ToString();
                            employeeList.job_desc = reader["job_desc"].ToString();
                            employeeList.pub_name = reader["pub_name"].ToString();
                            employeeList.city = reader["city"].ToString();
                            rtnEmployeeList.Add(employeeList);
                        }
                    }
                }
            }
            return rtnEmployeeList;
        }
    }
    public class EmployeeList
    {
        public string emp_id { get; set; }
        public string name { get; set; }

        public string hire_date { get; set; }
        public string job_desc { get; set; }
        public string pub_name { get; set; }
        public string city { get; set; }
    }
}
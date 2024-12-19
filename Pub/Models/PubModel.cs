using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Pub.Models
{
    public class PubModel
    {
        internal List<EmployeeList> GetEmployeeList()
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
                              left join publishers c on a.pub_id= c.pub_id   
                              order by hire_date desc";

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
        internal void AddEmployee(EmployeeFormData formData)
        {
            List<EmployeeList> rtnEmployeeList = new List<EmployeeList>();
            string sql = @"INSERT INTO employee ([emp_id]
                                                  ,[fname]
                                                  ,[minit]
                                                  ,[lname]
                                                  ,[job_id]
                                                  ,[pub_id]
                                                  ,[hire_date])
                                            VALUES (@emp_id
                                                    ,@fname
                                                    ,@minit
                                                    ,@lname
                                                    ,@job_id
                                                    ,@pub_id
                                                    ,@hire_date)";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-43GTN5A;Initial Catalog=pubs;Integrated Security=True"))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    string fullName = formData.name;
                    string[] nameSplit = fullName.Split(' ');
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@emp_id", formData.emp_id));
                    command.Parameters.Add(new SqlParameter("@fname", nameSplit[0] ?? ""));
                    command.Parameters.Add(new SqlParameter("@minit", ""));
                    command.Parameters.Add(new SqlParameter("@lname", nameSplit[1] ?? ""));
                    command.Parameters.Add(new SqlParameter("@job_id", formData.job_id));
                    command.Parameters.Add(new SqlParameter("@pub_id", formData.pub_id));
                    command.Parameters.Add(new SqlParameter("@hire_date", formData.hire_date));
                    command.ExecuteNonQuery();
                }
            }
        }
        internal List<JobsList> GetJobsList()
        {
            List<JobsList> rtnJobsList = new List<JobsList>();
            string sql = @"SELECT [job_id]
                                  ,[job_desc]
                              FROM [pubs].[dbo].[jobs]";

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
                            JobsList jobsList = new JobsList();
                            jobsList.job_id = reader["job_id"].ToString();
                            jobsList.job_desc = reader["job_desc"].ToString();
                            rtnJobsList.Add(jobsList);
                        }
                    }
                }
            }
            return rtnJobsList;
        }
        internal List<PublishersLis> GetPublishersList()
        {
            List<PublishersLis> rtnPublishersLis = new List<PublishersLis>();
            string sql = @"SELECT [pub_id]
                                  ,[pub_name]
                              FROM [pubs].[dbo].[publishers]";

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
                            PublishersLis publishersLis = new PublishersLis();
                            publishersLis.pub_id = reader["pub_id"].ToString();
                            publishersLis.pub_name = reader["pub_name"].ToString();
                            rtnPublishersLis.Add(publishersLis);
                        }
                    }
                }
            }
            return rtnPublishersLis;
        }
        internal QueryEmployee QueryEmployee(string emp_id)
        {
            QueryEmployee rtnQueryEmployee = new QueryEmployee();
            string sql = @"SELECT [emp_id]
                                  ,[fname]
                                  ,[lname]
                                  ,[hire_date]
	                              ,b.job_id
	                              ,c.pub_id
                                  ,b.job_desc
	                              ,c.pub_name
                              FROM [pubs].[dbo].[employee] a left join jobs b on a.job_id=b.job_id
                              left join publishers c on a.pub_id= c.pub_id   
                              where emp_id=@emp_id";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-43GTN5A;Initial Catalog=pubs;Integrated Security=True"))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@emp_id", emp_id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rtnQueryEmployee.emp_id = reader["emp_id"].ToString();
                            rtnQueryEmployee.name = reader["fname"].ToString() + " " + reader["lname"].ToString();
                            rtnQueryEmployee.job_id = reader["job_id"].ToString();
                            rtnQueryEmployee.pub_id = reader["pub_id"].ToString();
                            rtnQueryEmployee.job_desc = reader["job_desc"].ToString();
                            rtnQueryEmployee.pub_name = reader["pub_name"].ToString();
                        }
                    }
                }
            }
            return rtnQueryEmployee;
        }
        internal void UpdateEmployee(EmployeeFormData formData)
        {
            List<EmployeeList> rtnEmployeeList = new List<EmployeeList>();
            string sql = @"UPDATE [pubs].[dbo].[employee]
                                            SET fname=@fname
                                                ,lname=@lname
                                                ,job_id=@job_id
                                                ,pub_id=@pub_id
                                            WHERE emp_id=@emp_id";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-43GTN5A;Initial Catalog=pubs;Integrated Security=True"))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    string fullName = formData.name;
                    string[] nameSplit = fullName.Split(' ');
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@emp_id", formData.emp_id));
                    command.Parameters.Add(new SqlParameter("@fname", nameSplit[0] ?? ""));
                    command.Parameters.Add(new SqlParameter("@minit", ""));
                    command.Parameters.Add(new SqlParameter("@lname", nameSplit[1] ?? ""));
                    command.Parameters.Add(new SqlParameter("@job_id", formData.job_id));
                    command.Parameters.Add(new SqlParameter("@pub_id", formData.pub_id));
                    command.ExecuteNonQuery();
                }
            }
        }
        internal void DeleteEmployee(string emp_id)
        {
            List<EmployeeList> rtnEmployeeList = new List<EmployeeList>();
            string sql = @"DELETE FROM [pubs].[dbo].[employee]
                                        WHERE emp_id=@emp_id";

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-43GTN5A;Initial Catalog=pubs;Integrated Security=True"))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@emp_id", emp_id));
                    command.ExecuteNonQuery();
                }
            }
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
    public class JobsList
    {
        public string job_id { get; set; }
        public string job_desc { get; set; }
    }
    public class PublishersLis
    {
        public string pub_id { get; set; }
        public string pub_name { get; set; }
    }
    public class EmployeeFormData
    {
        public string emp_id { get; set; }
        public string name { get; set; }
        public string job_id { get; set; }
        public string pub_id { get; set; }
        public DateTime hire_date { get; set; }

        public List<JobsList> JobsList { get; set; }
        public List<PublishersLis> PublishersList { get; set; }
    }
    public class EmployeeEditFormData
    {
        public string emp_id { get; set; }
        public string name { get; set; }
        public string job_id { get; set; }
        public string pub_id { get; set; }

        public QueryEmployee QueryEmployee { get; set; }
        public List<JobsList> JobsList { get; set; }
        public List<PublishersLis> PublishersList { get; set; }
    }
    public class QueryEmployee
    {
        public string emp_id { get; set; }
        public string name { get; set; }
        public string job_id { get; set; }
        public string pub_id { get; set; }
        public string job_desc { get; set; }
        public string pub_name { get; set; }

    }
}
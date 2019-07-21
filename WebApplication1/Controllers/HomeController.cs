using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using ServiceStack;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Employee()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLEXPRESS";
            builder.InitialCatalog = "employee";
            builder.UserID = "sa";
            builder.Password = "123";

            SqlConnection conn = new SqlConnection(builder.ConnectionString);

            SqlDataAdapter adapter = new SqlDataAdapter("select * from employee", conn);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);

            if (dataset.Tables[0].Rows.Count == 0)
            {
                return View(new EmployeeModel() { employee = new List<Employee> { new Employee() { ID_Employee = -1, Surname = "null", Name = "null", Family = "null", Gender = "null", FamilyStatus = "null", Education = "null", citizenship = "null", Address = "null" } } });
            } else
            {
                List<Employee> list = new List<Employee>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    Employee emp = new Employee();
                    emp.ID_Employee = (int)row["ID_Employee"];
                    emp.Family = (string)row["Family"];
                    emp.Name = (string)row["Name"];
                    emp.Surname = (string)row["Surname"];
                    if (row["Gender"].GetType() == typeof(DBNull))
                    {
                        emp.Gender = "default";
                    }
                    else emp.Gender = (string)row["Gender"];

                    if (row["citizeship"].GetType() == typeof(DBNull))
                    {
                        emp.citizenship = "default";
                    }
                    else emp.citizenship = (string)row["citizeship"];


                    if (row["Address"].GetType() == typeof(DBNull))
                    {
                        emp.Address = "default";
                    }
                    else emp.Address = (string)row["Address"];

                    if (row["FamilyStatus"].GetType() == typeof(DBNull))
                    {
                        emp.FamilyStatus = "default";
                    }
                    else emp.FamilyStatus = (string)row["FamilyStatus"];

                    if (row["Educatoion"].GetType() == typeof(DBNull))
                    {
                        emp.Education = "default";
                    }
                    else emp.Education = (string)row["Educatoion"];
                    list.Add(emp);
                }
                return View(new EmployeeModel() { employee = list }); 
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetValue()
        {
            string family = Request.Form["InputFamily"];
            string name = Request.Form["InputName"];
            string surname = Request.Form["InputSurname"];
            string Address = Request.Form["InputAddress"];
            string Gender = Request.Form["Gender"];
            string citizenship = Request.Form["citizenship"];
            string Education = Request.Form["education"];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLEXPRESS";
            builder.InitialCatalog = "employee";
            builder.UserID = "sa";
            builder.Password = "123";

            SqlConnection conn = new SqlConnection(builder.ConnectionString);
            conn.Open();

            SqlCommand comand = new SqlCommand("CRUDEmployee", conn);
            comand.CommandType = CommandType.StoredProcedure;
            comand.Parameters.AddWithValue("id", 0);
            comand.Parameters.AddWithValue("name", name);
            comand.Parameters.AddWithValue("family", family);
            comand.Parameters.AddWithValue("surname", surname);
            comand.Parameters.AddWithValue("gender", Gender);
            comand.Parameters.AddWithValue("citizenship", "some");
            comand.Parameters.AddWithValue("addres", Address);
            comand.Parameters.AddWithValue("education", "some");
            comand.Parameters.AddWithValue("phone", 0);
            comand.Parameters.AddWithValue("FamilyStatus", "nothing");

            comand.ExecuteNonQuery();
            conn.Close();

            return View();
        }
    }
}

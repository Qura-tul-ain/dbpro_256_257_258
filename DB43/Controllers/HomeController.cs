using DB43.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB43.Controllers
{
	public class HomeController : Controller
	{
        public string connection = "Data Source=DESKTOP-G0K5DQK;Initial Catalog=DB433;Integrated Security=True";

        public static int CId;// course id
        public ActionResult Get_Id(int id)
        {

            CId = id;

            return RedirectToAction("Details");
        }
        public ActionResult Index()
		{
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;

            List<DepartmentViewModel> lists = new List<DepartmentViewModel>();

            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " SELECT * FROM Department ";

            SqlCommand = new SqlCommand(query, conn);
            SqlDataReader rdr = SqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                var dpt = new DepartmentViewModel();
                dpt.Id = Convert.ToInt32(rdr[0]);
                dpt.Name = rdr[1].ToString();
                dpt.Image = rdr[2].ToString();
                lists.Add(dpt);
            }

            return View(lists);
        }
        public ActionResult DIisplayCourses(int id)
        {


            SqlConnection conn = new SqlConnection(connection);
            //Open the connection to db
            conn.Open();
            string query;
            SqlCommand SqlCommand;

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();

            //Generating the query to fetch the contact details
            query = " SELECT* FROM Course where DepartmentId = ' " + id + "' ";

            SqlCommand = new SqlCommand(query, conn);
            SqlDataReader rdr = SqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                var dpt = new AddCourseViewModels();
                dpt.Id = Convert.ToInt32(rdr[0]);
                dpt.Title = rdr[1].ToString();
                dpt.Credits = rdr[2].ToString();
                dpt.Fee = Convert.ToInt32(rdr[3]);
                dpt.DepartmentId = Convert.ToInt32(rdr[4]);
                dpt.CourseImage = rdr[5].ToString();
                lists.Add(dpt);
            }

            return View(lists);
        }
        public ActionResult Details()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();
            string query = "SELECT * from Course where Id='" + CId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var course = new AddCourseViewModels();
                course.Id = Convert.ToInt32(rdr[0]);
                course.Title = rdr[1].ToString();
                course.Credits = rdr[2].ToString();
                course.Fee = Convert.ToInt32(rdr[3]);
                course.DepartmentId = Convert.ToInt32(rdr[4]);
                course.CourseImage = rdr[5].ToString();
                lists.Add(course);
            }
            rdr.Close();
            return View(lists);
        }


        public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
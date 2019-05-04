using System;
using System.Collections.Generic;
using System.Linq;
using DB43;
using System.Web;
using System.Web.Mvc;
using DB43.Models;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace DB43.Controllers
{
    public class CourseController : Controller
    {
		DB43Entities1 db = new DB43Entities1();
        public string connection = "Data Source=DESKTOP-QH0J28G;Initial Catalog=DB43;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        public static int Cid;
        public ActionResult Get_Id(int id)
        {

            Cid = id;// global variable

            return RedirectToAction("Details",new { CId=id});
        }

        public ActionResult Get_CID(int id)
        {

            Cid = id;// global variable

            return RedirectToAction("DetailsForStudents", new { CId = id });
        }

        //      public ActionResult CourseRegister(int id)
        //{
        //	Course c = new Course();
        //	c = db.Courses.Find(id);

        //	StudentRegisterCourse g = new StudentRegisterCourse();
        //	g.CourseId = c.Id;
        //	g.DepartmentId = c.DepartmentId;

        //	//g.DepartmentId = 
        //	//var user = db.People.
        //	var Get_Student = db.People.Single(u => u.Email == "farah@gmail.com");
        //	g.StudentId = Get_Student.Id;
        //	db.StudentRegisterCourses.Add(g);
        //	db.SaveChanges();
        //	return View("CourseRegister");
        //}
        // GET: Course
        public ActionResult Index(int id)
		{
			var Get_Student = db.People.Single(u => u.Email == "farah@gmail.com");

			List<Department> List3 = db.Departments.ToList();
			List<Course> List1 = db.Courses.ToList();
			List<CourseViewModels> viewList = new List<CourseViewModels>();
			List<Course> List2 = db.Courses.ToList();
			List<StudentRegisterCourse> viewList2 = db.StudentRegisterCourses.ToList();
			List<Department> d = db.Departments.ToList();
			foreach (Course c in List1)
			{
				bool f = false;
				CourseViewModels obj = new CourseViewModels();
				foreach (StudentRegisterCourse v in viewList2)
				{
					if (c.Id == v.CourseId && v.StudentId == Get_Student.Id)
					{
						f = true;
					}
				}

				if (f == false)
				{
					obj.Id = c.Id;
					obj.Title = c.Title;
					obj.Credits = c.Credits;
					obj.Fee = c.Fee;
					foreach (Department t in d)
					{
						if (c.DepartmentId == id)
						{
							obj.DepartmentName = t.Name;
						}
					}
					viewList.Add(obj);
				}


			}
			
			return View(viewList);
			//return View(viewList);
		}

        //Report
        public ActionResult ExportCourses()
        {
            List<Course> allCourses = new List<Course>();
            allCourses = db.Courses.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CourseReport.rpt"));
            rd.SetDataSource(allCourses);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Courses.pdf");
            }
            catch { throw; }
        }

        // GET: Course/Details/5
        public ActionResult Details(int CId)
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

        public ActionResult DetailsForStudents(int CID)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();
            string query = "SELECT * from Course where Id='" + CID + "'";
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

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //Report

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
         
            string query = "SELECT * from Course where Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            var course = new AddCourseViewModels();
            while (rdr.Read())
            {
               
                course.Id = Convert.ToInt32(rdr[0]);
                course.Title = rdr[1].ToString();
                course.Credits = rdr[2].ToString();
                course.Fee = Convert.ToInt32(rdr[3]);
                course.DepartmentId = Convert.ToInt32(rdr[4]);
                course.CourseImage = rdr[5].ToString();
             
            }
            rdr.Close();
            return View(course);
        
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,AddCourseViewModels course)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                // TODO: Add update logic here
                string query = "UPDATE Course SET Title='"+course.Title+ "', Credits='" + course.Credits + "',Fee='" + course.Fee + "'  where Id='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                return RedirectToAction("Details",new { CId=id});
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

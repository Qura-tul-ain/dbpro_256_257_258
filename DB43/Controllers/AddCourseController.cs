using DB43.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using DB43;
using System.Web.Mvc;

namespace DB43.Controllers
{
    public class AddCourseController : Controller
    {
        public string connection = " Data Source = DESKTOP-G0K5DQK; Initial Catalog = DB433; Integrated Security = True";
        public static int  DeptId;
        public static int instructorId;// store id of instructor
        public static int CId;// course id
        public ActionResult Get_Id(int id)
        {

            DeptId = id;

            return RedirectToAction("Create");
        }


        // GET: AddCourse
        public ActionResult Index(int id)
        {
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " SELECT * FROM Course where DepartmentId= ' " + id+ "' ";

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
        public ActionResult Get_CId(int id)
        {

            CId = id;

            return RedirectToAction("AssignInstructor");
        }

        public ActionResult AssignInstructor()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string query = "INSERT INTO AssignInstructor(InstructorId,CourseId)VALUES ('" + instructorId + "','" + CId + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            return RedirectToAction("AllCourses",new { id=instructorId});
          //  return RedirectToAction("InstructorWithCourses");
        }

        public ActionResult InstructorWithCourses()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            List<AssignmentViewModels> lists = new List<AssignmentViewModels>();
            string query = "select FirstName,LastName,Name,Title from Person inner join Instructor on Person.Id=Instructor.InstructorId inner join AssignInstructor on Instructor.InstructorId=AssignInstructor.InstructorId inner join Course on AssignInstructor.CourseId=Course.Id inner join Department on Course.DepartmentId = Department.Id ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var intsWithCourse = new AssignmentViewModels();

                intsWithCourse.FirstName = rdr[0].ToString();
                intsWithCourse.LastName = rdr[1].ToString();
                intsWithCourse.Name = rdr[2].ToString();
                intsWithCourse.Title = rdr[3].ToString();
               
                lists.Add(intsWithCourse);
            }
            rdr.Close();
            return View(lists);
     
        }

        public ActionResult AllCourses(int id)
        {
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;
            instructorId = id;
            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();

            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " SELECT * FROM Course ";

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

        public ActionResult AllInstructors()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            List<RegisterViewModel> lists = new List<RegisterViewModel>();
            string query = "SELECT Id,FirstName,LastName,Contact,Email,Gender,Qualification from person inner join Instructor on Person.Id=Instructor.InstructorId and Person.Discriminator='" + "Teacher" + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var std = new RegisterViewModel();
                std.Id=Convert.ToInt32(rdr[0]);
                std.FirstName = rdr[1].ToString();
                std.LastName = rdr[2].ToString();
                std.Contact = rdr[3].ToString();
                std.Email = rdr[4].ToString();
                std.Gender = rdr[5].ToString();
                std.RegistrationNumber = rdr[6].ToString();
                lists.Add(std);
            }
            rdr.Close();
            return View(lists);
        }


        // GET: AddCourse/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AddCourse/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: AddCourse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AddCourseViewModels course)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string filename = Path.GetFileNameWithoutExtension(course.ImageFile.FileName);
                string extension = Path.GetExtension(course.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                course.CourseImage = "~/images/" + filename;
                filename = Path.Combine(Server.MapPath("~/images/"), filename);
                course.ImageFile.SaveAs(filename);


                string query = "INSERT INTO Course(Title,Credits,Fee,DepartmentId,CourseImage)VALUES ('" + course.Title + "','" + course.Credits + "','" + course.Fee + "','" + DeptId + "','" + course.CourseImage + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                // ViewBag.Message("Successfull");
                return RedirectToAction("Index",new { id=DeptId});



            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

       
      

        // GET: AddCourse/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddCourse/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddCourse/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddCourse/Delete/5
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

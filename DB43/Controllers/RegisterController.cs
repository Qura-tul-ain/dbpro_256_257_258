﻿using CrystalDecisions.CrystalReports.Engine;
using DB43.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB43.Controllers
{
    public class RegisterController : Controller
    {

        DB43Entities1 db = new DB43Entities1();
        //public string connection = "Data Source=DESKTOP-GP94IEM\\SQLEXPRESS;Initial Catalog=DB433;Integrated Security=True";
        public string connection = "Data Source=DESKTOP-QH0J28G;Initial Catalog=DB43;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        public static int id;
        public static int loginId=3;
        public static int DeptId;
        public static int no;
        public ActionResult Get_Id(int id)
        {

            DeptId = id;

            if(no ==1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("DIisplayCoursesToTeacher");
            }
     
        }

  
        // for students 
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connection);
            //Open the connection to db
            conn.Open();
            string query;
            SqlCommand SqlCommand;

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();

            //Generating the query to fetch the contact details
       
            query = "SELECT * FROM Course WHERE NOT EXISTS (SELECT * FROM StudentRegisterCourse where StudentRegisterCourse.CourseId = Course.Id  and StudentRegisterCourse.StudentId = '" + loginId + "') and Course.DepartmentId='"+DeptId+"' ";
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

        // for teachers 
        //teacher report
        public ActionResult ExportTeacher()
        {
            List<Person> allTeacher = new List<Person>();
            allTeacher = db.People.ToList();


            ReportDocument rd = new ReportDocument();
            //  rd.Load(Path.Combine(Server.MapPath("Student.rpt")));
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "TeacherList.rpt"));
            rd.SetDataSource(allTeacher);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Teachers.pdf");
            }
            catch { throw; }
        }

        public ActionResult ExportStudent()
        {
            List<Person> allTeacher = new List<Person>();
            allTeacher = db.People.ToList();


            ReportDocument rd = new ReportDocument();
            //  rd.Load(Path.Combine(Server.MapPath("Student.rpt")));
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Student.rpt"));
            rd.SetDataSource(allTeacher);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Student.pdf");
            }
            catch { throw; }
        }


        public ActionResult DIisplayCoursesToTeacher()
        {


            SqlConnection conn = new SqlConnection(connection);
            //Open the connection to db
            conn.Open();
            string query;
            SqlCommand SqlCommand;

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();

            //Generating the query to fetch the contact details
            query = " SELECT* FROM Course where DepartmentId = ' " + DeptId + "' ";

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

        // display all departments
        public ActionResult IndexStudent()
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

        public ActionResult PersonalData()
        {
            if (no == 1)
            {

                return RedirectToAction("StudentData");
            }

            else
            {
               
                return RedirectToAction("TeacherData");
            }



        }

        public ActionResult StudentData(List<RegisterViewModel> list)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            List<RegisterViewModel> lists = new List<RegisterViewModel>();
            string query = "SELECT FirstName,LastName,Contact,Email,Gender,RegistrationNumber from person inner join student on Person.Id=Student.PersonId and Person.Id='" + loginId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var std = new RegisterViewModel();
                std.FirstName = rdr[0].ToString();
                std.LastName = rdr[1].ToString();
                std.Contact = rdr[2].ToString();
                std.Email = rdr[3].ToString();
                std.Gender = rdr[4].ToString();
                std.RegistrationNumber = rdr[5].ToString();
                lists.Add(std);
            }
            rdr.Close();
            return View(lists);

        }


        public ActionResult TeacherData(List<RegisterViewModel> list)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            List<RegisterViewModel> lists = new List<RegisterViewModel>();
            string query = "SELECT Person.Id,FirstName,LastName,Contact,Email,Gender,Qualification from Person inner join Instructor on Person.Id=Instructor.InstructorId and Person.Id='" + loginId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var std = new RegisterViewModel();
                std.Id = Convert.ToInt32(rdr[0]);
                std.FirstName = rdr[1].ToString();
                std.LastName = rdr[2].ToString();
                std.Contact = rdr[3].ToString();
                std.Email = rdr[4].ToString();
                std.Gender = rdr[5].ToString();
                std.Qualification = rdr[6].ToString();
                lists.Add(std);
            }
            rdr.Close();
            return View(lists);
        
    }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel obj)
        {

            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "INSERT INTO Person(LastName,FirstName,Contact,Email,Gender,Password,Discriminator)VALUES ('" + obj.LastName + "','" + obj.FirstName + "','" + obj.Contact + "','" + obj.Email + "','" + obj.Gender + "','" + obj.Password + "','" + "Student" + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                string q = "SELECT * from Person where Email='" + obj.Email + "'  ";
                SqlCommand cmdForId = new SqlCommand(q, con);
                SqlDataReader rdr = cmdForId.ExecuteReader();

                while (rdr.Read())
                {
                    id = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
                con.Close();
                string RegNo = "IMS_" + id;
                string st = "InActive";
                // put reg no check here
                DateTime date = DateTime.Now;
                con.Open();
                string studentquery = "INSERT INTO Student(RegistrationNumber,EnrollmentDate,Status,PersonId)VALUES ('" + RegNo + "','" + date + "','" + st + "','" + id + "')";
                SqlCommand cmd2 = new SqlCommand(studentquery, con);
                cmd2.ExecuteNonQuery();


                ModelState.Clear();
               // ViewBag.Message = obj.FirstName + " " + obj.LastName + "Successfully Registered";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisterViewModel obj)
        {

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string q = "SELECT * from Person where Email='" + obj.Email + "' and Password='" + obj.Password + "' ";
            SqlCommand cmdForId = new SqlCommand(q, con);
            SqlDataReader rdr = cmdForId.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[7].ToString() == "Student")
                {
                    loginId = Convert.ToInt32(rdr[0]);
                    no = 1;
                    return RedirectToAction("RegisteredCourses");
                }
                else if (rdr[7].ToString() == "Teacher")
                {
                    loginId = Convert.ToInt32(rdr[0]);
                    no = 0;
                    return RedirectToAction("Index2", "Departments");
                }
                else if (rdr[7].ToString() == "Admin")
                {
                    no = 2;
                    loginId = Convert.ToInt32(rdr[0]);
                    return RedirectToAction("Index", "Departments");
                }

            }
            rdr.Close();
            ViewBag.Message="Incorrect password or Email ,please try again";
            return View(obj);


        }


        public ActionResult LoggedOut()
        {
            loginId = 3;
          return  RedirectToAction("Index", "Home");      
        }





        public ActionResult RegisterTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterTeacher(RegisterViewModel obj)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "INSERT INTO Person(LastName,FirstName,Contact,Email,Gender,Password,Discriminator)VALUES ('" + obj.LastName + "','" + obj.FirstName + "','" + obj.Contact + "','" + obj.Email + "','" + obj.Gender + "','" + obj.Password + "','" + "Teacher" + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                string q = "SELECT Id from Person where Email='" + obj.Email + "'  ";
                SqlCommand cmdForId = new SqlCommand(q, con);
                SqlDataReader rdr = cmdForId.ExecuteReader();

                while (rdr.Read())
                {
                    id = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
                con.Close();

                // put reg no check here
                DateTime date = DateTime.Now;
                con.Open();
                string studentquery = "INSERT INTO Instructor(InstructorId,Qualification,HireDate)VALUES ('" + id + "','" + obj.Qualification + "','" + date + "')";
                SqlCommand cmd2 = new SqlCommand(studentquery, con);
                cmd2.ExecuteNonQuery();


                ModelState.Clear();
                ViewBag.Message = obj.FirstName + " " + obj.LastName + "Successfully Registered";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View(ex);

            }



        }
        // register in a course
        public ActionResult CourseRegister(int id)
        {
            Course c = new Course();
            c = db.Courses.Find(id);

            StudentRegisterCourse g = new StudentRegisterCourse();
            g.CourseId = c.Id;
            g.DepartmentId = c.DepartmentId;

            //g.DepartmentId = 
            //var user = db.People.
            var Get_Student = db.People.Single(u => u.Id == loginId);
            g.StudentId = Get_Student.Id;
            db.StudentRegisterCourses.Add(g);
            db.SaveChanges();
            return View("RegisteredCourses");
        }
        // courses teached by a teacher
        public ActionResult TeacherWithCourses()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();

            List<InstructorWithCourses> lists = new List<InstructorWithCourses>();
            string query = "select Course.Id ,Department.Name,Course.Title,Person.FirstName + ' ' + Person.LastName as fullname from Course inner join Department on Course.DepartmentId=Department.Id inner join AssignInstructor on Course.Id=AssignInstructor.CourseId inner join Person  on Person.Id=AssignInstructor.InstructorId where Person.Id='" + loginId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var std = new InstructorWithCourses();
                std.Id = Convert.ToInt32(rdr[0]);
                std.deptName = rdr[1].ToString();
                std.CourseTitle= rdr[2].ToString();
                std.InstName = rdr[3].ToString();
             
                lists.Add(std);
            }
            rdr.Close();
            return View(lists);


        }

        public ActionResult Announcements()
        {
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;

            List<AnnouncementViewModel> lists = new List<AnnouncementViewModel>();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " select Announcement.Text,Course.Title from Person inner join Student on Person.Id=Student.PersonId inner join StudentRegisterCourse on Student.PersonId=StudentRegisterCourse.StudentId inner join Announcement on Announcement.CourseId=StudentRegisterCourse.CourseId inner join Course on Course.Id=Announcement.CourseId and Student.PersonId= '"+loginId+"'";

            SqlCommand = new SqlCommand(query, conn);
            SqlDataReader rdr = SqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                var announce = new AnnouncementViewModel();

                announce.text = rdr[0].ToString();
                announce.CourseName = rdr[1].ToString();

                lists.Add(announce);
            }

            return View(lists);

        }

        public ActionResult RegisteredCourses()
        {
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;

            List<AddCourseViewModels> lists = new List<AddCourseViewModels>();
            //Open the connection to db
            conn.Open();
            //Generating the query to fetch the contact details
            query = " select Course.Id,StudentRegisterCourse.DepartmentId,Course.Title,Course.Fee,Course.Credits,Course.CourseImage from Course inner join StudentRegisterCourse on Course.Id=StudentRegisterCourse.CourseId and StudentRegisterCourse.StudentId='"+loginId+"'";

            SqlCommand = new SqlCommand(query, conn);
            SqlDataReader rdr = SqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                var course =new  AddCourseViewModels();

               course.Id = Convert.ToInt32(rdr[0]);
                course.DepartmentId = Convert.ToInt32(rdr[1]);
                course.Title = rdr[2].ToString();
                course.Fee= Convert.ToInt32(rdr[3]);
                course.Credits = rdr[4].ToString();
               course.CourseImage= rdr[5].ToString();

                lists.Add(course);
            }

            return View(lists);


        }


        public ActionResult RegisteredStudents()
        {
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;

            List<StudentsRegisteredInCourse> lists = new List<StudentsRegisteredInCourse>();
            //Open the connection to db
            conn.Open();
            //Generating the query to fetch the contact details
            query = "select Person.FirstName + ' '+ Person.LastName as Name,Student.RegistrationNumber,Course.Title from AssignInstructor inner join Course on AssignInstructor.CourseId=Course.Id inner join StudentRegisterCourse on StudentRegisterCourse.CourseId=Course.Id inner join Student on Student.PersonId=StudentRegisterCourse.StudentId inner join Person on Person.Id =Student.PersonId where  AssignInstructor.InstructorId='"+loginId+"'";

            SqlCommand = new SqlCommand(query, conn);
            SqlDataReader rdr = SqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                var studnt = new StudentsRegisteredInCourse();

              
                studnt.Name = rdr[0].ToString();
                studnt.regNo = rdr[1].ToString();
                studnt.Course_title = rdr[2].ToString();
            

                lists.Add(studnt);
            }

            return View(lists);


        }

    }
    }
























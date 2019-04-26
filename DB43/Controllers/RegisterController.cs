using DB43.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB43.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            using (DB43Entities db = new DB43Entities())
            {
                return View(db.People.ToList());
            }

        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel obj)
        {

            DB43Entities db2 = new DB43Entities();
            if (ModelState.IsValid)
            {

                var person = new Person();
                person.FirstName = obj.FirstName;
                person.LastName = obj.LastName;
                person.Contact = obj.Contact;
                person.Email = obj.Email;
                person.Gender = obj.Gender;
				// person.Discriminator = obj.Discriminator;
				person.Discriminator = "Student";
                person.Password = obj.Password;

				using (DB43Entities db = new DB43Entities())
				{
					db.People.Add(person);
					db.SaveChanges();
				}
                
                ModelState.Clear();
                ViewBag.Message =person.FirstName + " " + person.LastName +  "Successfully Registered";
            }
            var Get_Student = db2.People.Single(u => u.Email == obj.Email);

            int id = Get_Student.Id ;
            var student = new Student();
            student.RegistrationNumber = "IMS_" + id;
            student.PersonId = id;
            student.EnrollmentDate = DateTime.Now;
            student.Status = "InActive";
            using (DB43Entities db = new DB43Entities())
            {

                try
                {
                    db.Students.Add(student);
                    db.SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                    //throw the error messages.
                }
            }

            return View();
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisterViewModel obj)
        {
            using (DB43Entities db = new DB43Entities())
            {
                var user = db.People.Single(u => u.Email == obj.Email && u.Password == obj.Password);
              if(user !=null )
                {
                    Session["Id"] = user.Id.ToString();
                    Session["Email"] = user.Email.ToString();
                    return RedirectToAction("LoggedIn");

                }
                else
                {
                    ModelState.AddModelError("", "Email or password is wrong");
                }
             
            }
            return View();
        }


        public ActionResult LoggedIn()
        {
            if (Session["Id"] !=null )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }





        public ActionResult RegisterTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterTeacher(RegisterViewModel obj)
        {
            DB43Entities db2 = new DB43Entities();

            if (ModelState.IsValid)
            {

                var person = new Person();
                person.FirstName = obj.FirstName;
                person.LastName = obj.LastName;
                person.Contact = obj.Contact;
                person.Email = obj.Email;
                person.Gender = obj.Gender;
                person.Discriminator = "Teacher";
                person.Password = obj.Password;
                person.Id = 10;

                //   person.Id = db2.People.Count() + 1;

                using (DB43Entities db = new DB43Entities())
                {
                    try
                    {
                        db.People.Add(person);
                        db.SaveChanges();
                        ModelState.Clear();
                        ViewBag.Message = person.FirstName + " " + person.LastName + "Successfully Registered";
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                        //throw the error messages.
                    }
                }
            }
            var use = db2.People.Single(u => u.Email == obj.Email);
            int Tid = use.Id;
            var instructor = new Instructor();
            instructor.InstructorId = Tid;
            instructor.Qualification = obj.Qualification;
            instructor.HireDate = DateTime.Now;
          
            using (DB43Entities db = new DB43Entities())
            {

                try
                {
                    db.Instructors.Add(instructor);
                    db.SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                    //throw the error messages.
                }
            }

            return View();
        }





    }
}
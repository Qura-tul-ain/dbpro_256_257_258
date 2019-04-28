using System;
using System.Collections.Generic;
using System.Linq;
using DB43;
using System.Web;
using System.Web.Mvc;
using DB43.Models;

namespace DB43.Controllers
{
    public class CourseController : Controller
    {
		DB43Entities db = new DB43Entities();
		
		public ActionResult CourseRegister(int id)
		{
			Course c = new Course();
			c = db.Courses.Find(id);

			StudentRegisterCourse g = new StudentRegisterCourse();
			g.CourseId = c.Id;
			g.DepartmentId = c.DepartmentId;

			//g.DepartmentId = 
			//var user = db.People.
			var Get_Student = db.People.Single(u => u.Email == "nimra@gmail.com");
			g.StudentId = Get_Student.Id;
			db.StudentRegisterCourses.Add(g);
			db.SaveChanges();
			return View("CourseRegister");
		}
		// GET: Course
		public ActionResult Index()
		{
			var Get_Student = db.People.Single(u => u.Email == "nimra@gmail.com");

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
						if (c.DepartmentId == t.Id)
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



		// GET: Course/Details/5
		public ActionResult Details(int id)
        {
			Course c = new Course();
			List<Course> list1 = db.Courses.ToList();
			//int b = list1.Count();
			Course user1 = db.Courses.Find(id);
			CourseViewModels g = new CourseViewModels();
			g.Id = user1.Id;
			g.Title = user1.Title;
			g.Credits = user1.Credits;
			g.Fee = user1.Fee;

            return View(g);
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

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Course/Edit/5
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

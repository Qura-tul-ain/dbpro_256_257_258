using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB43;
using System.Data.SqlClient;
using DB43.Models;
using System.IO;

using System.ComponentModel.DataAnnotations;

namespace DB43.Controllers
{
    public class DepartmentsController : Controller
    {
       // private DB43Entities db = new DB43Entities();
        public string connection = " Data Source = DESKTOP-G0K5DQK; Initial Catalog = DB433; Integrated Security = True";
       
        // GET: Departments
        // fro admin
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
            while(rdr.Read())
            {
                var dpt = new DepartmentViewModel();
                dpt.Id = Convert.ToInt32(rdr[0]);
                dpt.Name = rdr[1].ToString();
                dpt.Image = rdr[2].ToString();
                lists.Add(dpt);
            }
          
            return View(lists);
        }
        // for teachers

        public ActionResult Index2()
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

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Department department = db.Departments.Find(id);
            //if (department == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DepartmentViewModel department)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string filename = Path.GetFileNameWithoutExtension(department.ImageFile.FileName);
                string extension = Path.GetExtension(department.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                department.Image = "~/images/" + filename;
                filename = Path.Combine(Server.MapPath("~/images/"), filename);
                department.ImageFile.SaveAs(filename);

              
                string query = "INSERT INTO Department(Name,Image)VALUES ('" + department.Name + "','" + department.Image + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
               // ViewBag.Message("Successfull");
                return RedirectToAction("Index");

                

            }
            catch (Exception ex)
            {
                return View(ex);
            }
          
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Department department = db.Departments.Find(id);
            //if (department == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Department department)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(department).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Department department = db.Departments.Find(id);
            //if (department == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // Department department = db.Departments.Find(id);
           // db.Departments.Remove(department);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
          base.Dispose(disposing);
        }
    }
}

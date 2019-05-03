using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB43;
using DB43.Models;
using System.Data.SqlClient;

namespace DB43.Controllers
{
    public class AnnouncementController : Controller
    {
        private DB433Entities db = new DB433Entities();
        public string connection = " Data Source = DESKTOP-G0K5DQK; Initial Catalog = DB433; Integrated Security = True";
        public static int cid;

        public ActionResult Get_CId(int id)
        {
            cid = id;
            return RedirectToAction("Create");
        }

        // GET: Announcement
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connection);
            string query;
            SqlCommand SqlCommand;

            List<AnnouncementViewModel> lists = new List<AnnouncementViewModel> ();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " SELECT A.Text as Announcement,C.Title as Course FROM Announcement A,Course C where A.CourseId=C.Id ";

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

        // GET: Announcement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Announcement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AnnouncementViewModel announcement)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string query = "INSERT INTO Announcement(Text,PersonId,CourseId)VALUES ('" + announcement.text + "','" + 3 + "','" + cid + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            return View("Index");
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", announcement.CourseId);
            ViewBag.PersonId = new SelectList(db.People, "Id", "LastName", announcement.PersonId);
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,PersonId,CourseId")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", announcement.CourseId);
            ViewBag.PersonId = new SelectList(db.People, "Id", "LastName", announcement.PersonId);
            return View(announcement);
        }

        // GET: Announcement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

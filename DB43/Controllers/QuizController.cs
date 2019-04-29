using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB43.Models;

namespace DB43.Controllers
{
    public class QuizController : Controller
    {
		// GET: Quiz
		DB43Entities db = new DB43Entities();

        public ActionResult Index(int id)
        {
			List<Quiz> q = db.Quizs.ToList();
			List<Option> o = db.Options.ToList();
			List<CorrectOption> c = db.CorrectOptions.ToList();
			List<Question> t = db.Questions.ToList();

			
			List<FullQuizModel> m = new List<FullQuizModel>();
			foreach (Quiz d in q)
			{

				FullQuizModel f = new FullQuizModel();
				foreach (Question s in t)
				{

					f.Id = d.Id;
					f.Title = d.Title;
					f.TotalMarks = d.TotalMarks;
					f.DateCreated = d.DateCreated;
					f.CourseId = id;
					if (s.QuizId == d.Id)
					{
						f.QuizId = s.QuizId;
						f.Q_TotalMarks = s.TotalMarks;
						f.Name = s.Name;
						f.QuestionId = s.Id;

					}
					int x = 1;
					foreach (Option n in o)
					{

						if (n.QuestionsId == f.QuestionId && x == 1)
						{
							f.QuestionsId_1 = n.QuestionsId;
							f.OptionValue_1 = n.OptionValue;
							f.Option_Id_1 = n.Id;
							x = x + 1;

						}
						else if (n.QuestionsId == f.QuestionId && x == 2)
						{
							f.QuestionsId_2 = n.QuestionsId;
							f.OptionValue_2 = n.OptionValue;
							f.Option_Id_2 = n.Id;
							x = x + 1;

						}
						else if (n.QuestionsId == f.QuestionId && x == 3)
						{
							f.QuestionsId_3 = n.QuestionsId;
							f.OptionValue_3 = n.OptionValue;
							f.Option_Id_3 = n.Id;
							x = x + 1;

						}

					}
					m.Add(new FullQuizModel
					{
						Id = f.Id,
						Title = f.Title,
						DateCreated = f.DateCreated,
						TotalMarks = f.TotalMarks,
						CourseId = f.CourseId,
						QuestionId = f.QuestionId,
						Name = f.Name,
						Q_TotalMarks = f.Q_TotalMarks,
						QuizId = f.QuizId,
						Option_Id_1 = f.Option_Id_1,
						OptionValue_1 = f.OptionValue_1,
						QuestionsId_1 = f.QuestionsId_1,
						Option_Id_2 = f.Option_Id_2,
						OptionValue_2 = f.OptionValue_2,
						QuestionsId_2 = f.QuestionsId_2,
						OptionValue_3 = f.OptionValue_3,
						Option_Id_3 = f.Option_Id_3,
						QuestionsId_3 = f.QuestionsId_3

					});
				}

			}
			
            return View(m);
        }


		public ActionResult Dep_Courses()
		{
			List<Department> list_dep = db.Departments.ToList();
			List<DepartmentViewModel> dept_Model = new List<DepartmentViewModel>();
			DepartmentViewModel m = new DepartmentViewModel();
			foreach (Department d in list_dep)
			{
				m.Id = d.Id;
				m.Name = d.Name;
				m.Image = d.Image;
				dept_Model.Add(m);
			}
			return View(dept_Model);
		}


		public ActionResult Dep_wise_course(int id)
		{
			List<Course> List2 = db.Courses.ToList();
			List<CourseViewModels> viewList2 = new List<CourseViewModels>();
			CourseViewModels v = new CourseViewModels();
			var dept = db.Departments.Find(id);
			foreach (Course c in List2)
			{
				if (c.DepartmentId == id)
				{
					v.Id = c.Id;
					v.Title = c.Title;
					v.Credits = c.Credits;
					v.Fee = c.Fee;
					v.DepartmentName = dept.Name;
					viewList2.Add(v);
				}
			}
			return View(viewList2);
		}

	




		// GET: Quiz/Details/5
		public ActionResult Add_Question(int id)
        {
            return View();
        }
		[HttpPost]
		public ActionResult Add_Question(int id, QuizQuestionsViewModel model)
		{
			List<Question> m = db.Questions.ToList();
			List<Question> s = new List<Question>();
			foreach (Question v in m)
			{
				if (v.QuizId == id)
				{
					s.Add(v);
				}
			}
			if (s.Count < 10)
			{
				try
				{
					Question o = new Question();
					//o.Id = model.Id;
					o.Name = model.Name;
					o.TotalMarks = 1;
					o.QuizId = id;
					db.Questions.Add(o);
					db.SaveChanges();
					return RedirectToAction("Dep_Courses");
				}
				catch (Exception ex)
				{

				}
			}
			else
			{
				return View("LimitExceed");
			}
			return View();
		}

		// GET: Quiz/Create
		public ActionResult Create()
        {

            return View();
        }

        // POST: Quiz/Create
        [HttpPost]
        public ActionResult Create(int id, InstructorQuizViewModel model)
        {
            try
            {
				Quiz q = new Quiz();
				q.Title = model.Title;
				q.TotalMarks = model.TotalMarks;
				q.DateCreated = DateTime.Now;
				q.CourseId = id;
				db.Quizs.Add(q);
				db.SaveChanges();

                return RedirectToAction("Dep_Courses");
            }
            catch
            {
                return View();
            }
        }

        // GET: Quiz/Edit/5
        public ActionResult Quiz_Index(int id)
        {
			List<Quiz> G = db.Quizs.ToList();
			List<InstructorQuizViewModel> j = new List<InstructorQuizViewModel>();
			InstructorQuizViewModel m = new InstructorQuizViewModel();
			foreach (Quiz q in G)
			{
				if (q.CourseId == id)
				{
					m.DateCreated = q.DateCreated;
					m.Title = q.Title;
					m.CourseId = q.CourseId;
					m.TotalMarks = q.TotalMarks;
					m.Id = q.Id;
					j.Add(new InstructorQuizViewModel
					{
						Id = m.Id,
						TotalMarks = m.TotalMarks,
						DateCreated = m.DateCreated,
						CourseId = m.CourseId,
						Title = m.Title

					});

				}
			}
            return View(j);
        }

		public ActionResult Q_Index(int id)
		{
			List<Question> n = db.Questions.ToList();
			List<QuizQuestionsViewModel> l = new List<QuizQuestionsViewModel>();
			QuizQuestionsViewModel s = new QuizQuestionsViewModel();
			foreach (Question q in n)
			{
				if (q.QuizId == id)
				{
					l.Add(new QuizQuestionsViewModel
					{
						Id = q.Id,
					    Name = q.Name,
					    Q_TotalMarks = q.TotalMarks,
					    QuizId = q.QuizId
				});

				}
			}
			return View(l);
		}

		public ActionResult Q_Options(int id)
		{
			List<Option> n = db.Options.ToList();
			List<OptionViewModel> l = new List<OptionViewModel>();
			OptionViewModel e = new OptionViewModel();
			foreach (Option q in n)
			{
				if (q.QuestionsId == id)
				{
					e.Id = q.Id;
					e.OptionValue = q.OptionValue;
					e.QuestionsId = q.QuestionsId;
					l.Add(new OptionViewModel
					{
						Id = e.Id,
						OptionValue = e.OptionValue,
						QuestionsId = e.QuestionsId,
					});

				}
			}
			return View(l);
		}


		public ActionResult Create_options(int id)
		{

			return View();
		}
		[HttpPost]
		public ActionResult Create_options(int id, OptionViewModel model)
		{
			List<Option> m = db.Options.ToList();
			List<Option> s = new List<Option>();
			foreach (Option v in m)
			{
				if (v.QuestionsId == id)
				{
					s.Add(v);
				}
			}
			if (s.Count < 3)
			{
				try
				{
					Option q = new Option();
					q.QuestionsId = id;
					q.OptionValue = model.OptionValue;
					db.Options.Add(q);
					db.SaveChanges();

					return RedirectToAction("Dep_Courses");
				}
				catch
				{
					return View();
				}
			}
			else
			{
				return View("LimitExceed");
			}
		}
		// GET: Quiz/Delete/5
		public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Quiz/Delete/5
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

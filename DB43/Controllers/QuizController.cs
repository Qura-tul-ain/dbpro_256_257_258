﻿using System;
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
		DB433Entities db = new DB433Entities();
        public static int cid;
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
                dept_Model.Add(new DepartmentViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Image = m.Image
                });
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
                    viewList2.Add(new CourseViewModels
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Credits = v.Credits,
                        Fee = v.Fee,
                        DepartmentName = v.DepartmentName
                    });
				}
			}
			return View(viewList2);
		}






        // GET: Quiz/Details/5
        int sum;
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
                    sum = v.TotalMarks;
					s.Add(v);
				}
			}
            var quz = db.Quizs.Find(id);

            if (sum < quz.TotalMarks)
			{
				try
				{
					Question o = new Question();
					//o.Id = model.Id;
					o.Name = model.Name;
					o.TotalMarks = model.Q_TotalMarks;
					o.QuizId = id;
					db.Questions.Add(o);
					db.SaveChanges();
					return RedirectToAction("Q_Index",new { id=id});
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
		public ActionResult Create(int id)
        {
            cid = id;
            return View();
        }

        // POST: Quiz/Create
        [HttpPost]
        public ActionResult Create(InstructorQuizViewModel model)
        {
            try
            {
				Quiz q = new Quiz();
				q.Title = model.Title;
				q.TotalMarks = model.TotalMarks;
				q.DateCreated = DateTime.Now;
				q.CourseId = cid;
				db.Quizs.Add(q);
				db.SaveChanges();

                return RedirectToAction("Q_Index", new { id = cid });
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
                    q.IsAnswer = Convert.ToString(model.IsAnswer);
                    db.Options.Add(q);
                    db.SaveChanges();
                    if (model.IsAnswer == true)
                    {
                        CorrectOption op = new CorrectOption();
                        op.Correctvalue = model.OptionValue;
                        op.QuestionId = id;
                        db.CorrectOptions.Add(op);
                        db.SaveChanges();
                    }

					return RedirectToAction("Q_Index",new { id=id});
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
		public ActionResult Attempt_Quiz(int id)
		{
		//	var Quix = db.Quizs.Find(id);
		//	List<Question> Q = db.Questions.ToList();
		//	List<Option> O = db.Options.ToList();
		//	List<OptionViewModel> h = new List<OptionViewModel>();
		//	OptionViewModel n = new OptionViewModel();
		//	foreach (Option i in O)
		//	{
		//		n.Id = i.Id;
		//		n.IsAnswer = Convert.ToBoolean(i.IsAnswer);
		//		n.ischecked = false;
		//		n.OptionValue = i.OptionValue;
		//		n.QuestionsId = i.QuestionsId;
		//		h.Add(new OptionViewModel
		//		{
		//			Id = n.Id,
		//			IsAnswer = n.IsAnswer,
		//			ischecked = n.ischecked,
		//			OptionValue = n.OptionValue,
		//			QuestionsId = n.QuestionsId
		//		});

		//	}
		//	List<StudentQuizViewModel> G = new List<StudentQuizViewModel>();
		//	StudentQuizViewModel v = new StudentQuizViewModel();
		//	v.Id = Quix.Id;
		//	v.Title = Quix.Title;
		//	v.TotalMarks = Quix.TotalMarks;
		//	QuestionOptionModel b = new QuestionOptionModel();
		//	foreach (Question q in Q)
		//	{
		//		if (q.QuizId == id)
		//		{
		//			b.Id = q.Id;
		//			b.Q_TotalMarks = q.TotalMarks;
		//			b.Name = q.Name;
		//			v.Questions.Add(new QuizQuestionsViewModel
  //                  {
		//				Id = b.Id,
		//				Q_TotalMarks = b.Q_TotalMarks,
		//				Name = b.Name
		//			});
		//		}
		//	}
		//	foreach (OptionViewModel k in h)
		//	{
		//		foreach (QuizQuestionsViewModel e in v.Questions)
		//		{
		//			if (k.QuestionsId == e.Id)
		//			{
		//				e._option.Add(k);
		//			}
		//		}

		//	}

			return View();
		}

		[HttpPost]
		public ActionResult Attempt_Quiz(int id, StudentQuizViewModel d)
		{
            //StudentQuizViewModel f = new StudentQuizViewModel();
            //OptionViewModel h = new OptionViewModel();

            //f.Id = d.Id;
            //f.obtainedMarks = d.obtainedMarks;
            //f.Questions = d.Questions;
            //f.TotalMarks = d.TotalMarks;
            //f.Title = d.Title;
            //f.DateCreated = d.DateCreated;
            //f.Questions = d.Questions;

            //foreach (QuizQuestionsViewModel q in f.Questions)
            //{
            //	foreach (OptionViewModel k in q._option)
            //	{
            //		if (k.ischecked == true && k.IsAnswer == true)
            //		{
            //			f.obtainedMarks = f.obtainedMarks + 1;
            //		}
            //	}
            //}
            //return View("Result", f);
            return View();
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

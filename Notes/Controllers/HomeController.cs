using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.Controllers
{
    public class HomeController : Controller
    {
        private static List<Note> Notes { get; set; }

        static HomeController()
        {
            Notes = new List<Note>();
            for (int i = 1; i <= 10; ++i)
                Notes.Add(new Note() { Id = GenerateID(), Caption = "Note " + i, Text = "Text of note " + i, IsDone = i % 3 == 0 });
        }

        public ActionResult Index()
        {
            return View(Notes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Note model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Caption", "Caption cant be empty");
                return View(model);
            }
            model.Id = GenerateID();
            Notes.Add(model);
            return RedirectToAction("Details", new { model.Id });
        }

        private static string GenerateID()
        {
            string Id;
            do
            {
                Id = Guid.NewGuid().ToString();
            }
            while (Notes.Where(n => n.Id == Id).Any());
            return Id;
        }

        public ActionResult Edit(string Id)
        {
            return View(Notes.First(n => n.Id == Id));
        }

        [HttpPost]
        public ActionResult Edit(Note model)
        {
            var note = Notes.First(n => n.Id == model.Id);
            note.Caption = model.Caption;
            note.Text = model.Text;
            return RedirectToAction("Details", new { id = model.Id });
        }

        public ActionResult Details(string Id)
        {
            return View(Notes.First(n=>n.Id==Id));
        }

        public ActionResult Delete(string Id)
        {
            return View(Notes.First(n => n.Id == Id));
        }

        [HttpPost]
        public ActionResult Delete(Note model)
        {
            Notes.RemoveAll(n => n.Id == model.Id);
            return RedirectToAction("Index");
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

        override protected void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
            filterContext.ExceptionHandled = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fin.Models;

namespace Fin.Controllers
{
    public class ActivitiesAdminController : Controller
    {
        private Model1 db = new Model1();

        // GET: ActivitiesAdmin
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.PACKAGE);
            return View(activities.ToList());
        }

        // GET: ActivitiesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: ActivitiesAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            return View();
        }

        // POST: ActivitiesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Activity_ID,Act_Name,Act_Type,Act_description,Act_Image,Package_FID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", activity.Package_FID);
            return View(activity);
        }

        // GET: ActivitiesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", activity.Package_FID);
            return View(activity);
        }

        // POST: ActivitiesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            if (activity.ACT_IMG != null)
            {
                activity.ACT_IMG.SaveAs(Server.MapPath("~/ActivityImage/" + activity.ACT_IMG.FileName));
                activity.Act_Image = "~/ActivityImage/" + activity.ACT_IMG.FileName;

            }
            db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", activity.Package_FID);
            return View(activity);
        }

        // GET: ActivitiesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: ActivitiesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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

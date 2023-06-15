using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fin.Models;

namespace Fin.Controllers
{
    public class ActivitiesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = new List<Activity>();
            if (Session["BussinessUser"] != null)
            {
                var b = (Business_User)Session["BussinessUser"];
                activities = db.Activities.Include(a => a.PACKAGE).Where(x=>x.PACKAGE.FID_Business_User==b.Bus_User_Id).ToList();
            }
            else
            {
             activities = db.Activities.Include(a => a.PACKAGE).ToList();
            }
            return View(activities);
        }

        // GET: Activities/Details/5
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

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activity activity)
        {
           
                // string fileName = Path.GetFileNameWithoutExtension(activity.act_img.FileName);
                // string extension = Path.GetExtension(activity.act_img.FileName);
                // fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                // activity.Act_Image = "~/ActivityImage" + fileName;
                // fileName = Path.Combine(Server.MapPath("~/ActivityImage"), fileName);
                //  activity.act_img.SaveAs(fileName);
                activity.ACT_IMG.SaveAs(Server.MapPath("~/ActivityImage/" + activity.ACT_IMG.FileName));
                activity.Act_Image = "~/ActivityImage/" + activity.ACT_IMG.FileName;


                db.Activities.Add(activity);
                db.SaveChanges();
                //using(Model1 db = new Model1())
                // {
                //db.Activities.Add(activity);
                // db.SaveChanges();
                // }

                return RedirectToAction("Index");
            

            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", activity.Package_FID);
            return View(activity);
        }

        // GET: Activities/Edit/5
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

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Activity activity)
        {
            if(activity.ACT_IMG!= null)
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

        // GET: Activities/Delete/5
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

        // POST: Activities/Delete/5
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

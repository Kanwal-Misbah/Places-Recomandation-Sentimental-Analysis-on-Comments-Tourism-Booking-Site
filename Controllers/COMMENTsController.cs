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
    public class COMMENTsController : Controller
    {
        private Model1 db = new Model1();

        // GET: COMMENTs
        public ActionResult Index()
        {
            var cOMMENTs = db.COMMENTs.Include(c => c.PACKAGE).Include(c => c.User);
            return View(cOMMENTs.ToList());
        }

        // GET: COMMENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT cOMMENT = db.COMMENTs.Find(id);
            if (cOMMENT == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENT);
        }

        // GET: COMMENTs/Create
        public ActionResult Create()
        {
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name");
            return View();
        }

        // POST: COMMENTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,ComentedOn,ComentDescription,Rating,Package_FID,User_FID")] COMMENT cOMMENT)
        {
            if (ModelState.IsValid)
            {
                db.COMMENTs.Add(cOMMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", cOMMENT.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", cOMMENT.User_FID);
            return View(cOMMENT);
        }

        // GET: COMMENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT cOMMENT = db.COMMENTs.Find(id);
            if (cOMMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", cOMMENT.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", cOMMENT.User_FID);
            return View(cOMMENT);
        }

        // POST: COMMENTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,ComentedOn,ComentDescription,Rating,Package_FID,User_FID")] COMMENT cOMMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", cOMMENT.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", cOMMENT.User_FID);
            return View(cOMMENT);
        }

        // GET: COMMENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT cOMMENT = db.COMMENTs.Find(id);
            if (cOMMENT == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENT);
        }

        // POST: COMMENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COMMENT cOMMENT = db.COMMENTs.Find(id);
            db.COMMENTs.Remove(cOMMENT);
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

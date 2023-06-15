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
    public class Business_UserAController : Controller
    {
        private Model1 db = new Model1();

        // GET: Business_UserA
        public ActionResult Index()
        {
            return View(db.Business_User.ToList());
        }

        // GET: Business_UserA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_User business_User = db.Business_User.Find(id);
            if (business_User == null)
            {
                return HttpNotFound();
            }
            return View(business_User);
        }

        // GET: Business_UserA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Business_UserA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Bus_User_Id,Bus_Name,Bus_Address,Bank_Name,Bank_Account_number,Bus_Reg,Bus_Email,Bus_Password,Bus_logo,Bus_Location")] Business_User business_User)
        {
            if (ModelState.IsValid)
            {
                db.Business_User.Add(business_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business_User);
        }

        // GET: Business_UserA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_User business_User = db.Business_User.Find(id);
            if (business_User == null)
            {
                return HttpNotFound();
            }
            return View(business_User);
        }

        // POST: Business_UserA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Business_User business_User)
        {
            if (business_User.bus_log != null)
            {
                business_User.bus_log.SaveAs(Server.MapPath("~/BusinessUserImage/" + business_User.bus_log.FileName));
                business_User.Bus_logo = "~/BusinessUserImage/" + business_User.bus_log.FileName;
            }
            db.Entry(business_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            return View(business_User);
        }

        // GET: Business_UserA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_User business_User = db.Business_User.Find(id);
            if (business_User == null)
            {
                return HttpNotFound();
            }
            return View(business_User);
        }

        // POST: Business_UserA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business_User business_User = db.Business_User.Find(id);
            db.Business_User.Remove(business_User);
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

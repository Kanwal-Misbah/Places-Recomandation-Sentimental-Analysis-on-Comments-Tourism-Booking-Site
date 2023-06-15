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
    public class PACKAGEsAdminController : Controller
    {
        private Model1 db = new Model1();

        // GET: PACKAGEsAdmin
        public ActionResult Index()
        {
            var pACKAGEs = db.PACKAGEs.Include(p => p.Business_User).Include(p => p.Category).Include(p => p.Destination);
            return View(pACKAGEs.ToList());
        }

        // GET: PACKAGEsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGE pACKAGE = db.PACKAGEs.Find(id);
            if (pACKAGE == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGE);
        }

        // GET: PACKAGEsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.FID_Business_User = new SelectList(db.Business_User, "Bus_User_Id", "Bus_Name");
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Cat_Name");
            ViewBag.Destination_FID = new SelectList(db.Destinations, "Destination_Id", "Des_Name");
            return View();
        }

        // POST: PACKAGEsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Package_id,Package_title,Package_duration,No_of_Members,Package_description,FID_Business_User,Category_FID,Package_Image,Package_Price,Internet,Room_Service,Parking_Space,Meal_Service,Package_Style,Travel_Guider,Transport,Start_date,End_date,Discount,Destination_FID,Departure_Address,Sentimental_score")] PACKAGE pACKAGE)
        {
            if (ModelState.IsValid)
            {
                db.PACKAGEs.Add(pACKAGE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FID_Business_User = new SelectList(db.Business_User, "Bus_User_Id", "Bus_Name", pACKAGE.FID_Business_User);
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Cat_Name", pACKAGE.Category_FID);
            ViewBag.Destination_FID = new SelectList(db.Destinations, "Destination_Id", "Des_Name", pACKAGE.Destination_FID);
            return View(pACKAGE);
        }

        // GET: PACKAGEsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGE pACKAGE = db.PACKAGEs.Find(id);
            if (pACKAGE == null)
            {
                return HttpNotFound();
            }
            ViewBag.FID_Business_User = new SelectList(db.Business_User, "Bus_User_Id", "Bus_Name", pACKAGE.FID_Business_User);
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Cat_Name", pACKAGE.Category_FID);
            ViewBag.Destination_FID = new SelectList(db.Destinations, "Destination_Id", "Des_Name", pACKAGE.Destination_FID);
            return View(pACKAGE);
        }

        // POST: PACKAGEsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PACKAGE pACKAGE)
        {

            if (pACKAGE.pack_img != null)
            {
                pACKAGE.pack_img.SaveAs(Server.MapPath("~/PackageImage/" + pACKAGE.pack_img.FileName));
                pACKAGE.Package_Image = "~/PackageImage/" + pACKAGE.pack_img.FileName;
            }
                db.Entry(pACKAGE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            ViewBag.FID_Business_User = new SelectList(db.Business_User, "Bus_User_Id", "Bus_Name", pACKAGE.FID_Business_User);
            ViewBag.Category_FID = new SelectList(db.Categories, "Category_ID", "Cat_Name", pACKAGE.Category_FID);
            ViewBag.Destination_FID = new SelectList(db.Destinations, "Destination_Id", "Des_Name", pACKAGE.Destination_FID);
            return View(pACKAGE);
        }

        // GET: PACKAGEsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGE pACKAGE = db.PACKAGEs.Find(id);
            if (pACKAGE == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGE);
        }

        // POST: PACKAGEsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PACKAGE pACKAGE = db.PACKAGEs.Find(id);
            db.PACKAGEs.Remove(pACKAGE);
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

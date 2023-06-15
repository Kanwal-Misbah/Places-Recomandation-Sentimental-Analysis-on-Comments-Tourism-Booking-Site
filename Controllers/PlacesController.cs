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
    public class PlacesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Places
        public ActionResult Index()
        {
            var places = new List<Place>();
            if (Session["BussinessUser"] != null)
            {
                var b = (Business_User)Session["BussinessUser"];
                places = db.Places.Include(p => p.PACKAGE).Where(x=>x.PACKAGE.FID_Business_User==b.Bus_User_Id).ToList();
            }
            else
            {
                places = db.Places.Include(p => p.PACKAGE).ToList();
            }

            return View(places.ToList());
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Place place)
        {
           
                place.plc_img.SaveAs(Server.MapPath("~/PlacesImage/" + place.plc_img.FileName));
                place.Place_Image = "~/PlacesImage/" + place.plc_img.FileName;
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", place.Package_FID);
            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", place.Package_FID);
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Place place)
        {
            if (place.plc_img != null)
            {
                place.plc_img.SaveAs(Server.MapPath("~/PlacesImage/" + place.plc_img.FileName));
                place.Place_Image = "~/PlacesImage/" + place.plc_img.FileName;
            }
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", place.Package_FID);
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
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

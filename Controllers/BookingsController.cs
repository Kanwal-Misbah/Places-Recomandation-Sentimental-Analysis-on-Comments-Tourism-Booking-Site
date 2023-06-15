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
    public class BookingsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.PACKAGE).Include(b => b.PACKAGE).Include(b => b.User);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Booking_id,Booking_date,Package_FID,User_FID,No_of_Members,Departure,Total_Payment,Payment_status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", booking.Package_FID);
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", booking.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", booking.User_FID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", booking.Package_FID);
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", booking.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", booking.User_FID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_id,Booking_date,Package_FID,User_FID,No_of_Members,Departure,Total_Payment,Payment_status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", booking.Package_FID);
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", booking.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", booking.User_FID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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

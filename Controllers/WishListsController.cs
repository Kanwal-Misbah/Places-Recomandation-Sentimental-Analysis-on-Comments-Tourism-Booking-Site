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
    public class WishListsController : Controller
    {
        private Model1 db = new Model1();

        // GET: WishLists
        public ActionResult Index()
        {
            var wishLists = db.WishLists.Include(w => w.PACKAGE).Include(w => w.User);
            return View(wishLists.ToList());
        }

        // GET: WishLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // GET: WishLists/Create
        public ActionResult Create()
        {
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title");
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name");
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Wishlist_id,Wishlist_Date,Package_FID,User_FID")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.WishLists.Add(wishList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", wishList.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", wishList.User_FID);
            return View(wishList);
        }

        // GET: WishLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", wishList.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", wishList.User_FID);
            return View(wishList);
        }

        // POST: WishLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Wishlist_id,Wishlist_Date,Package_FID,User_FID")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Package_FID = new SelectList(db.PACKAGEs, "Package_id", "Package_title", wishList.Package_FID);
            ViewBag.User_FID = new SelectList(db.Users, "User_Id", "User_Name", wishList.User_FID);
            return View(wishList);
        }

        // GET: WishLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishList wishList = db.WishLists.Find(id);
            db.WishLists.Remove(wishList);
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

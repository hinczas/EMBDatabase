using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMBDatabase.Context;
using EMBDatabase.Models;

namespace EMBDatabase.Controllers
{
    public class PartsController : Controller
    {
        private EMBContext db = new EMBContext();

        // GET: Parts
        public ActionResult Index()
        {
            return View(db.Part.ToList());
        }

        // GET: Parts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Part.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: Parts/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerNames = new SelectList(db.Manufacturer, "Id", "Name");
            ViewBag.LocationNames = new SelectList(db.Location, "Id", "Name");
            ViewBag.PackageNames = new SelectList(db.Package, "Id", "Name");
            ViewBag.TypeNames = new SelectList(db.Type, "Id", "Name");
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,Keywords,Voltage,Current,Quantity,Pin_Count,Price,Name,Description,Notes,Manufacturer_Id,Location_Id,Package_Id,Type_Id")] Part part)
        {
            if (ModelState.IsValid)
            {
                part.CreateDate = new SqlDateTime(DateTime.Now).Value;

                db.Part.Add(part);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(part);
        }

        // GET: Parts/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Part.Find(id);

            ViewBag.Manufacturer_Id = new SelectList(db.Manufacturer, "Id", "Name", part.Manufacturer_Id);
            ViewBag.Location_Id = new SelectList(db.Location, "Id", "Name", part.Location_Id);
            ViewBag.Package_Id = new SelectList(db.Package, "Id", "Name", part.Package_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", part.Type_Id);

            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,Keywords,Voltage,Current,Quantity,Pin_Count,Price,Name,Description,Notes,CreateDate,Manufacturer_Id,Location_Id,Package_Id,Type_Id")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;

                part.UpdateDate = new SqlDateTime(DateTime.Now).Value;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(part);
        }

        // GET: Parts/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Part.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            //return View(part);
            db.Part.Remove(part);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Part part = db.Part.Find(id);
            db.Part.Remove(part);
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

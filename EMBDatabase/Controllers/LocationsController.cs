using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMBDatabase.Classes;
using EMBDatabase.Context;
using EMBDatabase.Models;

namespace EMBDatabase.Controllers
{
    public class LocationsController : Controller
    {
        private EMBContext db = new EMBContext();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Location.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,File_Id,Name,Description,Notes,CreateDate,UpdateDate")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.CreateDate = new SqlDateTime(DateTime.Now).Value;
                db.Location.Add(location);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = location.Id });
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,File_Id,Name,Description,Notes,CreateDate,UpdateDate,del_file")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;

                location.UpdateDate = new SqlDateTime(DateTime.Now).Value;

                var checkbox = ValueProvider.GetValue("del_file");
                if (checkbox != null)
                {
                    var fid = Int32.Parse(checkbox.AttemptedValue.Replace('/', '\0'));
                    var file = db.File.Find(fid);

                    if (System.IO.File.Exists(Server.MapPath(file.File_Path)))
                    {
                        System.IO.File.Delete(Server.MapPath(file.File_Path));
                        db.Manufacturer.Find(location.Id).File = null;
                        location.File_Id = null;
                        db.File.Remove(file);
                    }

                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = location.Id });
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            db.Location.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Location location = db.Location.Find(id);
            db.Location.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            FileService fs = new FileService();
            byte[] fileBytes = fs.ExportToFile<Location, ExportLocation>();
            string fileName = DateTime.Now.ToString("yyMMddHHmmss") + ".Locations.tsv";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    FileService fs = new FileService();

                    var dbFile = fs.PrepareFile(file);

                    List<Location> partLines = fs.ImportDelimitedFile<ExportLocation, Location>(dbFile);
                    foreach (Location line in partLines)
                    {
                        var existingLine = db.Location.Where(a => a.Name.Equals(line.Name)).FirstOrDefault();
                        if (existingLine == null)
                        {
                            db.Location.Add(line);
                        }
                    }

                    db.SaveChanges();

                    fs.DeleteFile(dbFile);

                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("Index");
            }


            //return RedirectToAction("Index");
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMBDatabase.Classes;
using EMBDatabase.Context;
using EMBDatabase.Models;

namespace EMBDatabase.Controllers
{
    public class ManufacturersController : Controller
    {
        private EMBContext db = new EMBContext();

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(db.Manufacturer.ToList());
        }

        // GET: Manufacturers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Full_Name,Address,Email,Website,Name,Description,Notes,CreateDate,UpdateDate")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.CreateDate = new SqlDateTime(DateTime.Now).Value;
                db.Manufacturer.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = manufacturer.Id });
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Full_Name,Address,Email,Website,Name,Description,Notes,CreateDate,UpdateDate,del_file")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;

                manufacturer.UpdateDate = new SqlDateTime(DateTime.Now).Value;

                var checkbox = ValueProvider.GetValue("del_file");
                if (checkbox != null)
                {
                    var fid = Int32.Parse(checkbox.AttemptedValue.Replace('/', '\0'));
                    var file = db.File.Find(fid);

                    if (System.IO.File.Exists(Server.MapPath(file.File_Path)))
                    {
                        System.IO.File.Delete(Server.MapPath(file.File_Path));
                        db.Manufacturer.Find(manufacturer.Id).File = null;
                        manufacturer.File_Id = null;
                        db.File.Remove(file);
                    }

                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = manufacturer.Id });
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            db.Manufacturer.Remove(manufacturer);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            db.Manufacturer.Remove(manufacturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Export()
        {
            FileService fs = new FileService();
            byte[] fileBytes = fs.ExportToFile<Manufacturer, ExportManufacturer>();
            string fileName = DateTime.Now.ToString("yyMMddHHmmss") + ".Manufacturers.csv";
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

                    List<Manufacturer> partLines = fs.ImportDelimitedFile<ExportManufacturer, Manufacturer>(dbFile);
                    foreach (Manufacturer line in partLines)
                    {
                        var existingLine = db.Manufacturer.Where(a => a.Full_Name.Equals(line.Full_Name)).FirstOrDefault();
                        if (existingLine == null)
                        {
                            db.Manufacturer.Add(line);
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

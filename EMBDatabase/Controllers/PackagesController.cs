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
    public class PackagesController : Controller
    {
        private EMBContext db = new EMBContext();

        // GET: Packages
        public ActionResult Index()
        {
            return View(db.Package.ToList());
        }

        // GET: Packages/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Package.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,File_Id,Name,Description,Notes,CreateDate,UpdateDate")] Package package)
        {
            if (ModelState.IsValid)
            {
                package.CreateDate = new SqlDateTime(DateTime.Now).Value;
                db.Package.Add(package);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = package.Id });
            }

            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Package.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,File_Id,Name,Description,Notes,CreateDate,UpdateDate,del_file")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                package.UpdateDate = new SqlDateTime(DateTime.Now).Value;

                var checkbox = ValueProvider.GetValue("del_file");
                if (checkbox != null)
                {
                    var fid = Int32.Parse(checkbox.AttemptedValue.Replace('/', '\0'));
                    var file = db.File.Find(fid);

                    if (System.IO.File.Exists(Server.MapPath(file.File_Path)))
                    {
                        System.IO.File.Delete(Server.MapPath(file.File_Path));
                        db.Manufacturer.Find(package.Id).File = null;
                        package.File_Id = null;
                        db.File.Remove(file);
                    }

                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = package.Id });
            }
            return View(package);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Package.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            db.Package.Remove(package);
            db.SaveChanges();
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Package package = db.Package.Find(id);
            db.Package.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            FileService fs = new FileService();
            byte[] fileBytes = fs.ExportToFile<Package, ExportPackage>();
            string fileName = DateTime.Now.ToString("yyMMddHHmmss") + ".Package.tsv";
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

                    List<Package> partLines = fs.ImportDelimitedFile<ExportPackage, Package>(dbFile);
                    foreach (Package line in partLines)
                    {
                        var existingLine = db.Package.Where(a => a.Number.Equals(line.Number)).FirstOrDefault();
                        if (existingLine == null)
                        {
                            db.Package.Add(line);
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

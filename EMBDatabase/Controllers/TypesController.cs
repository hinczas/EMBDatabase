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
    public class TypesController : Controller
    {
        private EMBContext db = new EMBContext();

        // GET: Types
        public ActionResult Index()
        {
            return View(db.Type.ToList());
        }

        // GET: Types/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Type.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,File_Id,Name,Description,Notes,CreateDate,UpdateDate")] Models.Type type)
        {
            if (ModelState.IsValid)
            {
                type.CreateDate = new SqlDateTime(DateTime.Now).Value;
                db.Type.Add(type);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = type.Id });
            }

            return View(type);
        }

        // GET: Types/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Type.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,File_Id,Name,Description,Notes,CreateDate,UpdateDate,del_file")] Models.Type type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                type.UpdateDate = new SqlDateTime(DateTime.Now).Value;

                var checkbox = ValueProvider.GetValue("del_file");
                if (checkbox != null)
                {
                    var fid = Int32.Parse(checkbox.AttemptedValue.Replace('/', '\0'));
                    var file = db.File.Find(fid);

                    if (System.IO.File.Exists(Server.MapPath(file.File_Path)))
                    {
                        System.IO.File.Delete(Server.MapPath(file.File_Path));
                        db.Type.Find(type.Id).File = null;
                        type.File_Id = null;
                        db.File.Remove(file);
                    }

                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = type.Id });
            }
            return View(type);
        }

        // GET: Types/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Type.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            db.Type.Remove(type);
            db.SaveChanges();
            return View(type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Models.Type type = db.Type.Find(id);
            db.Type.Remove(type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            FileService fs = new FileService();
            byte[] fileBytes = fs.ExportToFile<Models.Type, ExportType>();
            string fileName = DateTime.Now.ToString("yyMMddHHmmss") + ".Type.tsv";
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

                    List<Models.Type> partLines = fs.ImportDelimitedFile<ExportType, Models.Type>(dbFile);
                    foreach (Models.Type line in partLines)
                    {
                        var existingLine = db.Type.Where(a => a.Name.Equals(line.Name)).FirstOrDefault();
                        if (existingLine == null)
                        {
                            db.Type.Add(line);
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

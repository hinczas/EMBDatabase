using EMBDatabase.Context;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMBDatabase.Classes;

namespace EMBDatabase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult Electronics()
        {
            ViewBag.Message = "Electronics page.";

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, long id, string sender="", string file_type="")
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Files/"+file_type), _FileName);
                    string relativePath = "~/Content/Files/" + file_type + "/" + _FileName;
                    file.SaveAs(_path);
                    if (sender.Equals(Constants.FILE_SENDER_PART))
                    {
                        var db = new EMBContext();
                        var part = db.Part.Find(id);
                        if (part!=null)
                        {
                            part.File.Add(new Models.File()
                            {
                                Name = _FileName,
                                File_Path = relativePath,
                                File_Type = file_type,
                                CreateDate = DateTime.Now
                            });
                            db.SaveChanges();
                        }
                    }
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("Details", sender, new { id = id });
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("Details", sender, new { id = id });
            }
        }
    }
}
using EMBDatabase.Context;
using EMBDatabase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EMBDatabase.Classes
{
    public class FileService
    {
        private readonly EMBContext db;

        public FileService()
        {
            db = new EMBContext();
        }

        public Models.File PrepareFile(HttpPostedFileBase file)
        {
            string _FileName = Path.GetFileName(file.FileName);
            string _path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Files/Static"), DateTime.Now.ToString("yyMMddHHmmss") + "_" + _FileName);
            string relativePath = "~/Content/Files/Static/" + DateTime.Now.ToString("yyMMddHHmmss") + "_" + _FileName;
            var dbFile = new Models.File()
            {
                Name = _FileName,
                File_Path = _path,
                CreateDate = DateTime.Now
            };
            file.SaveAs(_path);
            db.File.Add(dbFile);
            db.SaveChanges();

            return dbFile;
        }
        public bool DeleteFile(Models.File file)
        {
            bool exists = System.IO.File.Exists(file.File_Path);

            if (exists)
            {
                exists = true;
                System.IO.File.Delete(file.File_Path);
                db.File.Attach(file);
                db.File.Remove(file);
                db.SaveChanges();
            }

            return exists;
        }

        public List<T2> ImportDelimitedFile<T1, T2>(Models.File file, char delimiter = Constants.FILE_DELIMITER)
        {
            List<T2> data = new List<T2>();

            // Read file
            string[] allLines = System.IO.File.ReadAllLines(file.File_Path);
            string[] headerLine = allLines[0].Split(new[] { delimiter });

            // Parse header row to match with existing models
            System.Type srcType = typeof(T1);
            PropertyInfo[] srcProperties = new PropertyInfo[headerLine.Length];
            for (int prop = 0; prop < srcProperties.Length; prop++)
            {
                srcProperties[prop] = srcType.GetProperty(headerLine[prop]);
            }

            // Load values matching header columns with model structure
            for (int count = 1; count < allLines.Length; count++)
            {
                string[] valuesLine = allLines[count].Split(new[] { delimiter });
                var sourceModelT1 = Activator.CreateInstance<T1>();

                for (int value = 0; value < valuesLine.Length; value++)
                {
                    var t = srcProperties[value].PropertyType;

                    if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        t = Nullable.GetUnderlyingType(t);
                    }
                    
                    if (srcProperties[value] != null && !string.IsNullOrEmpty(valuesLine[value]))
                    {
                        srcProperties[value].SetValue(sourceModelT1, Convert.ChangeType(valuesLine[value], t));
                    }
                }

                var targetModelT2 = AutoMapper.Mapper.Map<T1, T2>(sourceModelT1);

                data.Add(targetModelT2);
            }

            return data;
        }

        public bool ExportToFile<T1, T2>(string savePath = "D:\\TEMP\\")
            where T1 : class
        {
            var allItems = db.Set<T1>().ToList();
            string fileName = DateTime.Now.ToString("yyMMddHHmmss") + "."+ typeof(T1).Name+ ".csv";

            //var allItems = db.Part.ToList();

            bool header = true;
            //AutoMapper.Mapper.Initialize(cfg => { cfg.CreateMap<T1, T2>(); });

            using (StreamWriter writer = System.IO.File.CreateText(Path.Combine(savePath, fileName)))
            {

                foreach (T1 item in (List<T1>)allItems)
                {
                    var expPart = AutoMapper.Mapper.Map<T1, T2>(item);
                    if (header)
                    {

                        var title_list = new List<object>();
                        foreach (PropertyInfo property in expPart.GetType().GetProperties())
                        {
                            title_list.Add(property.Name);
                        }

                        var headerLine = string.Join(Constants.FILE_DELIMITER.ToString(), title_list);
                        writer.WriteLine(headerLine);

                        header = false;

                    }

                    var field_list = new List<object>();

                    foreach (PropertyInfo property in expPart.GetType().GetProperties())
                    {
                        var val = property.GetValue(expPart, null);
                        field_list.Add(val);
                    }

                    var line = string.Join(Constants.FILE_DELIMITER.ToString(), field_list);
                    writer.WriteLine(line);
                }
                return true;
            }
        }

        public byte[] ExportToFile<T1, T2>()
            where T1 : class
        {
            var allItems = db.Set<T1>().ToList();

            MemoryStream fs = new MemoryStream();
            TextWriter tx = new StreamWriter(fs);


            bool header = true;
            
            foreach (T1 item in (List<T1>)allItems)
            {
                var expPart = AutoMapper.Mapper.Map<T1, T2>(item);
                if (header)
                {

                    var title_list = new List<object>();
                    foreach (PropertyInfo property in expPart.GetType().GetProperties())
                    {
                        title_list.Add(property.Name);
                    }

                    var headerLine = string.Join(Constants.FILE_DELIMITER.ToString(), title_list);
                    tx.WriteLine(headerLine);

                    header = false;

                }

                var field_list = new List<object>();

                foreach (PropertyInfo property in expPart.GetType().GetProperties())
                {
                    var val = property.GetValue(expPart, null);
                    field_list.Add(val);
                }

                var line = string.Join(Constants.FILE_DELIMITER.ToString(), field_list);
                tx.WriteLine(line);
            }
            tx.Flush();
            return fs.ToArray();
        }
    }
}
using EMBDatabase.Context;
using EMBDatabase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EMBDatabase.Classes
{
    public class FileService
    {
        private readonly EMBContext db;

        public FileService()
        {
            db = new EMBContext();
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

                        var headerLine = string.Join(",", title_list);
                        writer.WriteLine(headerLine);

                        header = false;

                    }

                    var field_list = new List<object>();

                    foreach (PropertyInfo property in expPart.GetType().GetProperties())
                    {
                        var val = property.GetValue(expPart, null);
                        field_list.Add(val);
                    }

                    var line = string.Join(",", field_list);
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

                    var headerLine = string.Join(",", title_list);
                    tx.WriteLine(headerLine);

                    header = false;

                }

                var field_list = new List<object>();

                foreach (PropertyInfo property in expPart.GetType().GetProperties())
                {
                    var val = property.GetValue(expPart, null);
                    field_list.Add(val);
                }

                var line = string.Join(",", field_list);
                tx.WriteLine(line);
            }
            return fs.ToArray();
        }
    }
}
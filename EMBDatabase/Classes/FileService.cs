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

        public bool ExportToFile(string savePath = "D:\\")
        {
            var allParts = db.Part.ToList();
            string fileName = "testAM.csv";

            bool header = true;

            using (StreamWriter writer = System.IO.File.CreateText(Path.Combine(savePath, fileName)))
            {

                foreach (Part part in allParts)
                {
                    var expPart = AutoMapper.Mapper.Map<Part, ExportPart>(part);
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
    }
}
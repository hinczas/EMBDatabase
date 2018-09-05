using EMBDatabase.Context;
using EMBDatabase.Models;
using EMBDatabase.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMBDatabase.Classes
{
    public class DataService
    {
        private EMBContext db;

        public DataService()
        {
            db = new EMBContext();
        }

        public ApiPart GetPart(int id)
        {
            var part = db.Part.Where(a => a.Id == id).FirstOrDefault();
            var epart = AutoMapper.Mapper.Map<ApiPart>(part);
            return epart;
        }
        public List<ApiPart> GetParts()
        {
            var parts = AutoMapper.Mapper.Map<List<ApiPart>>(db.Part.ToList());
            return parts;
        }

        public bool AddPart(ApiPart apiPart)
        {
            // Manufacturer setup
            if (apiPart.Manufacturer_Id==null)
            {
                if (!string.IsNullOrEmpty(apiPart.Manufacturer_Name))
                {
                    var man = db.Manufacturer.Where(a => a.Name.ToLower().Equals(apiPart.Manufacturer_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Manufacturer_Id = man.Id;
                    } else
                    {
                        db.Manufacturer.Add(new Manufacturer()
                        {
                            Name = apiPart.Manufacturer_Name,
                            Full_Name = apiPart.Manufacturer_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Manufacturer.Where(a => a.Name.Equals(apiPart.Manufacturer_Name)).FirstOrDefault();

                        apiPart.Manufacturer_Id = newMan.Id;
                    }
                }
            }

            // Package setup
            if (apiPart.Package_Id == null)
            {
                if (!string.IsNullOrEmpty(apiPart.Package_Name))
                {
                    var man = db.Package.Where(a => a.Name.ToLower().Equals(apiPart.Package_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Package_Id = man.Id;
                    }
                    else
                    {
                        db.Package.Add(new Package()
                        {
                            Name = apiPart.Package_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Package.Where(a => a.Name.Equals(apiPart.Package_Name)).FirstOrDefault();

                        apiPart.Package_Id = newMan.Id;
                    }
                }
            }

            // Location setup
            if (apiPart.Location_Id == null)
            {
                if (!string.IsNullOrEmpty(apiPart.Location_Name))
                {
                    var man = db.Location.Where(a => a.Name.ToLower().Equals(apiPart.Location_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Location_Id = man.Id;
                    }
                    else
                    {
                        db.Location.Add(new Location()
                        {
                            Name = apiPart.Location_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Location.Where(a => a.Name.Equals(apiPart.Location_Name)).FirstOrDefault();

                        apiPart.Location_Id = newMan.Id;
                    }
                }
            }

            // Type setup
            if (apiPart.Type_Id == null)
            {
                if (!string.IsNullOrEmpty(apiPart.Type_Name))
                {
                    var man = db.Type.Where(a => a.Name.ToLower().Equals(apiPart.Type_Name.ToLower())).FirstOrDefault();
                    if (man != null)
                    {
                        apiPart.Type_Id = man.Id;
                    }
                    else
                    {
                        db.Type.Add(new Models.Type()
                        {
                            Name = apiPart.Type_Name,
                            Notes = "Created through Part API",
                            CreateDate = DateTime.Now
                        });
                        db.SaveChangesAsync();

                        var newMan = db.Type.Where(a => a.Name.Equals(apiPart.Type_Name)).FirstOrDefault();

                        apiPart.Type_Id = newMan.Id;
                    }
                }
            }

            // Finally create Part
            try
            {
                var part = AutoMapper.Mapper.Map<Part>(apiPart);
                part.CreateDate = DateTime.Now;

                db.Part.Add(part);
                db.SaveChangesAsync();

                return true;
            } catch
            {
                return false;
            }
        }
    }
}